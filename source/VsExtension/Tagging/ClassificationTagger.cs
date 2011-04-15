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
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class ClassificationTagger : TaggerBase<TokenClassificationTag>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "ClassificationTagger" /> class.
        /// </summary>
        /// <param name = "buffer">The buffer.</param>
        /// <param name = "aggregator">The aggregator.</param>
        /// <param name = "tokenClassification">The token classification.</param>
        public ClassificationTagger( ITextBuffer buffer,
                                     ITagAggregator<TokenTag> aggregator,
                                     ITokenClassification tokenClassification )
                : base( buffer )
        {
            Aggregator = aggregator;
            TokenClassification = tokenClassification;
        }

        public ITagAggregator<TokenTag> Aggregator { get; set; }
        public ITokenClassification TokenClassification { get; set; }

        #region Implementation of ITagger<out ClassificationTag>

        protected override void ReParse()
        {
            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;
            IEnumerable<PSToken> tokens = GetTokens(newSnapshot, true);

            List<TokenClassificationTag> tags = ( from token in tokens
                                                  select new TokenClassificationTag( TokenClassification[token.Type] )
                                                          { TokenType = token.Type, Span = AsSnapshotSpan( newSnapshot, token ) } ).ToList();

            Snapshot = newSnapshot;
            Tags = tags.AsReadOnly();
            OnTagsChanged( new SnapshotSpanEventArgs( new SnapshotSpan( Snapshot, Span.FromBounds( 0, newSnapshot.Length ) ) ) );
        }

        #endregion
    }
}