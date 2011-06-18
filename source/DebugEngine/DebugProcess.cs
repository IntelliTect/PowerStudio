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
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace DebugEngine
{
    public class DebugProcess : IDebugProcess2
    {
        #region Implementation of IDebugProcess2

        /// <summary>
        /// Gets a description of the process.
        /// </summary>
        /// <param name="Fields">A combination of values from the PROCESS_INFO_FIELDS enumeration that specifies which fields of the pProcessInfo parameter are to be filled in.</param>
        /// <param name="pProcessInfo">A PROCESS_INFO structure that is filled in with a description of the process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetInfo( enum_PROCESS_INFO_FIELDS Fields, PROCESS_INFO[] pProcessInfo )
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of all the programs contained by this process.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugPrograms2 object that contains a list of all the programs in the process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int EnumPrograms( out IEnumDebugPrograms2 ppEnum )
        {
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the title, friendly name, or file name of the process.
        /// </summary>
        /// <param name="gnType">A value from the GETNAME_TYPE enumeration that specifies what type of name to return.</param>
        /// <param name="pbstrName">Returns the name of the process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetName( enum_GETNAME_TYPE gnType, out string pbstrName )
        {
            pbstrName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the server that this process is running on.
        /// </summary>
        /// <param name="ppServer">Returns an IDebugCoreServer2 object that represents the server on which this process is running.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>More than one server can be running on a single machine.</remarks>
        public virtual int GetServer( out IDebugCoreServer2 ppServer )
        {
            ppServer = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Terminates the process.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>When a process is terminated, all programs within that process are terminated; none are allowed to run any more code.</remarks>
        public virtual int Terminate()
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Attaches the session debug manager (SDM) to the process.
        /// </summary>
        /// <param name="pCallback">An IDebugEventCallback2 object that is used for debug event notification.</param>
        /// <param name="rgguidSpecificEngines">An array of GUIDs of debug engines to be used to debug programs running in the process. This parameter can be a null value. See Remarks for details.</param>
        /// <param name="celtSpecificEngines">The number of debug engines in the rgguidSpecificEngines array and the size of the rghrEngineAttach array.</param>
        /// <param name="rghrEngineAttach">An array of HRESULT codes returned by the debug engines. The size of this array is specified in the celtSpecificEngines parameter. Each code is typically either S_OK or S_ATTACH_DEFERRED. The latter indicates that the DE is currently attached to no programs.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code. The following table shows other possible values.
        /// 
        /// Value                                          Description
        /// E_ATTACH_DEBUGGER_ALREADY_ATTACHED             The specified process is already attached to the debugger.
        /// E_ATTACH_DEBUGGEE_PROCESS_SECURITY_VIOLATION   A security violation occurred during the attach procedure.
        /// E_ATTACH_CANNOT_ATTACH_TO_DESKTOP              A desktop process cannot be attached to the debugger.
        /// </returns>
        /// <remarks>
        /// Attaching to a process attaches the SDM to all programs running in that process that can be debugged by the debug engines (DE) specified in the rgguidSpecificEngines array. Set the rgguidSpecificEngines parameter to a null value or include GUID_NULL in the array to attach to all programs in the process.
        /// 
        /// All debug events that occur in the process are sent to the given IDebugEventCallback2 object. This IDebugEventCallback2 object is provided when the SDM calls this method.
        /// </remarks>
        public virtual int Attach( IDebugEventCallback2 pCallback,
                                   Guid[] rgguidSpecificEngines,
                                   uint celtSpecificEngines,
                                   int[] rghrEngineAttach )
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Determines if the session debug manager (SDM) can detach the process.
        /// </summary>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if the debugger cannot detach from the process. Otherwise, returns an error code.</returns>
        public virtual int CanDetach()
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Detaches the debugger from this process by detaching all of the programs in the process.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>All programs and the process continue running, but are no longer part of the debug session. After the detach operation is complete, no more debug events for this process (and its programs) will be sent.</remarks>
        public virtual int Detach()
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the system process identifier.
        /// </summary>
        /// <param name="pguidProcessId">An AD_PROCESS_ID structure that is filled in with the system process identifier information.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetPhysicalProcessId( AD_PROCESS_ID[] pProcessId )
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the GUID for this process.
        /// </summary>
        /// <param name="pguidProcessId">Returns the GUID for this process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The Globally Unique IDentifier (GUID) identifies this process from all other processes running in the system.</remarks>
        public virtual int GetProcessId( out Guid pguidProcessId )
        {
            pguidProcessId = default( Guid );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the name of the session that is debugging this process. An IDE can display this information to a user who is debugging a particular process on a particular machine.
        /// </summary>
        /// <param name="pbstrSessionName"></param>
        /// <returns>This method should always return E_NOTIMPL.</returns>
        [Obsolete( "This method is deprecated, and its implementation should always return E_NOTIMPL.", true )]
        public int GetAttachedSessionName( out string pbstrSessionName )
        {
            pbstrSessionName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of all the threads running in the process.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugThreads2 object that contains a list of all threads in all programs in the process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// This method enumerates the threads running in each program and then combines them into a process view of the threads. A single thread may run in multiple programs; this method enumerates that thread only once.
        /// 
        /// This method presents a list of the process's threads without duplicates. Otherwise, to enumerate the threads running in a particular program, use the IDebugProgram2::EnumThreads method.
        /// </remarks>
        public virtual int EnumThreads( out IEnumDebugThreads2 ppEnum )
        {
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Requests that the next program that is running code in this process halt and send an IDebugBreakEvent2 event object.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int CauseBreak()
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the port that the process is running on.
        /// </summary>
        /// <param name="ppPort">Returns an IDebugPort2 object that represents the port on which the process was launched.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetPort( out IDebugPort2 ppPort )
        {
            ppPort = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}