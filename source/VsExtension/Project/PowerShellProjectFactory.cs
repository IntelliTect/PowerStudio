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
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;

#endregion

namespace PowerStudio.VsExtension.Project
{
    [Guid( PsConstants.ProjectFactoryGuid )]
    public class PowerShellProjectFactory : ProjectFactory
    {
        private readonly PowerShellPackage _Package;

        public PowerShellProjectFactory( PowerShellPackage package )
                : base( package )
        {
            _Package = package;
        }

        /// <summary>
        /// Creates the project.
        /// </summary>
        /// <returns></returns>
        protected override ProjectNode CreateProject()
        {
            var project = new PowerShellProjectNode( _Package );
            var oleProvider =
                    (IOleServiceProvider) ( (IServiceProvider) _Package ).GetService( typeof (IOleServiceProvider) );
            project.SetSite( oleProvider );

            return project;
        }
    }
}