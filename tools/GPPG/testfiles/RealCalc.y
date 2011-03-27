%{
    double[] regs = new double[26];
    StringBuilder buffer = null;
%}

%start list

%union { public double dVal; 
         public char cVal; 
         public int iVal; }

%token <cVal> DIGIT 
%token <iVal> LETTER
%type <dVal> expr

%left '+' '-'
%left '*' '/'
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
        |   expr '+' expr
                {
                    $$ = $1 + $3;
                }
        |   expr '-' expr
                {
                    $$ = $1 - $3;
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
                {
                    try {
                        $$ = double.Parse(buffer.ToString());
                    } catch (FormatException) {
                        scanner.yyerror("Illegal number \"{0}\"", buffer);
                    }
                }
        ;

number  :   DIGIT  
               { 
                   buffer = new StringBuilder();
                   buffer.Append($1);
               }
        |   number DIGIT
               { 
                   buffer.Append($2);
               }
        |   number '.' DIGIT
               { 
                   buffer.Append('.');
                   buffer.Append($3);
               }
        ;

%%

static void Main(string[] args)
{
    Parser parser = new Parser();
    
    System.IO.TextReader reader;
    if (args.Length > 0)
        reader = new System.IO.StreamReader(args[0]);
    else
        reader = System.Console.In;
        
    parser.scanner = new Lexer(reader);
    //parser.Trace = true;
    
    Console.WriteLine("RealCalc expression evaluator, type ^C to exit");
    parser.Parse();
}

/*
 *  Version for real arithmetic.  YYSTYPE is ValueType.
 */
class Lexer: QUT.Gppg.AbstractScanner<ValueType, LexLocation>
{
     private System.IO.TextReader reader;

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
             yylval.cVal = ch;
             return (int)Tokens.DIGIT;
         }
         else if ((ch >= 'a' && ch <= 'z') ||
                  (ch >= 'A' && ch <= 'Z'))
         {
            yylval.iVal = char.ToLower(ch) - 'a';
            return (int)Tokens.LETTER;
         }
         else
             switch (ch)
             {
                 case '.':
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
