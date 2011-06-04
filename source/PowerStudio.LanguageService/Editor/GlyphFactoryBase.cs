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
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageService.Editor
{
    public abstract class GlyphFactoryBase<TToken> : IGlyphFactory<TToken>
            where TToken : GlyphTag
    {
        /// <summary>
        ///   Generates a new glyph visual for the given line.
        /// </summary>
        /// <param name = "line">The line that this glyph will be placed on.</param>
        /// <param name = "tag">Information about the glyph for which the visual is being generated.</param>
        /// <returns>
        ///   The visual element for the given tag.
        /// </returns>
        public UIElement GenerateGlyph( IWpfTextViewLine line, IGlyphTag tag )
        {
            if ( tag == null ||
                 !( tag is TToken ) )
            {
                return null;
            }
            var tokenTag = (TToken) tag;
            return CreateGlyph( line, tokenTag );
        }

        /// <summary>
        ///   Generates a new glyph visual for the given line and token.
        /// </summary>
        /// <param name = "line">The line that this glyph will be placed on.</param>
        /// <param name = "token">Information about the token glyph for which the visual is being generated.</param>
        /// <returns>
        ///   The visual element for the given tag.
        /// </returns>
        public abstract UIElement CreateGlyph( IWpfTextViewLine line, TToken token );
    }
}