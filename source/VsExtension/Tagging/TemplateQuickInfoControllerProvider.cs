using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace PowerStudio.VsExtension.Tagging
{
    [Export( typeof (IIntellisenseControllerProvider) )]
    [Name( "Template QuickInfo Controller" )]
    [ContentType( "text" )]
    internal class TemplateQuickInfoControllerProvider : IIntellisenseControllerProvider
    {
        #region Asset Imports

        [Import]
        internal IQuickInfoBroker QuickInfoBroker { get; set; }

        #endregion

        #region IIntellisenseControllerProvider Members

        public IIntellisenseController TryCreateIntellisenseController( ITextView textView,
                                                                        IList<ITextBuffer> subjectBuffers )
        {
            return new TemplateQuickInfoController( textView, subjectBuffers, this );
        }

        #endregion
    }
}