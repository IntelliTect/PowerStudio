#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.LanguageServices.Tagging.Providers
{
    public abstract class ViewTaggerProviderBase : IViewTaggerProvider
    {
        #region IViewTaggerProvider Members

        /// <summary>
        ///   Creates a tag provider for the specified view and buffer.
        /// </summary>
        /// <param name = "textView">The <see cref = "T:Microsoft.VisualStudio.Text.Editor.ITextView" />.</param>
        /// <param name = "buffer">The <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />.</param>
        /// <typeparam name = "T">The type of the tag.</typeparam>
        ITagger<T> IViewTaggerProvider.CreateTagger<T>( ITextView textView, ITextBuffer buffer )
        {
            if ( !IsTopBuffer( textView, buffer ) )
            {
                return null;
            }
            return GetTagger<T>( textView, buffer );
        }

        #endregion

        protected abstract ITagger<T> GetTagger<T>( ITextView textView, ITextBuffer buffer ) where T : ITag;

        /// <summary>
        /// Determines whether the buffer is the top buffer for the specified text view.
        /// </summary>
        /// <param name="textView">The text view.</param>
        /// <param name="buffer">The buffer.</param>
        /// <returns>
        ///   <c>true</c> if the buffer is the top buffer for the specified text view; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsTopBuffer( ITextView textView, ITextBuffer buffer )
        {
            return textView.TextBuffer == buffer;
        }
    }
}