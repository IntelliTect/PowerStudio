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
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public abstract class ViewTaggerProviderBase : IViewTaggerProvider
    {
        protected abstract Func<ITagger<T>> GetFactory<T>( ITextView textView, ITextBuffer buffer ) where T : ITag;

        #region Implementation of IViewTaggerProvider

        /// <summary>
        ///   Creates a tag provider for the specified view and buffer.
        /// </summary>
        /// <param name = "textView">The <see cref = "T:Microsoft.VisualStudio.Text.Editor.ITextView" />.</param>
        /// <param name = "buffer">The <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />.</param>
        /// <typeparam name = "T">The type of the tag.</typeparam>
        public ITagger<T> CreateTagger<T>( ITextView textView, ITextBuffer buffer ) where T : ITag
        {
            return buffer.Properties.GetOrCreateSingletonProperty( GetFactory<T>( textView, buffer ) );
        }

        #endregion
    }
}