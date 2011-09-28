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
    public abstract class GlyphFactoryBase<TTokenTag, TToken> : IGlyphFactory<TTokenTag, TToken>
            where TTokenTag : GlyphTag<TToken>
    {
        #region IGlyphFactory<TTokenTag,TToken> Members

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
                 !( tag is TTokenTag ) )
            {
                return null;
            }
            var tokenTag = (TTokenTag) tag;
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
        public abstract UIElement CreateGlyph( IWpfTextViewLine line, TTokenTag token );

        #endregion
    }
}