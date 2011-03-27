%using Microsoft.VisualStudio.TextManager.Interop
%using PowerStudio.Resources
%namespace PowerStudio.Language
%valuetype LexValue
%partial

/* %expect 5 */


%union {
	public string str;
}


%{
	IErrorHandler errorHandler = null;
	
	public void SetHandler(IErrorHandler errorHandler) { this.errorHandler = errorHandler; }
	
	internal void HandleError(string message, LexLocation lexLocation)
	{
		errorHandler.AddError(message, lexLocation.StartLine, lexLocation.StartColumn, lexLocation.EndColumn - lexLocation.StartColumn);
	}
	
	internal TextSpan ToSpan(LexLocation lexLocation) { return TextSpan(lexLocation.StartLine, lexLocation.StartColumn, lexLocation.EndLine, lexLocation.EndColumn); }

	internal void Match(LexLocation lhs, LexLocation rhs) 
	{
		DefineMatch(ToSpan(lhs), ToSpan(rhs)); 
	}
%}


// keywords
%token BEGIN BREAK CATCH CONTINUE DATA DO DYNAMICPARAM ELSE ELSEIF END EXIT FILTER FINALLY
%token FOR FOREACH FROM FUNCTION IF IN PARAM PROCESS RETURN SWITCH THROW TRAP TRY UNTIL WHILE

// comprison operators
%token COEQ CONE COGE COGT COLT COLE COIEQ COINE COIGE COIGT COILT COILE COCEQ COCNE COCGE COCGT COCLT COCLE
%token COLIKE CONOTLIKE COMATCH CONOTMATCH COILIKE COINOTLIKE COIMATCH COINOTMATCH COCLIKE COCNOTLIKE COCMATCH COCNOTMATCH
%token COCONTAINS CONOTCONTAINS COICONTAINS COINOTCONTAINS COCCONTAINS COCNOTCONTAINS COISNOT COIS COAS
%token COREPLACE COIREPLACE COCREPLACE

// logical operators
%token LOAND LOOR

// Bitwise operators
%token BOBAND BOBOR

%token PLUSEQ MINUSEQ TIMESEQ DIVEQ MODEQ

%token IDENTIFIER NUMBER CMDLET DOLLAR VARIABLE

  // %token ',' ';' '(' ')' '{' '}' '=' 
  // %token '+' '-' '*' '/' '!' '&' '|' '^'

// binary operators
%token DOT COLON MINUS PLUS DIV MULT EXP MOD

%token AMPAMP BARBAR STRING CAST
%token LEX_WHITE COMMENT LEX_ERROR STATEMENTSEPARATOR ASSIGNMENTOPERATOR

// this declaration must be after all other tokens
%token maxParseToken

%left '*' '/'
%left '+' '-'
%%

Program
	: /* empty */
	;

%%



