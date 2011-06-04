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
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.LanguageService.Tagging.Providers
{
    public abstract class TaggerProviderBase : ITaggerProvider
    {
        /// <summary>
        ///   Creates a tag provider for the specified buffer.
        /// </summary>
        /// <param name = "buffer">The <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />.</param>
        /// <typeparam name = "T">The type of the tag.</typeparam>
        public virtual ITagger<T> CreateTagger<T>( ITextBuffer buffer ) where T : ITag
        {
            return buffer.Properties.GetOrCreateSingletonProperty( GetFactory<T>( buffer ) );
        }

        protected abstract Func<ITagger<T>> GetFactory<T>( ITextBuffer buffer ) where T : ITag;
    }
}