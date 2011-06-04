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
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.LanguageService.Tagging.Tags
{
    public class BraceMatchingTag : TokenTag, ITextMarkerTag
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "BraceMatchingTag" /> class.
        /// </summary>
        public BraceMatchingTag()
                : this( PredefinedTextMarkerTags.BraceHighlight )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "BraceMatchingTag" /> class.
        /// </summary>
        /// <param name = "type">The type.</param>
        public BraceMatchingTag( string type )
        {
            if ( string.IsNullOrEmpty( type ) )
            {
                throw new ArgumentNullException( "type" );
            }
            Type = type;
        }

        /// <summary>
        ///   Gets or sets the matching tag for the span being represented.
        /// </summary>
        /// <remarks>
        ///   The Match property must be set so that pairs of group tags can 
        ///   be identified and queried for location information. In order to do
        ///   brace matching, we need to be able to see if either tag is under the
        ///   cursor and whether either tag is in the currently updating span.
        /// </remarks>
        /// <value>The match.</value>
        public BraceMatchingTag Match { get; set; }

        #region ITextMarkerTag Members

        /// <summary>
        ///   Gets the type of adornment to use.
        /// </summary>
        public string Type { get; private set; }

        #endregion
    }
}