#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Windows;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Editor
{
    public interface IGlyphFactory<in TToken> : IGlyphFactory
            where TToken : GlyphTag
    {
        /// <summary>
        ///   Generates a new glyph visual for the given line and token.
        /// </summary>
        /// <param name = "line">The line that this glyph will be placed on.</param>
        /// <param name = "token">Information about the token glyph for which the visual is being generated.</param>
        /// <returns>
        ///   The visual element for the given tag.
        /// </returns>
        UIElement CreateGlyph( IWpfTextViewLine line, TToken token );
    }
}