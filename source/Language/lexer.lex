%namespace PowerStudio.Language
%using QUT.Gppg;

%s IN_IDENT_DOLLAR
%s IN_INNER_IDENT_BLOCK
%x COMMENT

%{
        int GetIdToken(string txt)
        {
            txt = txt.ToUpperInvariant();
            switch (txt[0])
            {
			    case '-':
					txt = txt.Substring(1);
					if (txt.Equals("BAND")) return (int)Tokens.BOBAND;
					if (txt.Equals("BOR")) return (int)Tokens.BOBOR;
					if (txt.Equals("AND")) return (int)Tokens.LOAND;
					if (txt.Equals("OR")) return (int)Tokens.LOOR;
			        if (txt.Equals("EQ")) return (int)Tokens.COEQ;
					if (txt.Equals("NE")) return (int)Tokens.CONE;
					if (txt.Equals("GE")) return (int)Tokens.COGE;
					if (txt.Equals("GT")) return (int)Tokens.COGT;
					if (txt.Equals("LT")) return (int)Tokens.COLT;
					if (txt.Equals("LE")) return (int)Tokens.COLE;
					if (txt.Equals("IEQ")) return (int)Tokens.COIEQ;
					if (txt.Equals("INE")) return (int)Tokens.COINE;
					if (txt.Equals("IGE")) return (int)Tokens.COIGE;
					if (txt.Equals("IGT")) return (int)Tokens.COIGT;
					if (txt.Equals("ILT")) return (int)Tokens.COILT;
					if (txt.Equals("ILE")) return (int)Tokens.COILE;
					if (txt.Equals("CEQ")) return (int)Tokens.COCEQ;
					if (txt.Equals("CNE")) return (int)Tokens.COCNE;
					if (txt.Equals("CGE")) return (int)Tokens.COCGE;
					if (txt.Equals("CGT")) return (int)Tokens.COCGT;
					if (txt.Equals("CLT")) return (int)Tokens.COCLT;
					if (txt.Equals("CLE")) return (int)Tokens.COCLE;
					if (txt.Equals("LIKE")) return (int)Tokens.COLIKE;
					if (txt.Equals("NOTLIKE")) return (int)Tokens.CONOTLIKE;
					if (txt.Equals("MATCH")) return (int)Tokens.COMATCH;
					if (txt.Equals("NOTMATCH")) return (int)Tokens.CONOTMATCH;
					if (txt.Equals("ILIKE")) return (int)Tokens.COILIKE;
					if (txt.Equals("INOTLIKE")) return (int)Tokens.COINOTLIKE;
					if (txt.Equals("IMATCH")) return (int)Tokens.COIMATCH;
					if (txt.Equals("INOTMATCH")) return (int)Tokens.COINOTMATCH;
					if (txt.Equals("CLIKE")) return (int)Tokens.COCLIKE;
					if (txt.Equals("CNOTLIKE")) return (int)Tokens.COCNOTLIKE;
					if (txt.Equals("CMATCH")) return (int)Tokens.COCMATCH;
					if (txt.Equals("CNOTMATCH")) return (int)Tokens.COCNOTMATCH;
					if (txt.Equals("CONTAINS")) return (int)Tokens.COCONTAINS;
					if (txt.Equals("NOTCONTAINS")) return (int)Tokens.CONOTCONTAINS;
					if (txt.Equals("ICONTAINS")) return (int)Tokens.COICONTAINS;
					if (txt.Equals("INOTCONTAINS")) return (int)Tokens.COINOTCONTAINS;
					if (txt.Equals("CCONTAINS")) return (int)Tokens.COCCONTAINS;
					if (txt.Equals("CNOTCONTAINS")) return (int)Tokens.COCNOTCONTAINS;
					if (txt.Equals("ISNOT")) return (int)Tokens.COISNOT;
					if (txt.Equals("IS")) return (int)Tokens.COIS;
					if (txt.Equals("AS")) return (int)Tokens.COAS;
					if (txt.Equals("REPLACE")) return (int)Tokens.COREPLACE;
					if (txt.Equals("IREPLACE")) return (int)Tokens.COIREPLACE;
					if (txt.Equals("CREPLACE")) return (int)Tokens.COCREPLACE;
                    break;
                case 'B':
                    if (txt.Equals("BEGIN")) return (int)Tokens.BEGIN;
                    if (txt.Equals("BREAK")) return (int)Tokens.BREAK;
                    break;
                case 'C':
                    if (txt.Equals("CATCH")) return (int)Tokens.CATCH;
                    if (txt.Equals("CONTINUE")) return (int)Tokens.CONTINUE;
					break;
                case 'D':
                    if (txt.Equals("DATA")) return (int)Tokens.DATA;
                    if (txt.Equals("DO")) return (int)Tokens.DO;
                    if (txt.Equals("DYNAMICPARAM")) return (int)Tokens.DYNAMICPARAM;
                    break;
                case 'E':
                    if (txt.Equals("ELSE")) return (int)Tokens.ELSE;
                    if (txt.Equals("ELSEIF")) return (int)Tokens.ELSEIF;
                    if (txt.Equals("END")) return (int)Tokens.END;
                    if (txt.Equals("EXIT")) return (int)Tokens.EXIT;
                    break;
                case 'F':
                    if (txt.Equals("FILTER")) return (int)Tokens.FILTER;
                    if (txt.Equals("FINALLY")) return (int)Tokens.FINALLY;
                    if (txt.Equals("FOR")) return (int)Tokens.FOR;
                    if (txt.Equals("FOREACH")) return (int)Tokens.FOREACH;
                    if (txt.Equals("FROM")) return (int)Tokens.FROM;
                    if (txt.Equals("FUNCTION")) return (int)Tokens.FUNCTION;
                    break;
                case 'I':
                    if (txt.Equals("IF")) return (int)Tokens.IF;
                    if (txt.Equals("IN")) return (int)Tokens.IN;
                    break;
                case 'P':
                    if (txt.Equals("PARAM")) return (int)Tokens.PARAM;
					if (txt.Equals("PROCESS")) return (int)Tokens.PROCESS;
                    break;
                 case 'R':
                    if (txt.Equals("RETURN")) return (int)Tokens.RETURN;
                    break;
                case 'S':
                    if (txt.Equals("SWITCH")) return (int)Tokens.SWITCH;
                    break;
                case 'T':
                    if (txt.Equals("THROW")) return (int)Tokens.THROW;
                    if (txt.Equals("TRAP")) return (int)Tokens.TRAP;
                    if (txt.Equals("TRY")) return (int)Tokens.TRY;
                    break;
                case 'U':
                    if (txt.Equals("UNTIL")) return (int)Tokens.UNTIL;
                    break;
                case 'W':
                    if (txt.Equals("WHILE")) return (int)Tokens.WHILE;
                    break;
                default: 
                    break;
            }
            return (int)Tokens.IDENTIFIER;
       }
       
       internal void LoadYylval()
       {
           yylval.str = tokTxt;
           yylloc = new LexLocation(tokLin, tokCol, tokLin, tokECol);
       }
       /*
       public override void yyerror(string s, params object[] a)
       {
           if (handler != null) handler.AddError(s, tokLin, tokCol, tokLin, tokECol);
       }*/
%}

