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

#endregion

namespace PowerStudio.VsExtension.Intellisense.Completion
{
    public abstract class TextViewCreationListenerBase<T> : IVsTextViewCreationListener
            where T : class
    {
        [Import]
        internal IVsEditorAdaptersFactoryService AdapterService { get; set; }

        [Import]
        internal ICompletionBroker CompletionBroker { get; set; }

        [Import]
        internal SVsServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Called when a <see cref="T:Microsoft.VisualStudio.TextManager.Interop.IVsTextView"/> adapter has been created and initialized.
        /// </summary>
        /// <param name="textViewAdapter">The newly created and initialized text view
        ///             adapter.</param>
        public void VsTextViewCreated( IVsTextView textViewAdapter )
        {
            IWpfTextView wpfTextView = AdapterService.GetWpfTextView( textViewAdapter );
            if ( wpfTextView == null )
            {
                return;
            }
            wpfTextView.Properties.GetOrCreateSingletonProperty( GetFactory( textViewAdapter, wpfTextView ) );
        }

        protected abstract Func<T> GetFactory( IVsTextView textViewAdapter, IWpfTextView wpfTextView );
    }
}