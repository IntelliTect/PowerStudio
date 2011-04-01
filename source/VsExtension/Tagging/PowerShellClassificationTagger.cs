using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellClassificationTagger : ITagger<ClassificationTag>
    {
        public ITextBuffer Buffer { get; set; }
        public ITagAggregator<PowerShellTokenTag> Aggregator { get; set; }
        public IClassificationTypeRegistryService RegistryService { get; set; }

        public PowerShellClassificationTagger(ITextBuffer buffer, ITagAggregator<PowerShellTokenTag> aggregator, IClassificationTypeRegistryService registryService)
        {
            Buffer = buffer;
            Aggregator = aggregator;
            RegistryService = registryService;
        }

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
            foreach (IMappingTagSpan<PowerShellTokenTag> tagSpan in Aggregator.GetTags(spans))
            {
                
            }
            SnapshotSpan blah = new SnapshotSpan();
            yield return new TagSpan<ClassificationTag>( blah , null );
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged {add{}remove{}}

        #endregion
    }
}