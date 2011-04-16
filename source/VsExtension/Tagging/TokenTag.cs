#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Management.Automation;
using Microsoft.VisualStudio.Text;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class TokenTag : ITokenTag
    {
        private static readonly TokenClass[] _TokenClasses = new[]
                                                             {
                                                                     TokenClass.Other, TokenClass.FormalLanguage,
                                                                     TokenClass.Other, TokenClass.Other,
                                                                     TokenClass.NumberLiteral, TokenClass.StringLiteral,
                                                                     TokenClass.Identifier, TokenClass.Identifier,
                                                                     TokenClass.Literal, TokenClass.SymbolReference,
                                                                     TokenClass.SymbolReference, TokenClass.Operator,
                                                                     TokenClass.Operator, TokenClass.Operator,
                                                                     TokenClass.Keyword, TokenClass.Comment,
                                                                     TokenClass.Other, TokenClass.Other,
                                                                     TokenClass.Other, TokenClass.Operator
                                                             };

        #region ITokenTag Members

        public PSToken Token { get; set; }
        public SnapshotSpan Span { get; set; }

        public TokenClass Class
        {
            get { return GetTokenClass( Token.Type ); }
        }

        #endregion

        internal static TokenClass GetTokenClass( PSTokenType psTokenType )
        {
            var index = (int) psTokenType;
            if ( ( index >= 0 ) &&
                 ( index < _TokenClasses.Length ) )
            {
                return _TokenClasses[index];
            }
            return TokenClass.Other;
        }
    }
}