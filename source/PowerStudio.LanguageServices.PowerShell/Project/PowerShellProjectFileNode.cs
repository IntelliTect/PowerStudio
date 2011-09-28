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
using EnvDTE;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using NLog;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Project
{
    public class PowerShellProjectFileNode : FileNode
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private OAPsProjectFileItem _AutomationObject;

        internal PowerShellProjectFileNode( ProjectNode root, ProjectElement e )
                : base( root, e )
        {
        }

        internal OleServiceProvider.ServiceCreatorCallback ServiceCreator
        {
            get { return CreateServices; }
        }

        /// <summary>
        /// Get an instance of the automation object for a FileNode
        /// </summary>
        /// <returns>
        /// An instance of the Automation.OAFileNode if succeeded
        /// </returns>
        public override object GetAutomationObject()
        {
            return _AutomationObject ??
                   ( _AutomationObject = new OAPsProjectFileItem( ProjectMgr.GetAutomationObject() as OAProject, this ) );
        }

        private object CreateServices( Type serviceType )
        {
            object service = null;
            if ( typeof (ProjectItem) == serviceType )
            {
                service = GetAutomationObject();
            }
            return service;
        }
    }
}