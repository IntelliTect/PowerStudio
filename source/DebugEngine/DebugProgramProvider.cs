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
    public abstract class DebugProgramProvider : IDebugProgramProvider2
    {
        #region Implementation of IDebugProgramProvider2

        /// <summary>
        /// Retrieves a list of running programs from a specified process.
        /// </summary>
        /// <param name="Flags">
        /// A combination of flags from the PROVIDER_FLAGS enumeration. The following flags are typical for this call:
        /// 
        /// Flag                         Description
        /// PFLAG_REMOTE_PORT            Caller is running on remote machine. 
        /// PFLAG_DEBUGGEE               Caller is currently being debugged (additional information about marshalling will be returned for each node).
        /// PFLAG_ATTACHED_TO_DEBUGGEE   Caller was attached to but not launched by the debugger.
        /// PFLAG_GET_PROGRAM_NODES      Caller is asking for a list of program nodes to be returned.
        /// </param>
        /// <param name="pPort">The port the calling process is running on.</param>
        /// <param name="ProcessId">An AD_PROCESS_ID structure holding the ID of the process that contains the program in question.</param>
        /// <param name="EngineFilter">An array of GUIDs for debug engines assigned to debug this process (these will be used to filter the programs that are actually returned based on what the supplied engines support; if no engines are specified, then all programs will be returned).</param>
        /// <param name="pProcess">A PROVIDER_PROCESS_DATA structure that is filled in with the requested information.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method is normally called by a process to obtain a list of programs running in that process. The returned information is a list of IDebugProgramNode2 objects.</remarks>
        public virtual int GetProviderProcessData( enum_PROVIDER_FLAGS Flags,
                                                   IDebugDefaultPort2 pPort,
                                                   AD_PROCESS_ID ProcessId,
                                                   CONST_GUID_ARRAY EngineFilter,
                                                   PROVIDER_PROCESS_DATA[] pProcess )
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves the program node for a specific program.
        /// </summary>
        /// <param name="Flags">
        /// A combination of flags from the PROVIDER_FLAGS enumeration. The following flags are typical for this call:
        /// 
        /// Flag                         Description
        /// PFLAG_REMOTE_PORT            Caller is running on remote machine.
        /// PFLAG_DEBUGGEE               Caller is currently being debugged (additional information about marshalling will be returned for each node).
        /// PFLAG_ATTACHED_TO_DEBUGGEE   Caller was attached to but not launched by the debugger.
        /// </param>
        /// <param name="pPort">The port the calling process is running on.</param>
        /// <param name="ProcessId">An AD_PROCESS_ID structure holding the ID of the process that contains the program in question.</param>
        /// <param name="guidEngine">GUID of the debug engine that the program is attached to (if any).</param>
        /// <param name="programId">ID of the program for which to get the program node.</param>
        /// <param name="ppProgramNode">An IDebugProgramNode2 object representing the requested program node.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetProviderProgramNode( enum_PROVIDER_FLAGS Flags,
                                                   IDebugDefaultPort2 pPort,
                                                   AD_PROCESS_ID ProcessId,
                                                   ref Guid guidEngine,
                                                   ulong programId,
                                                   out IDebugProgramNode2 ppProgramNode )
        {
            ppProgramNode = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Allows the process to be notified of port events.
        /// </summary>
        /// <param name="Flags">
        /// A combination of flags from the PROVIDER_FLAGS enumeration. The following flags are typical for this call:
        /// 
        /// Flag                         Description
        /// PFLAG_REMOTE_PORT            Caller is running on remote machine.
        /// PFLAG_DEBUGGEE               Caller is currently being debugged (additional information about marshalling is returned for each node).
        /// PFLAG_ATTACHED_TO_DEBUGGEE   Caller was attached to but not launched by the debugger.
        /// PFLAG_REASON_WATCH           Caller wants to watch for events. If this flag is not set. then the callback event is removed and the caller no longer receives notifications.
        /// </param>
        /// <param name="pPort">The port the calling process is running on.</param>
        /// <param name="ProcessId">An AD_PROCESS_ID structure holding the ID of the process that contains the program in question.</param>
        /// <param name="EngineFilter">An array of GUIDs of debug engines associated with the process.</param>
        /// <param name="guidLaunchingEngine">GUID of the debug engine that launched this process (if any).</param>
        /// <param name="pEventCallback">An IDebugPortNotify2 object that receives the event notifications.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>When a caller wants to remove an event handler that was established with a previous call to this method, the caller passes the same parameters as it did the first time but leaves off the PFLAG_REASON_WATCH flag.</remarks>
        public virtual int WatchForProviderEvents( enum_PROVIDER_FLAGS Flags,
                                                   IDebugDefaultPort2 pPort,
                                                   AD_PROCESS_ID ProcessId,
                                                   CONST_GUID_ARRAY EngineFilter,
                                                   ref Guid guidLaunchingEngine,
                                                   IDebugPortNotify2 pEventCallback )
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Establishes a locale to be used for any locale-specific resources.
        /// </summary>
        /// <param name="wLangID">Language ID to establish. For example, 1033 for English.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int SetLocale( ushort wLangID )
        {
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}