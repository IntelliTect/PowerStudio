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
using System.Diagnostics;
using System.Runtime.InteropServices;
using IntelliTect.DebugEngine;
using Microsoft.VisualStudio;
using NLog;
using PowerStudio.Resources;

#endregion

namespace PowerStudio.Debugger
{
    [ComVisible( true )]
    [Guid( PsConstants.PortSupplierGuid )]
    public class PowerShellPortSupplier : DebugPortSupplier
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellPortSupplier()
        {
            Logger.Debug(string.Empty);
            Trace.WriteLine( "Create Port Provider" );
        }

        /// <summary>
        /// Verifies that a port supplier can add new ports.
        /// </summary>
        /// <returns>
        /// If the port can be added, returns S_OK; otherwise, returns S_FALSE to indicate no ports can be added to this port supplier.
        /// </returns>
        public override int CanAddPort()
        {
            Logger.Debug(string.Empty);
            return VSConstants.S_FALSE;
        }

        public override int GetPortSupplierId(out Guid pguidPortSupplier)
        {
            Logger.Debug(string.Empty);
            pguidPortSupplier = GetType().GUID;
            return VSConstants.S_OK;
        }

        /// <summary>
        /// Gets a port from a port supplier.
        /// </summary>
        /// <param name="guidPort">Globally unique identifier (GUID) of the port.</param>
        /// <param name="ppPort">Returns an IDebugPort2 object that represents the port.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code. Returns E_PORTSUPPLIER_NO_PORT if no port exists with the given identifier.
        /// </returns>
        public override int GetPort(ref Guid guidPort, out Microsoft.VisualStudio.Debugger.Interop.IDebugPort2 ppPort)
        {
            Logger.Debug(string.Empty);
            guidPort = new Guid( PsConstants.PortGuid );
            ppPort = new PowerShellDebugPort();
            return VSConstants.S_OK;
        }
    }
}