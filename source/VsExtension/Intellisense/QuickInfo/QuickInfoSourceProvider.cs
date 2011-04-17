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
using Microsoft.VisualStudio.Text.Tagging;
using PowerStudio.VsExtension.Tagging;

#endregion

namespace PowerStudio.VsExtension.Intellisense.QuickInfo
{
    public abstract class QuickInfoSourceProvider<T> : IQuickInfoSourceProvider
            where T : ITokenTag
    {
        [Import]
        protected IBufferTagAggregatorFactoryService TagAggregatorFactory { get; set; }

        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        #region IQuickInfoSourceProvider Members

        /// <summary>
        ///   Creates a Quick Info provider for the specified context.
        /// </summary>
        /// <param name = "textBuffer">The text buffer for which to create a provider.</param>
        /// <returns>
        ///   A valid <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSource" /> instance, or null if none could be created.
        /// </returns>
        public abstract IQuickInfoSource TryCreateQuickInfoSource( ITextBuffer textBuffer );

        #endregion
    }
}