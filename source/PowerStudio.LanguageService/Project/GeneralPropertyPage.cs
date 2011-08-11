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
using Microsoft.VisualStudio.Shell.Interop;
using NLog;
using PowerStudio.Resources;

#endregion

namespace PowerStudio.LanguageService.Project
{
    [ComVisible( true )]
    [Guid( PsConstants.GeneralPropertyPageGuid )]
    public class GeneralPropertyPage : SettingsPage
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        //[ResourcesCategoryAttribute(Resources.AssemblyName)]
        //[LocDisplayName(Resources.AssemblyName)]
        //[ResourcesDescriptionAttribute(Resources.AssemblyNameDescription)]
        ///// <summary>
        ///// Gets or sets Assembly Name.
        ///// </summary>
        ///// <remarks>IsDirty flag was switched to true.</remarks>
        //public string AssemblyName
        //{
        //    get { return this.assemblyName; }
        //    set { this.assemblyName = value; this.IsDirty = true; }
        //}

        public override string GetClassName()
        {
            return GetType().FullName;
        }

        protected override void BindProperties()
        {
            if ( ProjectMgr == null )
            {
                return;
            }

            //assemblyName = this.ProjectMgr.GetProjectProperty("AssemblyName", true);
        }

        /// <summary>
        ///   Apply Changes on project node.
        /// </summary>
        /// <returns>E_INVALIDARG if internal ProjectMgr is null, otherwise applies changes and return S_OK.</returns>
        protected override int ApplyChanges()
        {
            if ( ProjectMgr == null )
            {
                return VSConstants.E_INVALIDARG;
            }

            var propertyPageFrame =
                    (IVsPropertyPageFrame) ProjectMgr.Site.GetService( ( typeof (SVsPropertyPageFrame) ) );
            //bool reloadRequired = this.ProjectMgr.TargetFrameworkMoniker != this.targetFrameworkMoniker;

            //ProjectMgr.SetProjectProperty( "AssemblyName", this.assemblyName );

            IsDirty = false;

            //if ( reloadRequired )
            {
                // This prevents the property page from displaying bad data from the zombied (unloaded) project
                //propertyPageFrame.HideFrame();
                //propertyPageFrame.ShowFrame( GetType().GUID );
            }

            return VSConstants.S_OK;
        }
    }
}