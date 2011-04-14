#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class ClassificationTagger : ITagger<ClassificationTag>
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
        {
            Buffer = buffer;
            Aggregator = aggregator;
            TokenClassification = tokenClassification;
        }

        public ITextBuffer Buffer { get; set; }
        public ITagAggregator<TokenTag> Aggregator { get; set; }
        public ITokenClassification TokenClassification { get; set; }

        #region Implementation of ITagger<out ClassificationTag>

        /// <summary>
        ///   Gets all the tags that overlap the <paramref name = "spans" />.
        /// </summary>
        /// <param name = "spans">The spans to visit.</param>
        /// <returns>
        ///   A <see cref = "T:Microsoft.VisualStudio.Text.Tagging.ITagSpan`1" /> for each tag.
        /// </returns>
        /// <remarks>
        ///   <para>
        ///     Taggers are not required to return their tags in any specific order.
        ///   </para>
        ///   <para>
        ///     The recommended way to implement this method is by using generators ("yield return"),
        ///     which allows lazy evaluation of the entire tagging stack.
        ///   </para>
        /// </remarks>
        public IEnumerable<ITagSpan<ClassificationTag>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            foreach ( SnapshotSpan span in spans )
            {
                foreach ( IMappingTagSpan<TokenTag> tagSpan in Aggregator.GetTags( span ) )
                {
                    NormalizedSnapshotSpanCollection tokenSpan = tagSpan.Span.GetSpans( span.Snapshot );

                    var classificationTag = new ClassificationTag( TokenClassification[tagSpan.Tag.TokenType] );
                    yield return new TagSpan<ClassificationTag>( tokenSpan[0], classificationTag );
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        #endregion
    }
}