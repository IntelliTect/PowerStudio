#region License

// 
// Copyright (c) 2011, PowerStudio.DebugEngine Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    public abstract class DebugPortSupplier : IDebugPortSupplier2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugPortSupplier2

        /// <summary>
        /// Gets the port supplier name.
        /// </summary>
        /// <param name="pbstrName">Returns the name of the port supplier.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetPortSupplierName( out string pbstrName )
        {
            Logger.Debug( string.Empty );
            pbstrName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the port supplier identifier.
        /// </summary>
        /// <param name="pguidPortSupplier">Returns the GUID of the port supplier.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetPortSupplierId( out Guid pguidPortSupplier )
        {
            Logger.Debug( string.Empty );
            pguidPortSupplier = default( Guid );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets a port from a port supplier.
        /// </summary>
        /// <param name="guidPort">Globally unique identifier (GUID) of the port.</param>
        /// <param name="ppPort">Returns an IDebugPort2 object that represents the port.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_PORTSUPPLIER_NO_PORT if no port exists with the given identifier.</returns>
        public virtual int GetPort( ref Guid guidPort, out IDebugPort2 ppPort )
        {
            Logger.Debug( string.Empty );
            ppPort = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of all the ports supplied by a port supplier.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugPorts2 object containing a list of ports supplied.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int EnumPorts( out IEnumDebugPorts2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Verifies that a port supplier can add new ports.
        /// </summary>
        /// <returns>If the port can be added, returns S_OK; otherwise, returns S_FALSE to indicate no ports can be added to this port supplier.</returns>
        /// <remarks>
        /// Call this method before calling the IDebugPortSupplier2::AddPort method since the latter method creates the port as well as adding it, which could be a time-consuming operation.
        /// </remarks>
        public virtual int CanAddPort()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Adds a port.
        /// </summary>
        /// <param name="pRequest">An IDebugPortRequest2 object that describes the port to be added.</param>
        /// <param name="ppPort">Returns an IDebugPort2 object that represents the port.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// This method actually creates the requested port as well as adding it to the port supplier's internal list of active ports. The IDebugPortSupplier2::CanAddPort method can be called first to avoid possible time-consuming delays.
        /// </remarks>
        public virtual int AddPort( IDebugPortRequest2 pRequest, out IDebugPort2 ppPort )
        {
            Logger.Debug( string.Empty );
            ppPort = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Removes a port.
        /// </summary>
        /// <param name="pPort">An IDebugPort2 object that represents the port to be removed.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method removes the port from the port supplier's internal list of active ports.</remarks>
        public virtual int RemovePort( IDebugPort2 pPort )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}