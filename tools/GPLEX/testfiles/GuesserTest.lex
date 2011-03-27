
%namespace GPLEX.Guesser
%option verbose summary noparser codepage:raw out:GuesserTest.cs 

/* 
 *  Reads the bytes of a file to determine if it is 
 *  UTF-8 or a single-byte codepage file.
 */
 
%{
    int utf2 = 0;
    int utf3 = 0;
    int utf4 = 0;
    int uppr = 0;
%}
 
Utf8pfx2    [\xc0-\xdf]
Utf8pfx3    [\xe0-\xef]
Utf8pfx4    [\xf0-\xf7]
Utf8cont    [\x80-\xbf]
Upper128    [\x80-\xff]
 
%%

{Utf8pfx2}{Utf8cont}     { utf2++; }
{Utf8pfx3}{Utf8cont}{2}  { utf3++; }
{Utf8pfx3}{Utf8cont}     { uppr += 2; }
{Utf8pfx4}{Utf8cont}{3}  { utf4++; }
{Utf8pfx4}{Utf8cont}     { uppr += 2; }
{Utf8pfx4}{Utf8cont}{2}  { uppr += 3; }
{Upper128}               { uppr++; }

<<EOF>> {
      Console.WriteLine("utf2 {0}, utf3 {1}, utf4 {2}, uppr {3}", utf2, utf3, utf4, uppr);
      Console.WriteLine("Guesser says: this is a {0} file",
               (utf2 + utf3 * 2 + utf4 * 3 > uppr * 10 ? "utf-8" : "default-codepage"));
    }

%%

    public static void Main(string[] argp) {
        DateTime start = DateTime.Now;
        int count = 0;
        if (argp.Length == 0)  
            Console.WriteLine("Usage: Guesser filename(s), (wildcards ok)");
        DirectoryInfo dirInfo = new DirectoryInfo(".");
        for (int i = 0; i < argp.Length; i++) {
            string name = argp[i];
            FileInfo[] fInfo = dirInfo.GetFiles(name);
            foreach (FileInfo info in fInfo)
            {
	        try {
		    int tok;
 		    FileStream file = new FileStream(info.Name, FileMode.Open); 
		    Scanner scnr = new Scanner(file);
		    Console.WriteLine("File: " + info.Name);
		    do {
  		        tok = scnr.yylex();
		    } while (tok > (int)Tokens.EOF);
		    count++;
		} catch (IOException) {
		    Console.WriteLine("File " + name + " not found");
		}
            }
        }
        TimeSpan span = DateTime.Now - start;
        Console.WriteLine("Elapsed time: {0,4:D} msec", (int)span.TotalMilliseconds);
    }
