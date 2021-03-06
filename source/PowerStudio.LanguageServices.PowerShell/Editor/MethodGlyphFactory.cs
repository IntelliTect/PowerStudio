﻿#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Management.Automation;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Text.Formatting;
using PowerStudio.LanguageServices.Editor;
using PowerStudio.LanguageServices.PowerShell.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Editor
{
    internal class MethodGlyphFactory : GlyphFactoryBase<MethodTag, PSToken>
    {
        public override UIElement CreateGlyph( IWpfTextViewLine line, MethodTag token )
        {
            const double size = 15.0;
            var ellipse =
                    new Ellipse
                    {
                            Fill = Brushes.LightBlue,
                            StrokeThickness = 1,
                            Stroke = Brushes.DarkBlue,
                            Height = size,
                            Width = size,
                            Opacity = 0.44
                    };

            return ellipse;
        }
    }
}