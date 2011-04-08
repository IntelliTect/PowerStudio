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

#endregion

namespace PowerStudio.VsExtension.Project
{
    public class PowerShellProjectFileNode : FileNode
    {
        private OAPsProjectFileItem _AutomationObject;

        internal PowerShellProjectFileNode( ProjectNode root, ProjectElement e )
                : base( root, e )
        {
        }

        internal OleServiceProvider.ServiceCreatorCallback ServiceCreator
        {
            get { return CreateServices; }
        }

        public override object GetAutomationObject()
        {
            if ( _AutomationObject == null )
            {
                _AutomationObject = new OAPsProjectFileItem( ProjectMgr.GetAutomationObject() as OAProject, this );
            }

            return _AutomationObject;
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