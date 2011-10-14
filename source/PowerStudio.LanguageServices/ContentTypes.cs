#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.ComponentModel;

#endregion

namespace PowerStudio.LanguageServices
{
    public enum ContentTypes
    {
        /// <summary>
        ///   the basic content type. Parent of all other content types.
        /// </summary>
        [Description( "any" )]
        Any,
        /// <summary>
        ///   the basic type for non-projection content. Inherits from 'any'.
        /// </summary>
        [Description( "text" )]
        Text,
        /// <summary>
        ///   for non-code text. Inherits from 'text'.
        /// </summary>
        [Description( "plaintext" )]
        Plaintext,
        /// <summary>
        ///   for code of all kinds. Inherits from 'text'.
        /// </summary>
        [Description( "code" )]
        Code,
        /// <summary>
        ///   excludes the text from any kind of handling. Text of this content type will never have any extension applied to it.
        /// </summary>
        [Description( "inert" )]
        Inert,
        /// <summary>
        ///   for the contents of projection buffers. Inherits from 'any'.
        /// </summary>
        [Description( "projection" )]
        Projection,
        /// <summary>
        ///   for the contents of IntelliSense. Inherits from 'text'.
        /// </summary>
        [Description( "intellisense" )]
        Intellisense,
        /// <summary>
        ///   signature help. Inherits from 'intellisense'.
        /// </summary>
        [Description( "sighelp" )]
        SigHelp,
        /// <summary>
        ///   signature help documentation. Inherits from 'intellisense'.
        /// </summary>
        [Description( "sighelp-doc" )]
        SigHelpDoc
    }
}