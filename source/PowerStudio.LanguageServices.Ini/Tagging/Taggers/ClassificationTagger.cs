#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text;
using PowerStudio.LanguageServices.Tagging;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Ini.Tagging.Taggers
{
    public class ClassificationTagger : IniTokenTagger<TokenClassificationTag<IniToken>>
    {
        public ClassificationTagger( ITextBuffer buffer,
                                     ITokenClassifier<IniToken> tokenClassifier )
                : base( buffer )
        {
            TokenClassifier = tokenClassifier;
            Parse();
        }

        public ITokenClassifier<IniToken> TokenClassifier { get; set; }

        protected override List<TokenClassificationTag<IniToken>> GetTags( ITextSnapshot snapshot )
        {
            IEnumerable<IniToken> tokens = GetTokens( snapshot, true );

            List<TokenClassificationTag<IniToken>> tags
                    = ( from token in tokens
                        let classificationType = TokenClassifier[token]
                        select new TokenClassificationTag<IniToken>( classificationType )
                               {
                                       Token = token,
                                       Span = CreateSnapshotSpan( snapshot, token )
                               } )
                            .ToList();

            return tags;
        }
    }
}