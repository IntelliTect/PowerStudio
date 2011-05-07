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
    internal class CompletionHandlerProvider : TextViewCreationListenerBase<CompletionCommandHandler>
    {
        protected override Func<CompletionCommandHandler> GetFactory( IVsTextView textViewAdapter,
                                                                      IWpfTextView wpfTextView )
        {
            return () => new CompletionCommandHandler( textViewAdapter, wpfTextView, this );
        }
    }
}