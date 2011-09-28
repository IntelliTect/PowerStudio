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
using Microsoft.VisualStudio.Project.Automation;
using NLog;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Project
{
    [ComVisible( true )]
    public class OAPsProject : OAProject
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public OAPsProject( PowerShellProjectNode project )
                : base( project )
        {
        }
    }
}