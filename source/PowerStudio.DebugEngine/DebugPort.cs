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
    public abstract class DebugPort : IDebugPort2, IDebugDefaultPort2, IDebugPortNotify2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugPort2

        /// <summary>
        /// Gets the port name.
        /// </summary>
        /// <param name="pbstrName">Returns the name of the port.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetPortName( out string pbstrName )
        {
            Logger.Debug( string.Empty );
            pbstrName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the port identifier.
        /// </summary>
        /// <param name="pguidPort">Returns the GUID that identifies the port.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetPortId( out Guid pguidPort )
        {
            Logger.Debug( string.Empty );
            pguidPort = default( Guid );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the description of a port that was previously used to create the port (if available).
        /// </summary>
        /// <param name="ppRequest">Returns an IDebugPortRequest2 object representing the request that was used to create the port.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_PORT_NO_REQUEST if a port was not created using an IDebugPortRequest2 port request.</returns>
        public virtual int GetPortRequest( out IDebugPortRequest2 ppRequest )
        {
            Logger.Debug( string.Empty );
            ppRequest = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the port supplier for this port.
        /// </summary>
        /// <param name="ppSupplier">Returns an IDebugPortSupplier2 object represents the port supplier for a port.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetPortSupplier( out IDebugPortSupplier2 ppSupplier )
        {
            Logger.Debug( string.Empty );
            ppSupplier = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the specified process running on a port.
        /// </summary>
        /// <param name="ProcessId">An AD_PROCESS_ID structure that specifies the process identifier.</param>
        /// <param name="ppProcess">Returns an IDebugProcess2 object representing the process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetProcess( AD_PROCESS_ID ProcessId, out IDebugProcess2 ppProcess )
        {
            Logger.Debug( string.Empty );
            ppProcess = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Returns a list of all the processes running on a port.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugProcesses2 object that contains a list of all the processes running on a port.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int EnumProcesses( out IEnumDebugProcesses2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region IDebugDefaultPort2

        /// <summary>
        /// This method gets an IDebugPortNotify2 interface for this port.
        /// </summary>
        /// <param name="ppPortNotify">An IDebugPortNotify2 object.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// Normally, the QueryInterface method is called on the object implementing the IDebugPort2 interface to obtain an IDebugPortNotify2 interface. However, there are circumstances in which the desired interface is implemented on a different object. This method hides those circumstances and returns the IDebugPortNotify2 interface from the most appropriate object.
        /// </remarks>
        public virtual int GetPortNotify( out IDebugPortNotify2 ppPortNotify )
        {
            Logger.Debug( string.Empty );
            ppPortNotify = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// This method obtains an interface to the server that this port is on.
        /// </summary>
        /// <param name="ppServer">Returns an object implementing the IDebugCoreServer3 interface.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The IDebugCoreServer3 is implemented by Visual Studio and represents the server that the port is located on.</remarks>
        public virtual int GetServer( out IDebugCoreServer3 ppServer )
        {
            Logger.Debug( string.Empty );
            ppServer = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// This method determines whether this port is on the local machine.
        /// </summary>
        /// <returns>Returns S_OK if this port is local (on the same machine as the caller) or S_FALSE if the port is on another machine.</returns>
        public virtual int QueryIsLocal()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugPortNotify2

        /// <summary>
        /// Registers a program that can be debugged with the port it is running on.
        /// </summary>
        /// <param name="pProgramNode">An IDebugProgramNode2 object that represents the program to be registered.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A program node can be unregistered from the port by calling the IDebugPortNotify2::RemoveProgramNode method.</remarks>
        public virtual int AddProgramNode( IDebugProgramNode2 pProgramNode )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Unregisters a program that can be debugged from the port it is running on.
        /// </summary>
        /// <param name="pProgramNode">An IDebugProgramNode2 object that represents the program to be unregistered.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int RemoveProgramNode( IDebugProgramNode2 pProgramNode )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}