Any [\r\n\u0085\u2028\u2029]
White0          [ \t\f\v]
White           {White0}

IDENT_INCURLY [^\{\}\r\n]+\}
SIMPLEIDENT [[:IsLetter:]]([[:IsLetterOrDigit:]]|[_])*
VARIABLE ((script|global|private)[\:])?{SIMPLEIDENT}
ParameterToken \-[[:IsLetter:]]+VARIABLE?
CmdletNameToken [^$0-9(@"'][^ \t]*
Cast \[{SIMPLEIDENT}\]

CmntStart    \/\*
CmntEnd      \*\/
ABStar       [^\*\n]*

SINGLE_QUOTED_STRING_BEGIN [\']
SINGLE_QUOTED_STRING_CONTENT ([^\\\'\r\n])*
SINGLE_QUOTED_STRING {SINGLE_QUOTED_STRING_BEGIN}{SINGLE_QUOTED_STRING_CONTENT}[\']

QUOTED_STRING_BEGIN [\"]
QUOTED_STRING_CONTENT ([^\\\"\r\n])*
QUOTED_STRING {QUOTED_STRING_BEGIN}{QUOTED_STRING_CONTENT}[\"]
AMPAMP [\&\&]
BARBAR [\|\|]
NL [\r\n]
STATEMENTSEPARATOR [;|{AMPAMP}|{BARBAR}|{NL|\r|\n}]

%%

{Cast}            { return (int)Tokens.CAST; }
[#][^{NL}]*       { return (int)Tokens.COMMENT; }
{CmntStart}{ABStar}\**{CmntEnd} { return (int)Tokens.COMMENT; } 
{CmntStart}{ABStar}\**          { BEGIN(COMMENT); return (int)Tokens.COMMENT; }
<COMMENT>\n                     |                                
<COMMENT>{ABStar}\**            { return (int)Tokens.COMMENT; }                                
<COMMENT>{ABStar}\**{CmntEnd}   { BEGIN(INITIAL); return (int)Tokens.COMMENT; }
[$]{VARIABLE}    {  return (int)Tokens.VARIABLE; }



{SINGLE_QUOTED_STRING}    { return (int)Tokens.STRING; }
{QUOTED_STRING}    { return (int)Tokens.STRING; }


[a-zA-Z_][a-zA-Z0-9_]*    { return GetIdToken(yytext); }
[0-9]+                    { return (int)Tokens.NUMBER; }
\-[a-zA-Z]+                { return GetIdToken(yytext); }
,                         { return (int)',';    }
\(                        { return (int)'(';    }
\)                        { return (int)')';    }
\{                        { return (int)'{';    }
\}                        { return (int)'}';    }
\=                         { return (int)Tokens.ASSIGNMENTOPERATOR;  }
\^                        { return (int)'^';    }
\+                        { return (int)'+';    }
\-                        { return (int)'-';    }
\*                        { return (int)'*';    }
\/                        { return (int)'/';    }
\%                        { return (int)'%';    }
\!                        { return (int)'!';    }
\+=                        { return (int)Tokens.ASSIGNMENTOPERATOR;  }
\-=                        { return (int)Tokens.ASSIGNMENTOPERATOR;  }
\*=                        { return (int)Tokens.ASSIGNMENTOPERATOR;  }
\/=                        { return (int)Tokens.ASSIGNMENTOPERATOR;  }
\%=                        { return (int)Tokens.ASSIGNMENTOPERATOR;  }
\&                        { return (int)'&';    }
\&\&                      { return (int)Tokens.AMPAMP; }
\|                        { return (int)'|';    }
\|\|                      { return (int)Tokens.BARBAR; }
\.                        { return (int)'.';    }

{STATEMENTSEPARATOR}    { return (int)Tokens.STATEMENTSEPARATOR; }

{White0}+                  { return (int)Tokens.LEX_WHITE; }

.                          { yyerror("illegal char");
                             return (int)Tokens.LEX_ERROR; }

%{
                      LoadYylval();
%}

%%

/* .... */
