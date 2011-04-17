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
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

#endregion

namespace PowerStudio.VsExtension.Intellisense.Completion
{
    [Export( typeof (IVsTextViewCreationListener) )]
    [Name( "token completion handler" )]
    [ContentType( LanguageConfiguration.Name )]
    [TextViewRole( PredefinedTextViewRoles.Editable )]
    internal class CompletionHandlerProvider : IVsTextViewCreationListener
    {
        [Import]
        internal IVsEditorAdaptersFactoryService AdapterService;

        [Import]
        internal ICompletionBroker CompletionBroker { get; set; }

        [Import]
        internal SVsServiceProvider ServiceProvider { get; set; }

        #region Implementation of IVsTextViewCreationListener

        /// <summary>
        /// Called when a <see cref="T:Microsoft.VisualStudio.TextManager.Interop.IVsTextView"/> adapter has been created and initialized.
        /// </summary>
        /// <param name="textViewAdapter">The newly created and initialized text view
        ///             adapter.</param>
        public void VsTextViewCreated( IVsTextView textViewAdapter )
        {
            ITextView textView = AdapterService.GetWpfTextView( textViewAdapter );
            if ( textView == null )
            {
                return;
            }

            Func<CompletionCommandHandler> createCommandHandler =
                    delegate { return new CompletionCommandHandler( textViewAdapter, textView, this ); };
            textView.Properties.GetOrCreateSingletonProperty( createCommandHandler );
        }

        #endregion
    }
}