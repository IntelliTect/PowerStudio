using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Parsing;

namespace PowerStudio.VsExtension.Tagging
{
    [Export(typeof(ITaggerProvider))]
    [ContentType(Configuration.Name)]
    [TagType(typeof(ClassificationTag))]
    public class PowerShellClassificationTaggerProvider : ITaggerProvider
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry;

        [Import]
        internal IBufferTagAggregatorFactoryService AggregatorFactory;

        #region Implementation of ITaggerProvider
        
        public ITagger<T> CreateTagger<T>( ITextBuffer buffer ) where T : ITag
        {
            ITagAggregator<PowerShellTokenTag> tagAggregator =
                    AggregatorFactory.CreateTagAggregator<PowerShellTokenTag>( buffer );
            return (ITagger<T>) new PowerShellClassificationTagger( buffer, tagAggregator, ClassificationTypeRegistry );
        }

        #endregion
    }
}
