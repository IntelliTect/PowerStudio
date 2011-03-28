// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.4.4
// Machine:  WIN-O4E9B4BK3TB
// DateTime: 3/27/2011 3:33:31 PM
// UserName: idavis
// Input file <parser.y - 3/27/2011 3:33:26 PM>

// options: babel lines diagnose & report gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using Microsoft.VisualStudio.TextManager.Interop;
using PowerStudio.Resources;

namespace PowerStudio.Language
{
public enum Tokens {error=126,
    EOF=127,BEGIN=128,BREAK=129,CATCH=130,CONTINUE=131,DATA=132,
    DO=133,DYNAMICPARAM=134,ELSE=135,ELSEIF=136,END=137,EXIT=138,
    FILTER=139,FINALLY=140,FOR=141,FOREACH=142,FROM=143,FUNCTION=144,
    IF=145,IN=146,PARAM=147,PROCESS=148,RETURN=149,SWITCH=150,
    THROW=151,TRAP=152,TRY=153,UNTIL=154,WHILE=155,COEQ=156,
    CONE=157,COGE=158,COGT=159,COLT=160,COLE=161,COIEQ=162,
    COINE=163,COIGE=164,COIGT=165,COILT=166,COILE=167,COCEQ=168,
    COCNE=169,COCGE=170,COCGT=171,COCLT=172,COCLE=173,COLIKE=174,
    CONOTLIKE=175,COMATCH=176,CONOTMATCH=177,COILIKE=178,COINOTLIKE=179,COIMATCH=180,
    COINOTMATCH=181,COCLIKE=182,COCNOTLIKE=183,COCMATCH=184,COCNOTMATCH=185,COCONTAINS=186,
    CONOTCONTAINS=187,COICONTAINS=188,COINOTCONTAINS=189,COCCONTAINS=190,COCNOTCONTAINS=191,COISNOT=192,
    COIS=193,COAS=194,COREPLACE=195,COIREPLACE=196,COCREPLACE=197,LOAND=198,
    LOOR=199,BOBAND=200,BOBOR=201,PLUSEQ=202,MINUSEQ=203,TIMESEQ=204,
    DIVEQ=205,MODEQ=206,IDENTIFIER=207,NUMBER=208,CMDLET=209,DOLLAR=210,
    VARIABLE=211,DOT=212,COLON=213,MINUS=214,PLUS=215,DIV=216,
    MULT=217,EXP=218,MOD=219,AMPAMP=220,BARBAR=221,STRING=222,
    CAST=223,LEX_WHITE=224,COMMENT=225,LEX_ERROR=226,STATEMENTSEPARATOR=227,ASSIGNMENTOPERATOR=228,
    maxParseToken=229};

public partial struct LexValue
#line 10 "parser.y"
{
#line 11 "parser.y"
	public string str;
#line 12 "parser.y"
}
// Abstract base class for GPLEX scanners
public abstract class ScanBase : AbstractScanner<LexValue,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }

  protected abstract int CurrentSc { get; set; }
  //
  // Override the virtual EolState property if the scanner state is more
  // complicated then a simple copy of the current start state ordinal
  //
  public virtual int EolState { get { return CurrentSc; } set { CurrentSc = value; } }
}

// Interface class for 'colorizing' scanners
public interface IColorScan {
  void SetSource(string source, int offset);
  int GetNext(ref int state, out int start, out int end);
}

public partial class Parser: ShiftReduceParser<LexValue, LexLocation>
{
  // Verbatim content from parser.y - 3/27/2011 3:33:26 PM
#line 16 "parser.y"
	IErrorHandler errorHandler = null;
#line 17 "parser.y"
	
#line 18 "parser.y"
	public void SetHandler(IErrorHandler errorHandler) { this.errorHandler = errorHandler; }
#line 19 "parser.y"
	
#line 20 "parser.y"
	internal void HandleError(string message, LexLocation lexLocation)
#line 21 "parser.y"
	{
#line 22 "parser.y"
		errorHandler.AddError(message, lexLocation.StartLine, lexLocation.StartColumn, lexLocation.EndColumn - lexLocation.StartColumn);
#line 23 "parser.y"
	}
#line 24 "parser.y"
	
#line 25 "parser.y"
	internal TextSpan ToSpan(LexLocation lexLocation) { return TextSpan(lexLocation.StartLine, lexLocation.StartColumn, lexLocation.EndLine, lexLocation.EndColumn); }
#line 26 "parser.y"

#line 27 "parser.y"
	internal void Match(LexLocation lhs, LexLocation rhs) 
#line 28 "parser.y"
	{
#line 29 "parser.y"
		DefineMatch(ToSpan(lhs), ToSpan(rhs)); 
#line 30 "parser.y"
	}
  // End verbatim content from parser.y - 3/27/2011 3:33:26 PM

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[25];
  private static State[] states = new State[37];
  private static string[] nonTerms = new string[] {
      "Program", "$accept", "statementListRule", "statementRule", "statementRule_", 
      "ParenExprAlways", "statementBlockRule", "ifSuffixRule", "OpenBlock", "CloseBlock", 
      "ParenExpr", "Expr", "BoolOp", };

