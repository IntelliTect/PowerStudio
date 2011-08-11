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
using IntelliTect.DebugEngine;
using Microsoft.VisualStudio;
using NLog;
using PowerStudio.Resources;

#endregion

namespace PowerStudio.Debugger
{
    [ComVisible( true )]
    [Guid( PsConstants.PortGuid )]
    public class PowerShellDebugPort : DebugPort
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets the port identifier.
        /// </summary>
        /// <param name="pguidPort">Returns the GUID that identifies the port.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int GetPortId( out Guid pguidPort )
        {
            Logger.Debug( string.Empty );
            pguidPort = GetType().GUID;
            return VSConstants.S_OK;
        }
    }
}