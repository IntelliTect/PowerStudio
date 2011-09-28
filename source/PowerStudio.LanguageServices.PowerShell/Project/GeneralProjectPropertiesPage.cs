#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using PowerStudio.Resources;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Project
{
    [ComVisible( true )]
    [Guid( PsConstants.GeneralProjectPropertiesPage )]
    public class GeneralProjectPropertiesPage : SettingsPage
    {
        public GeneralProjectPropertiesPage()
        {
            Name = "PowerShell Project Settings";
        }

        #region Overrides of SettingsPage

        protected override void BindProperties()
        {
        }

        protected override int ApplyChanges()
        {
            return VSConstants.S_OK;
        }

        public override string GetClassName()
        {
            return GetType().FullName;
        }

        #endregion
    }
}