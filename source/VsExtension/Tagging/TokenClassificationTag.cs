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
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    /// <summary>
    ///   An implementation of <see cref = "T:Microsoft.VisualStudio.Text.Tagging.IClassificationTag" />.
    /// </summary>
    public class TokenClassificationTag : TokenTag, IClassificationTag
    {
        /// <summary>
        ///   Create a new tag associated with the given type of classification.
        /// </summary>
        /// <param name = "type">The type of classification</param>
        /// <exception cref = "T:System.ArgumentNullException">If the type is passed in as null</exception>
        public TokenClassificationTag( IClassificationType type )
        {
            if ( ReferenceEquals( type, null ) )
            {
                throw new ArgumentNullException( "type" );
            }
            ClassificationType = type;
        }

        #region IClassificationTag Members

        /// <summary>
        ///   The classification type associated with this tag.
        /// </summary>
        public IClassificationType ClassificationType { get; private set; }

        #endregion
    }
}