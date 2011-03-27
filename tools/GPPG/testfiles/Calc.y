%{
    int[] regs = new int[26];
    int _base;
%}

%start list

%token DIGIT LETTER

%left '|'
%left '&'
%left '+' '-'
%left '*' '/' '%'
%left UMINUS

%%

list    :   /*empty */
        |   list stat '\n'
        |   list error '\n'
                {
                    yyerrok();
                }
        ;

stat    :   expr
                {
                    System.Console.WriteLine($1);
                }
        |   LETTER '=' expr
                {
                    regs[$1] = $3;
                }
        ;

expr    :   '(' expr ')'
                {
                    $$ = $2;
                }
        |   expr '*' expr
                {
                    $$ = $1 * $3;
                }
        |   expr '/' expr
                {
                    $$ = $1 / $3;
                }
        |   expr '%' expr
                {
                    $$ = $1 % $3;
                }
        |   expr '+' expr
                {
                    $$ = $1 + $3;
                }
        |   expr '-' expr
                {
                    $$ = $1 - $3;
                }
        |   expr '&' expr
                {
                    $$ = $1 & $3;
                }
        |   expr '|' expr
                {
                    $$ = $1 | $3;
                }
        |   '-' expr %prec UMINUS
                {
                    $$ = -$2;
                }
        |   LETTER
                {
                    $$ = regs[$1];
                }
        |   number
        ;

number  :   DIGIT
                {
                    $$ = $1;
                    _base = ($1==0) ? 8 : 10;
                }
        |   number DIGIT
                {
                    $$ = _base * $1 + $2;
                }
        ;

%%

    Parser() : base(null) { }

    static void Main(string[] args)
    {
        Parser parser = new Parser();
        
        System.IO.TextReader reader;
        if (args.Length > 0)
            reader = new System.IO.StreamReader(args[0]);
        else
            reader = System.Console.In;
            
        parser.Scanner = new Lexer(reader);
        //parser.Trace = true;
        
        parser.Parse();
    }


    /*
     * Copied from GPPG documentation.
     */
    class Lexer: QUT.Gppg.AbstractScanner<int,LexLocation>
    {
         private System.IO.TextReader reader;
    
         //
         // Version 1.2.0 needed the following code.
         // In V1.2.1 the base class provides this empty default.
         //
         // public override LexLocation yylloc { 
         //     get { return null; } 
         //     set { /* skip */; }
         // }
         //
    
         public Lexer(System.IO.TextReader reader)
         {
             this.reader = reader;
         }
    
         public override int yylex()
         {
             char ch;
             int ord = reader.Read();
             //
             // Must check for EOF
             //
             if (ord == -1)
                 return (int)Tokens.EOF;
             else
                 ch = (char)ord;
    
             if (ch == '\n')
                return ch;
             else if (char.IsWhiteSpace(ch))
                 return yylex();
             else if (char.IsDigit(ch))
             {
                 yylval = ch - '0';
                 return (int)Tokens.DIGIT;
             }
             // Don't use IsLetter here!
             else if ((ch >= 'a' && ch <= 'z') ||
                      (ch >= 'A' && ch <= 'Z'))
             {
                yylval = char.ToLower(ch) - 'a';
                return (int)Tokens.LETTER;
             }
             else
                 switch (ch)
                 {
                     case '+':
                     case '-':
                     case '*':
                     case '/':
                     case '(':
                     case ')':
                     case '%':
                     case '=':
                         return ch;
                     default:
                         Console.Error.WriteLine("Illegal character '{0}'", ch);
                         return yylex();
                 }
         }
    
         public override void yyerror(string format, params object[] args)
         {
             Console.Error.WriteLine(format, args);
         }
    }
