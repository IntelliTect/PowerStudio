#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellClassificationTagger : ITagger<ClassificationTag>
    {
        public PowerShellClassificationTagger( ITextBuffer buffer,
                                               ITagAggregator<PowerShellTokenTag> aggregator,
                                               ITokenClassification tokenClassification)
        {
            Buffer = buffer;
            Aggregator = aggregator;
            TokenClassification = tokenClassification;
        }

        public ITextBuffer Buffer { get; set; }
        public ITagAggregator<PowerShellTokenTag> Aggregator { get; set; }
        public ITokenClassification TokenClassification { get; set; }

        #region Implementation of ITagger<out ClassificationTag>

        /// <summary>
        /// Gets all the tags that overlap the <paramref name="spans"/>.
        /// </summary>
        /// <param name="spans">The spans to visit.</param>
        /// <returns>
        /// A <see cref="T:Microsoft.VisualStudio.Text.Tagging.ITagSpan`1"/> for each tag.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Taggers are not required to return their tags in any specific order.
        /// </para>
        /// <para>
        /// The recommended way to implement this method is by using generators ("yield return"),
        ///             which allows lazy evaluation of the entire tagging stack.
        /// </para>
        /// </remarks>
        public IEnumerable<ITagSpan<ClassificationTag>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            foreach ( SnapshotSpan span in spans )
            {
                foreach ( var tagSpan in Aggregator.GetTags( span ) )
                {
                    NormalizedSnapshotSpanCollection tokenSpan = tagSpan.Span.GetSpans( span.Snapshot );

                    yield return
                            new TagSpan<ClassificationTag>( tokenSpan[0],
                                                            new ClassificationTag(
                                                                    TokenClassification[tagSpan.Tag.TokenType] ) )
                            ;
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