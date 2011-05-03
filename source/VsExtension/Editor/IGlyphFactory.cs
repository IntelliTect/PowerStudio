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
using PowerStudio.VsExtension.Tagging;

#endregion

namespace PowerStudio.VsExtension.Editor
{
    public interface IGlyphFactory<in TToken> : IGlyphFactory
            where TToken : GlyphTag
    {
        UIElement CreateGlyph( IWpfTextViewLine line, TToken token );
    }
}