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
using Microsoft.VisualStudio.Text.Editor;
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Editor
{
    internal abstract class GlyphFactoryProviderBase<T> : IGlyphFactoryProvider
            where T : GlyphTag
    {
        /// <summary>
        ///   Gets the <see cref = "T:Microsoft.VisualStudio.Text.Editor.IGlyphFactory" /> for the given text view and margin.
        /// </summary>
        /// <param name = "view">The view for which the factory is being created.</param>
        /// <param name = "margin">The margin for which the factory will create glyphs.</param>
        /// <returns>
        ///   An <see cref = "T:Microsoft.VisualStudio.Text.Editor.IGlyphFactory" /> for the given view and margin.
        /// </returns>
        public IGlyphFactory GetGlyphFactory( IWpfTextView view, IWpfTextViewMargin margin )
        {
            return view.Properties.GetOrCreateSingletonProperty( GetFactory( view, margin ) );
        }

        protected abstract Func<IGlyphFactory<T>> GetFactory( IWpfTextView view, IWpfTextViewMargin margin );
    }
}