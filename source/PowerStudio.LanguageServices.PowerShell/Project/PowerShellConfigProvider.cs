#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Project;
using NLog;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Project
{
    public class PowerShellConfigProvider : ConfigProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellConfigProvider( ProjectNode manager )
                : base( manager )
        {
            Logger.Debug( string.Empty );
        }

        protected override ProjectConfig CreateProjectConfiguration( string configName )
        {
            Logger.Debug( string.Empty );
            return new PowerShellProjectConfig( ProjectMgr, configName );
        }
    }
}