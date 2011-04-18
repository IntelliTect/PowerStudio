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

namespace PowerStudio.VsExtension.Tagging
{
    public class BraceMatchingTag : TokenTag, ITextMarkerTag
    {
        public BraceMatchingTag( string type )
        {
            Type = type;
        }

        #region Implementation of ITextMarkerTag

        /// <summary>
        ///   Gets the type of adornment to use.
        /// </summary>
        public string Type { get; private set; }

        #endregion

        public BraceMatchingTag Match { get; set; }
    }
}