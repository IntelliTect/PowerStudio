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
using IntelliTect.DebugEngine;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.Debugger
{
    public class PowerShellProcess : DebugProcess
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellProcess( IDebugPort2 port )
                : this( port, Guid.NewGuid() )
        {
            Logger.Debug( string.Empty );
        }

        public PowerShellProcess( IDebugPort2 port, Guid guid )
        {
            Logger.Debug( string.Empty );
            Port = port;
            Id = guid;
        }

        public Guid Id { get; private set; }

        public IDebugPort2 Port { get; private set; }

        /// <summary>
        /// Gets the port that the process is running on.
        /// </summary>
        /// <param name="ppPort">Returns an IDebugPort2 object that represents the port on which the process was launched.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int GetPort( out IDebugPort2 ppPort )
        {
            Logger.Debug( string.Empty );
            ppPort = Port;
            return VSConstants.S_OK;
        }
    }
}