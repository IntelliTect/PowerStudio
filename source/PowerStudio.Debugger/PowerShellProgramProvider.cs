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
using PowerStudio.DebugEngine;
using NLog;
using PowerStudio.Resources;

#endregion

namespace PowerStudio.Debugger
{
    [ComVisible( true )]
    [Guid( PsConstants.ProgramProviderGuid )]
    public class PowerShellProgramProvider : DebugProgramProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellProgramProvider()
        {
            Logger.Debug( string.Empty );
        }
    }
}