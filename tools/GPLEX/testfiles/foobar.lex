/*
   This file checks that ambiguous tokens of the 
   same length are recognized in production order priority
   Thus "foo" matches the first and third rules
   Try > foobar "foo bar foobar tla" 
 */

%namespace LexScanner
%option noparser nofiles

alpha [a-zA-Z]

%%
foo         |
bar         Console.WriteLine("keyword " + yytext);
{alpha}{3}  Console.WriteLine("TLA " + yytext);
{alpha}+    Console.WriteLine("ident: " + yytext);

%%

    public static void Main(string[] argp) { 
        Scanner scnr = new Scanner();
        for (int i = 0; i < argp.Length; i++) {
            Console.WriteLine("Scanning \"" + argp[i] + "\"");
            scnr.SetSource(argp[i], 0);
            scnr.yylex();
        }
    }

