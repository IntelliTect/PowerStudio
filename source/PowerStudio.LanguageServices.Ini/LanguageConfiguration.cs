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
using Microsoft.VisualStudio.Utilities;

#endregion

namespace PowerStudio.LanguageServices.Ini
{
    public static class LanguageConfiguration
    {
        public const string Name = "ini";

        public const string BaseDefinitionName = "text";

        [Export]
        [Name( Name )]
        [BaseDefinition( BaseDefinitionName )]
        internal static ContentTypeDefinition IniContentTypeDefinition;

        [Export]
        [FileExtension( ".ini" )]
        [ContentType( Name )]
        internal static FileExtensionToContentTypeDefinition IniFileExtensionDefinition;
    }
}