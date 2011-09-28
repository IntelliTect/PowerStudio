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

namespace PowerStudio.LanguageServices.PowerShell
{
    public static class LanguageConfiguration
    {
        public const string Name = "PowerShell";

        public const string BaseDefinitionName = "code";

        public const int OutlineThreshold = 2; // TODO: make this a language setting.

        [Export]
        [Name( Name )]
        [BaseDefinition( BaseDefinitionName )]
        internal static ContentTypeDefinition PowerShellContentTypeDefinition;

        [Export]
        [FileExtension( ".ps1" )]
        [ContentType( Name )]
        internal static FileExtensionToContentTypeDefinition PowerShellFileExtensionDefinition;

        [Export]
        [FileExtension( ".psm1" )]
        [ContentType( Name )]
        internal static FileExtensionToContentTypeDefinition PowerShellModuleFileExtensionDefinition;

        [Export]
        [FileExtension( ".psc1" )]
        [ContentType( Name )]
        internal static FileExtensionToContentTypeDefinition PowerShellConsoleFileExtensionDefinition;

        [Export]
        [FileExtension( ".psd1" )]
        [ContentType( Name )]
        internal static FileExtensionToContentTypeDefinition PowerShellDataFileExtensionDefinition;

        [Export]
        [FileExtension( ".psm1xml" )]
        [ContentType( Name )]
        internal static FileExtensionToContentTypeDefinition PowerShellXmlFileExtensionDefinition;
    }
}