  static Parser() {
    states[0] = new State(new int[]{145,8,123,26,127,-3},new int[]{-1,1,-3,3,-4,4,-5,7,-7,25,-9,17});
    states[1] = new State(new int[]{127,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{227,5});
    states[5] = new State(new int[]{145,8,123,26,127,-5,126,-5,125,-5},new int[]{-3,6,-4,4,-5,7,-7,25,-9,17});
    states[6] = new State(-4);
    states[7] = new State(-6);
    states[8] = new State(new int[]{40,28,126,35},new int[]{-6,9,-11,27});
    states[9] = new State(new int[]{123,26},new int[]{-7,10,-9,17});
    states[10] = new State(new int[]{136,12,135,15},new int[]{-8,11});
    states[11] = new State(-7);
    states[12] = new State(new int[]{123,26},new int[]{-7,13,-9,17});
    states[13] = new State(new int[]{136,12,135,15},new int[]{-8,14});
    states[14] = new State(-9);
    states[15] = new State(new int[]{123,26},new int[]{-7,16,-9,17});
    states[16] = new State(-10);
    states[17] = new State(new int[]{126,23,125,22,145,8,123,26},new int[]{-10,18,-3,19,-4,4,-5,7,-7,25,-9,17});
    states[18] = new State(-11);
    states[19] = new State(new int[]{126,21,125,22},new int[]{-10,20});
    states[20] = new State(-12);
    states[21] = new State(-13);
    states[22] = new State(-16);
    states[23] = new State(new int[]{125,22},new int[]{-10,24});
    states[24] = new State(-14);
    states[25] = new State(-8);
    states[26] = new State(-15);
    states[27] = new State(-17);
    states[28] = new State(new int[]{220,33,221,34},new int[]{-12,29,-13,32});
    states[29] = new State(new int[]{41,30,126,31});
    states[30] = new State(-20);
    states[31] = new State(-21);
    states[32] = new State(-22);
    states[33] = new State(-23);
    states[34] = new State(-24);
    states[35] = new State(new int[]{41,36,123,-19});
    states[36] = new State(-18);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,127});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-1, new int[]{});
    rules[4] = new Rule(-3, new int[]{-4,227,-3});
    rules[5] = new Rule(-3, new int[]{});
    rules[6] = new Rule(-4, new int[]{-5});
    rules[7] = new Rule(-5, new int[]{145,-6,-7,-8});
    rules[8] = new Rule(-5, new int[]{-7});
    rules[9] = new Rule(-8, new int[]{136,-7,-8});
    rules[10] = new Rule(-8, new int[]{135,-7});
    rules[11] = new Rule(-7, new int[]{-9,-10});
    rules[12] = new Rule(-7, new int[]{-9,-3,-10});
    rules[13] = new Rule(-7, new int[]{-9,-3,126});
    rules[14] = new Rule(-7, new int[]{-9,126,-10});
    rules[15] = new Rule(-9, new int[]{123});
    rules[16] = new Rule(-10, new int[]{125});
    rules[17] = new Rule(-6, new int[]{-11});
    rules[18] = new Rule(-6, new int[]{126,41});
    rules[19] = new Rule(-6, new int[]{126});
    rules[20] = new Rule(-11, new int[]{40,-12,41});
    rules[21] = new Rule(-11, new int[]{40,-12,126});
    rules[22] = new Rule(-12, new int[]{-13});
    rules[23] = new Rule(-13, new int[]{220});
    rules[24] = new Rule(-13, new int[]{221});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
      case 11: // statementBlockRule -> OpenBlock, CloseBlock
#line 95 "parser.y"
{ Match(LocationStack[LocationStack.Depth-2], LocationStack[LocationStack.Depth-1]); }
        break;
      case 12: // statementBlockRule -> OpenBlock, statementListRule, CloseBlock
#line 97 "parser.y"
{ Match(LocationStack[LocationStack.Depth-3], LocationStack[LocationStack.Depth-1]); }
        break;
      case 13: // statementBlockRule -> OpenBlock, statementListRule, error
#line 99 "parser.y"
{ HandleError("missing '}'", LocationStack[LocationStack.Depth-1]); }
        break;
      case 14: // statementBlockRule -> OpenBlock, error, CloseBlock
#line 101 "parser.y"
{ Match(LocationStack[LocationStack.Depth-3], LocationStack[LocationStack.Depth-1]); }
        break;
      case 15: // OpenBlock -> '{'
#line 105 "parser.y"
{ /*  */ }
        break;
      case 16: // CloseBlock -> '}'
#line 109 "parser.y"
{ /*  */ }
        break;
      case 18: // ParenExprAlways -> error, ')'
#line 114 "parser.y"
{ HandleError("error in expr", LocationStack[LocationStack.Depth-2]); }
        break;
      case 19: // ParenExprAlways -> error
#line 115 "parser.y"
{ HandleError("error in expr", LocationStack[LocationStack.Depth-1]); }
        break;
      case 20: // ParenExpr -> '(', Expr, ')'
#line 119 "parser.y"
{ Match(LocationStack[LocationStack.Depth-3], LocationStack[LocationStack.Depth-1]); }
        break;
      case 21: // ParenExpr -> '(', Expr, error
#line 120 "parser.y"
{ HandleError("unmatched parentheses", LocationStack[LocationStack.Depth-1]); }
        break;
    }
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 132 "parser.y"

#line 133 "parser.y"

#line 134 "parser.y"

}
}
