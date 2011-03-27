/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.Collections.Generic;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;
using PowerStudio.Language;

namespace PowerStudio.VsExtension.Parsing
{
	public static partial class Configuration
	{
		public const string Name = "PowerShell";

		static CommentInfo _CommentInfo;
		public static CommentInfo CommentInfo { get { return _CommentInfo; } }

		static Configuration()
		{
			_CommentInfo.BlockEnd = "*/";
			_CommentInfo.BlockStart = "/*";
			_CommentInfo.LineStart = "#";
			_CommentInfo.UseLineComments = true;

			// default colors - currently, these need to be declared
			CreateColor("Keyword", COLORINDEX.CI_BLUE, COLORINDEX.CI_USERTEXT_BK);
			CreateColor("Comment", COLORINDEX.CI_DARKGREEN, COLORINDEX.CI_USERTEXT_BK);
            var cast = CreateColor("Cast", COLORINDEX.CI_DARKGREEN, COLORINDEX.CI_USERTEXT_BK);
			var identifier = CreateColor("Identifier", COLORINDEX.CI_PURPLE, COLORINDEX.CI_USERTEXT_BK);
            var variable = CreateColor("Variable", COLORINDEX.CI_RED, COLORINDEX.CI_USERTEXT_BK);
			CreateColor("String", COLORINDEX.CI_MAGENTA, COLORINDEX.CI_USERTEXT_BK);
			CreateColor("Number", COLORINDEX.CI_SYSPLAINTEXT_FG, COLORINDEX.CI_USERTEXT_BK);
			CreateColor("Text", COLORINDEX.CI_SYSPLAINTEXT_FG, COLORINDEX.CI_USERTEXT_BK);
            var cmdlet = CreateColor("Cmdlet", COLORINDEX.CI_PURPLE, COLORINDEX.CI_USERTEXT_BK);

            var op = CreateColor("Operator", COLORINDEX.CI_DARKGRAY, COLORINDEX.CI_USERTEXT_BK);

			TokenColor error = CreateColor("Error", COLORINDEX.CI_RED, COLORINDEX.CI_USERTEXT_BK, false, true);

			//
			// map tokens to color classes
			//
            ColorToken((int)Tokens.BREAK, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.BEGIN, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            
            ColorToken((int)Tokens.CATCH, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.CONTINUE, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

            ColorToken((int)Tokens.DATA, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.DO, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.DYNAMICPARAM, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

            ColorToken((int)Tokens.ELSE, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.ELSEIF, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.END, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.EXIT, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

            ColorToken((int)Tokens.FILTER, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.FINALLY, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.FOR, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.FOREACH, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.FROM, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.FUNCTION, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

            ColorToken((int)Tokens.IF, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.IN, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

            ColorToken((int)Tokens.PARAM, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);
            ColorToken((int)Tokens.PROCESS, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

			ColorToken((int)Tokens.RETURN, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

            ColorToken((int)Tokens.SWITCH, TokenType.Literal, TokenColor.String, TokenTriggers.None);
            ColorToken((int)Tokens.THROW, TokenType.Literal, TokenColor.String, TokenTriggers.None);
            ColorToken((int)Tokens.TRAP, TokenType.Literal, TokenColor.String, TokenTriggers.None);
            ColorToken((int)Tokens.TRY, TokenType.Literal, TokenColor.String, TokenTriggers.None);

            ColorToken((int)Tokens.UNTIL, TokenType.Literal, TokenColor.String, TokenTriggers.None);

            ColorToken((int)Tokens.WHILE, TokenType.Keyword, TokenColor.Keyword, TokenTriggers.None);

            ColorToken((int)Tokens.NUMBER, TokenType.Literal, TokenColor.String, TokenTriggers.None);

            
            ColorToken((int)Tokens.CMDLET, TokenType.Identifier, cmdlet, TokenTriggers.None);

            ColorToken((int)Tokens.IDENTIFIER, TokenType.Identifier, identifier, TokenTriggers.None);

            ColorToken((int)Tokens.VARIABLE, TokenType.Identifier, variable, TokenTriggers.None);

            ColorToken((int)Tokens.LOAND, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.LOOR, TokenType.Operator, op, TokenTriggers.None);

            ColorToken((int)Tokens.COEQ, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.CONE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COGE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COGT, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COLT, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COLE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COIEQ, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COINE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COIGE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COIGT, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COILT, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COILE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCEQ, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCNE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCGE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCGT, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCLT, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCLE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COLIKE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.CONOTLIKE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COMATCH, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.CONOTMATCH, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COILIKE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COINOTLIKE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COIMATCH, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COINOTMATCH, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCLIKE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCNOTLIKE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCMATCH, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCNOTMATCH, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCONTAINS, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.CONOTCONTAINS, TokenType.Operator, op,TokenTriggers.None);
            ColorToken((int)Tokens.COICONTAINS, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COINOTCONTAINS, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCCONTAINS, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCNOTCONTAINS, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COISNOT, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COIS, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COAS, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COREPLACE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COIREPLACE, TokenType.Operator, op, TokenTriggers.None);
            ColorToken((int)Tokens.COCREPLACE, TokenType.Operator, op, TokenTriggers.None);

            ColorToken((int)Tokens.STRING, TokenType.Operator, TokenColor.String, TokenTriggers.None);

            ColorToken((int)'(', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);
			ColorToken((int)')', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);
			ColorToken((int)'{', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);
			ColorToken((int)'}', TokenType.Delimiter, TokenColor.Text, TokenTriggers.MatchBraces);

            ColorToken((int)Tokens.CAST, TokenType.Operator, cast, TokenTriggers.None);
            
			//// Extra token values internal to the scanner
			ColorToken((int)Tokens.LEX_ERROR, TokenType.Text, error, TokenTriggers.None);
			ColorToken((int)Tokens.COMMENT, TokenType.Text, TokenColor.Comment, TokenTriggers.None);

		}

        private static readonly List<IVsColorableItem> colorableItems = new List<IVsColorableItem>();

        private static readonly TokenDefinition defaultDefinition = new TokenDefinition(TokenType.Text,
                                                                                         TokenColor.Text,
                                                                                         TokenTriggers.None);

        private static readonly Dictionary<int, TokenDefinition> definitions = new Dictionary<int, TokenDefinition>();

        public static IList<IVsColorableItem> ColorableItems
        {
            get { return colorableItems; }
        }

        public static TokenColor CreateColor(string name, COLORINDEX foreground, COLORINDEX background)
        {
            return CreateColor(name, foreground, background, false, false);
        }

        public static TokenColor CreateColor(string name,
                                              COLORINDEX foreground,
                                              COLORINDEX background,
                                              bool bold,
                                              bool strikethrough)
        {
            colorableItems.Add(new ColorableItem(name, foreground, background, bold, strikethrough));
            return (TokenColor)colorableItems.Count;
        }

        public static void ColorToken(int token, TokenType type, TokenColor color, TokenTriggers trigger)
        {
            definitions[token] = new TokenDefinition(type, color, trigger);
        }

        public static TokenDefinition GetDefinition(int token)
        {
            TokenDefinition result;
            return definitions.TryGetValue(token, out result) ? result : defaultDefinition;
        }

        #region Nested type: TokenDefinition

        public struct TokenDefinition
        {
            public TokenColor TokenColor;
            public TokenTriggers TokenTriggers;
            public TokenType TokenType;

            public TokenDefinition(TokenType type, TokenColor color, TokenTriggers triggers)
            {
                TokenType = type;
                TokenColor = color;
                TokenTriggers = triggers;
            }
        }

        #endregion
	}
}