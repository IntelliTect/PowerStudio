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
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    [Export( typeof (EditorFormatDefinition) )]
    [Name( Name )]
    [UserVisible( true )]
    internal class HighlightWordFormatDefinition : MarkerFormatDefinition
    {
        public const string Name = "MarkerFormatDefinition/HighlightWordFormatDefinition";

        public HighlightWordFormatDefinition()
        {
            // TODO: Pull from language configuration
            BackgroundColor = Colors.LightBlue;
            ForegroundColor = Colors.DarkBlue;
            DisplayName = "Highlight Word";
            ZOrder = 5;
        }
    }
}