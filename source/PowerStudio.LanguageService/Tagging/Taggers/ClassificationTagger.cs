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
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using PowerStudio.LanguageService.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageService.Tagging.Taggers
{
    public class ClassificationTagger : TaggerBase<TokenClassificationTag>
    {
        public ClassificationTagger( ITextBuffer buffer,
                                     ITokenClassifier tokenClassifier )
                : base( buffer )
        {
            TokenClassifier = tokenClassifier;
            Parse();
        }

        public ITokenClassifier TokenClassifier { get; set; }

        protected override List<TokenClassificationTag> GetTags( ITextSnapshot snapshot )
        {
            IEnumerable<PSToken> tokens = GetTokens( snapshot, true );

            List<TokenClassificationTag> tags = ( from token in tokens
                                                  let tokenClassifier = TokenClassifier[token]
                                                  select new TokenClassificationTag( tokenClassifier )
                                                         {
                                                                 Token = token,
                                                                 Span = CreateSnapshotSpan( snapshot, token )
                                                         } )
                    .ToList();

            return tags;
        }
    }
}