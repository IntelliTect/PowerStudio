#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

#endregion

namespace PowerStudio.LanguageServices.Intellisense.Completion
{
    public abstract class CompletionSourceProvider : ICompletionSourceProvider
    {
        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        #region ICompletionSourceProvider Members

        /// <summary>
        ///   Creates a completion provider for the given context.
        /// </summary>
        /// <param name = "textBuffer">The text buffer over which to create a provider.</param>
        /// <returns>
        ///   A valid <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSource" /> instance, or null if none could be created.
        /// </returns>
        public abstract ICompletionSource TryCreateCompletionSource( ITextBuffer textBuffer );

        #endregion
    }
}