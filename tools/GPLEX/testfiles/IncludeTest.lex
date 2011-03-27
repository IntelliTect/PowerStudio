/*
   A simple example of using GPLEX to implement the unix "strings" 
   functionality.  Reads a (possibly binary) file, finding sequences
   of alphabetic ASCII characters.
 */
   
%namespace LexScanner
%option verbose, summary, noparser

%{
    Stack<BufferContext> buffStack = new Stack<BufferContext>();
    string Indent() { return new string(' ', buffStack.Count * 4); }
%}

eol   (\r\n?|\n)
dotchr [^\r\n]
alpha [a-zA-Z]
alphaplus [a-zA-Z\-']

%x INCL

%%

{alpha}{alphaplus}*{alpha}   Console.WriteLine("{0}{1} {2}:{3}", Indent(), yytext, yyline, yycol);

^"#include"                  BEGIN(INCL);

<INCL>{eol}                  BEGIN(INITIAL); TryInclude(null);
<INCL>[ \t]                  /* skip whitespace */
<INCL>[^ \t]{dotchr}*        BEGIN(INITIAL); TryInclude(yytext);      

%%
    public static void Main(string[] argp) {
        if (argp.Length == 0)  
        {
            Console.WriteLine("Usage: IncludeTest args");
            Console.WriteLine("example> IncludeTest \"#include IncludeTest.txt\"");
        }
        else
        {
            int tok;
            Scanner scnr = new Scanner();
            scnr.SetSource(argp);
            do {
                tok = scnr.yylex();
            } while (tok > (int)Tokens.EOF);
        }
    }


    private void TryInclude(string fName)
    {
        if (fName == null)
            Console.Error.WriteLine("#include, no filename");
        else 
            try {
                BufferContext savedCtx = MkBuffCtx();
                SetSource(new FileStream(fName, FileMode.Open));
                Console.WriteLine("Included file {0} opened", fName);
                buffStack.Push(savedCtx); // Don't push until file open succeeds!
            }
            catch
            {
                Console.Error.WriteLine("#include, could not open file \"{0}\"", fName);
            }
    }

    protected override bool yywrap()
    {
        if (buffStack.Count == 0) return true;
        RestoreBuffCtx(buffStack.Pop());
        Console.WriteLine("Popped include file stack");
        return false;     
    }


