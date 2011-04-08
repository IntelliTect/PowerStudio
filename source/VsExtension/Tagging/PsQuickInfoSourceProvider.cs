using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Parsing;

namespace PowerStudio.VsExtension.Tagging
{
    [Export( typeof (IQuickInfoSourceProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [Name( LanguageConfiguration.Name + "QuickInfo" )]
    public class PsQuickInfoSourceProvider : IQuickInfoSourceProvider
    {
        [Import]
        private IBufferTagAggregatorFactoryService aggService;

        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        #region IQuickInfoSourceProvider Members

        public IQuickInfoSource TryCreateQuickInfoSource( ITextBuffer textBuffer )
        {
            return new PsQuickInfoSource( textBuffer, aggService.CreateTagAggregator<ErrorTag>( textBuffer ), this );
        }

        #endregion
    }
}