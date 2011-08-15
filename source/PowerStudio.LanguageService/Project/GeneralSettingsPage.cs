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
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using PowerStudio.Resources;

#endregion

namespace PowerStudio.LanguageService.Project
{
    [ClassInterface( ClassInterfaceType.AutoDual )]
    [ComVisible( true )]
    [Guid( PsConstants.GeneralSettingsPage )]
    public class GeneralSettingsPage : DialogPage
    {
        public GeneralSettingsPage()
        {
            InitialTabSize = 4;
            NormalTabSize = 4;
        }

        [Category( LanguageConfiguration.Name )]
        [DisplayName( "Initial Tab Size" )]
        [Description( "The size of the initial tab in spaces." )]
        public int InitialTabSize { get; set; }

        [Category( LanguageConfiguration.Name )]
        [DisplayName( "Normal Tab Size" )]
        [Description( "The size of the tabs in spaces following the initial tab." )]
        public int NormalTabSize { get; set; }
    }
}