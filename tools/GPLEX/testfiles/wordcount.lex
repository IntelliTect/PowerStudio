/*
   Using LEX to implement a simple case of the unix "wc" utility.
 */

%namespace LexScanner
%option noparser, verbose, summary

%{
    static int lineTot = 0;
    static int wordTot = 0;
    static int intTot = 0;
    static int fltTot = 0;
%}

alpha [a-zA-Z]
alphaplus [a-zA-Z\-']
digits [0-9]+

%%
%{
    // local variables
    int lineNum = 0;
    int wordNum = 0;
    int intNum = 0;
    int fltNum = 0;
%}

\n|\r\n?              lineNum++; lineTot++;
{alpha}{alphaplus}+   wordNum++; wordTot++;
{digits}              intNum++; intTot++;
{digits}\.{digits}    fltNum++; fltTot++; // Console.WriteLine(yytext);
<<EOF>>               {
            Console.Write("Lines: " + lineNum);
            Console.Write(", Words: " + wordNum);
            Console.Write(", Ints: " + intNum);
            Console.WriteLine(", Floats: " + fltNum);
        }
%%

    public static void Main(string[] argp) {
        DateTime start = DateTime.Now;
        int count = 0;
        if (argp.Length == 0)  
            Console.WriteLine("Usage: WordCount filename(s), (wildcards ok)");
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
        if (count > 1) {
            Console.Write("Total Lines: " + lineTot);
            Console.Write(", Words: " + wordTot);
            Console.Write(", Ints: " + intTot);
            Console.WriteLine(", Floats: " + fltTot);
        }
        TimeSpan span = DateTime.Now - start;
        Console.WriteLine("Elapsed time: {0,4:D} msec", (int)span.TotalMilliseconds);
    }

