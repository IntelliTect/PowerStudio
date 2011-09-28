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
using PowerStudio.LanguageServices.Tagging;
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
{
    public class ClassificationTagger : TaggerBase<TokenClassificationTag<PSToken>, PSToken>
    {
        public ClassificationTagger( ITextBuffer buffer,
                                     ITokenClassifier<PSToken> tokenClassifier )
                : base( buffer )
        {
            TokenClassifier = tokenClassifier;
            Parse();
        }

        public ITokenClassifier<PSToken> TokenClassifier { get; set; }

        protected override List<TokenClassificationTag<PSToken>> GetTags( ITextSnapshot snapshot )
        {
            IEnumerable<PSToken> tokens = GetTokens( snapshot, true );

            List<TokenClassificationTag<PSToken>> tags = ( from token in tokens
                                                           let tokenClassifier = TokenClassifier[token]
                                                           select new TokenClassificationTag<PSToken>( tokenClassifier )
                                                                  {
                                                                          Token = token,
                                                                          Span = CreateSnapshotSpan( snapshot, token )
                                                                  } )
                    .ToList();

            return tags;
        }
    }
}