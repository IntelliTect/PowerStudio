#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;

namespace PowerStudio.VsExtension.Project
{
    [ComVisible( true )]
    [Guid( PsConstants.OAPsProjectFileItemGuidString )]
    public class OAPsProjectFileItem : OAFileItem
    {
        public OAPsProjectFileItem( OAProject project, FileNode node )
                : base( project, node )
        {
        }
    }
}