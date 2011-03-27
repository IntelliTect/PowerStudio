/*
   A simple example of using GPLEX to implement the unix "strings" 
   functionality.  Reads a (possibly binary) file, finding sequences
   of alphabetic ASCII characters.
 */
   
%namespace LexScanner
%option verbose, summary, noparser

alpha [a-zA-Z]
alphaplus [a-zA-Z\-']

%%

{alpha}{alphaplus}*{alpha}   Console.WriteLine(yytext);

%%
    public static void Main(string[] argp) {
        if (argp.Length == 0)  
            Console.WriteLine("Usage: strings filename(s), (wildcards ok)");
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
		} catch (IOException) {
		    Console.WriteLine("File " + name + " not found");
		}
            }
        }
    }


