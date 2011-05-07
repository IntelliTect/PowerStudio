#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging.Tags
{
    public class HighlightWordTag : TokenTag, ITextMarkerTag
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "HighlightWordTag" /> class.
        /// </summary>
        public HighlightWordTag()
        {
            Type = HighlightWordFormatDefinition.Name;
        }

        /// <summary>
        ///   Gets the type of adornment to use.
        /// </summary>
        public string Type { get; private set; }
    }
}