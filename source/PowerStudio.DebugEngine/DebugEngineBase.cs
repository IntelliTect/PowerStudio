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
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    [ComVisible( true )]
    public abstract class DebugEngineBase : IDebugEngine2, IDebugEngineLaunch2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected IDebugEngineEventSource EngineEventSource { get; set; }

        #region Implementation of IDebugEngine2

        /// <summary>
        /// Retrieves a list of all programs being debugged by a debug DebugEngine (DE).
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugPrograms2 object that contains a list of all programs being debugged by a DE.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int EnumPrograms( out IEnumDebugPrograms2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Attaches a debug DebugEngine (DE) to a program or programs. Called by the session debug manager (SDM) when the DE is running in-process to the SDM.
        /// </summary>
        /// <param name="rgpPrograms">An array of IDebugProgram2 objects that represent programs to be attached to. These are port programs.</param>
        /// <param name="rgpProgramNodes">An array of IDebugProgramNode2 objects that represent program nodes, one for each program. The program nodes in this array represent the same programs as in pProgram. The program nodes are given so that the DE can identify the programs to attach to.</param>
        /// <param name="celtPrograms">Number of programs and/or program nodes in the pProgram and rgpProgramNodes arrays.</param>
        /// <param name="pCallback">The IDebugEventCallback2 object to be used to send debug events to the SDM.</param>
        /// <param name="dwReason">A value from the ATTACH_REASON enumeration that specifies the reason for attaching these programs. For more information, see the Remarks section.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// There are three reasons for attaching to a program, as follows:
        ///     ATTACH_REASON_LAUNCH indicates that the DE is attaching to the program because the user launched the process that contains it.
        ///     ATTACH_REASON_USER indicates that the user has explicitly requested the DE to attach to a program (or the process that contains a program).
        ///     ATTACH_REASON_AUTO indicates the DE is attaching to a particular program because it is already debugging other programs in a particular process. This is also called auto-attach
        /// 
        /// When this method is called, the DE needs to send these events in sequence:
        ///     IDebugEngineCreateEvent2 (if it has not already been sent for a particular instance of the debug DebugEngine)
        ///     IDebugProgramCreateEvent2
        ///     IDebugLoadCompleteEvent2
        /// 
        /// In addition, if the reason for attaching is ATTACH_REASON_LAUNCH, the DE needs to send the IDebugEntryPointEvent2 event.
        /// Once the DE gets the IDebugProgramNode2 object corresponding to the program being debugged, it can be queried for any private interface.
        /// Before calling the methods of a program node in the array given by pProgram or rgpProgramNodes, impersonation, if required, should be enabled on the IDebugProgram2 interface that represents the program node. Normally, however, this step is not necessary. For more information, see Security Issues.
        /// </remarks>
        public virtual int Attach( IDebugProgram2[] rgpPrograms,
                                   IDebugProgramNode2[] rgpProgramNodes,
                                   uint celtPrograms,
                                   IDebugEventCallback2 pCallback,
                                   enum_ATTACH_REASON dwReason )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Creates a pending breakpoint in the debug DebugEngine (DE).
        /// </summary>
        /// <param name="pBPRequest">An IDebugBreakpointRequest2 object that describes the pending breakpoint to create.</param>
        /// <param name="ppPendingBP">Returns an IDebugPendingBreakpoint2 object that represents the pending breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Typically returns E_FAIL if the pBPRequest parameter does not match any language supported by the DE of if the pBPRequest parameter is invalid or incomplete.</returns>
        /// <remarks>
        /// A pending breakpoint is essentially a collection of all the information needed to bind a breakpoint to code. The pending breakpoint returned from this method is not bound to code until the IDebugPendingBreakpoint2::Bind method is called.
        /// 
        /// For each pending breakpoint the user sets, the session debug manager (SDM) calls this method in each attached DE. It is up to the DE to verify that the breakpoint is valid for programs running in that DE.
        /// 
        /// When the user sets a breakpoint on a line of code, the DE is free to bind the breakpoint to the closest line in the document that corresponds to this code. This makes it possible for the user to set a breakpoint on the first line of a multi-line statement, but bind it on the last line (where all the code is attributed in the debug information).
        /// </remarks>
        public virtual int CreatePendingBreakpoint( IDebugBreakpointRequest2 pBPRequest,
                                                    out IDebugPendingBreakpoint2 ppPendingBP )
        {
            Logger.Debug( string.Empty );
            ppPendingBP = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Specifies how the debug DebugEngine (DE) should handle a given exception.
        /// </summary>
        /// <param name="pException">An EXCEPTION_INFO structure that describes the exception and how to debug it.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A DE could be instructed to stop the program generating an exception at first chance, second chance, or not at all.</remarks>
        public virtual int SetException( EXCEPTION_INFO[] pException )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Removes the specified exception so it is no longer handled by the debug DebugEngine.
        /// </summary>
        /// <param name="pException">An EXCEPTION_INFO structure that describes the exception to be removed.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// The exception being removed must have been previously set by an earlier call to the IDebugEngine2::SetException method.
        /// To remove all set exceptions at once, call the IDebugEngine2::RemoveAllSetExceptions method.
        /// </remarks>
        public virtual int RemoveSetException( EXCEPTION_INFO[] pException )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Removes the list of exceptions the IDE has set for a particular run-time architecture or language.
        /// </summary>
        /// <param name="guidType">Either the GUID for the language or the GUID for the debug DebugEngine that is specific to a run-time architecture.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// The exceptions removed by this method were set by earlier calls to the IDebugEngine2::SetException method.
        /// To remove a specific exception, call the IDebugEngine2::RemoveSetException method.
        /// </remarks>
        public virtual int RemoveAllSetExceptions( ref Guid guidType )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the GUID of the debug DebugEngine (DE).
        /// </summary>
        /// <param name="pguidEngine">Returns the GUID of the DE.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>Some examples of typical GUIDs are guidScriptEng, guidNativeEng, or guidSQLEng. New debug engines will create their own GUID for identification.</remarks>
        public virtual int GetEngineId( out Guid pguidEngine )
        {
            Logger.Debug( string.Empty );
            pguidEngine = default( Guid );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Informs a debug DebugEngine (DE) that the program specified has been atypically terminated and that the DE should clean up all references to the program and send a program destroy event.
        /// </summary>
        /// <param name="pProgram">An IDebugProgram2 object that represents the program that has been atypically terminated.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// After this method is called, the DE subsequently sends an IDebugProgramDestroyEvent2 event back to the session debug manager (SDM).
        /// This method is not implemented (returns E_NOTIMPL) if the DE runs in the same process as the program being debugged. This method is implemented only if the DE runs in the same process as the SDM.
        /// </remarks>
        public virtual int DestroyProgram( IDebugProgram2 pProgram )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Called by the session debug manager (SDM) to indicate that a synchronous debug event, previously sent by the debug DebugEngine (DE) to the SDM, was received and processed.
        /// </summary>
        /// <param name="pEvent">An IDebugEvent2 object that represents the previously sent synchronous event from which the debugger should now continue.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The DE must verify that it was the source of the event represented by the pEvent parameter.</remarks>
        public virtual int ContinueFromSynchronousEvent( IDebugEvent2 pEvent )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets the locale of the debug DebugEngine (DE).
        /// </summary>
        /// <param name="wLangID">Specifies the language locale. For example, 1033 for English.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method is called by the session debug manager (SDM) to propagate the locale settings of the IDE so that strings returned by the DE are properly localized.</remarks>
        public virtual int SetLocale( ushort wLangID )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets the registry root for the debug DebugEngine (DE).
        /// </summary>
        /// <param name="pszRegistryRoot">The registry root to use.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method allows Visual Studio to specify an alternate registry root that the DE should use to obtain registry settings; for example, "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\8.0Exp".</remarks>
        public virtual int SetRegistryRoot( string pszRegistryRoot )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// This method sets a registry value known as a metric.
        /// </summary>
        /// <param name="pszMetric">The metric name.</param>
        /// <param name="varValue">Specifies the metric value.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A metric is a registry value used to change a debug DebugEngine's behavior or to advertise supported functionality. This method can forward the call to the appropriate form of the SDK Helpers for Debugging function, SetMetric.</remarks>
        public virtual int SetMetric( string pszMetric, object varValue )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Requests that all programs being debugged by this debug DebugEngine (DE) to stop execution the next time one of their threads attempts to run.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method is asynchronous: an IDebugBreakEvent2 event is sent when the program next attempts to execute after this method is called.</remarks>
        public virtual int CauseBreak()
        {
            Logger.Debug( string.Empty );
            return ( (IDebugProgram2) this ).CauseBreak();
        }

        #endregion

        #region Implementation of IDebugEngineLaunch2

        /// <summary>
        /// This method launches a process by means of the debug DebugEngine (DE).
        /// </summary>
        /// <param name="pszServer">The name of the machine in which to launch the process. Use a null value to specify the local machine.</param>
        /// <param name="pPort">The IDebugPort2 interface representing the port that the program will run in.</param>
        /// <param name="pszExe">The name of the executable to be launched.</param>
        /// <param name="pszArgs">The arguments to pass to the executable. May be a null value if there are no arguments.</param>
        /// <param name="pszDir">The name of the working directory used by the executable. May be a null value if no working directory is required.</param>
        /// <param name="bstrEnv">Environment block of NULL-terminated strings, followed by an additional NULL terminator.</param>
        /// <param name="pszOptions">The options for the executable.</param>
        /// <param name="dwLaunchFlags">Specifies the LAUNCH_FLAGS for a session.</param>
        /// <param name="hStdInput">Handle to an alternate input stream. May be 0 if redirection is not required.</param>
        /// <param name="hStdOutput">Handle to an alternate output stream. May be 0 if redirection is not required.</param>
        /// <param name="hStdError">Handle to an alternate error output stream. May be 0 if redirection is not required.</param>
        /// <param name="pCallback">The IDebugEventCallback2 object that receives debugger events.</param>
        /// <param name="ppProcess">Returns the resulting IDebugProcess2 object that represents the launched process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// Normally, Visual Studio launches a program using the IDebugPortEx2::LaunchSuspended method and then attaches the debugger to the suspended program. However, there are circumstances in which the debug DebugEngine may need to launch a program (for example, if the debug DebugEngine is part of an interpreter and the program being debugged is an interpreted language), in which case Visual Studio uses the IDebugEngineLaunch2::LaunchSuspended method.
        /// 
        /// The IDebugEngineLaunch2::ResumeProcess method is called to start the process after the process has been successfully launched in a suspended state.
        /// </remarks>
        public virtual int LaunchSuspended( string pszServer,
                                            IDebugPort2 pPort,
                                            string pszExe,
                                            string pszArgs,
                                            string pszDir,
                                            string bstrEnv,
                                            string pszOptions,
                                            enum_LAUNCH_FLAGS dwLaunchFlags,
                                            uint hStdInput,
                                            uint hStdOutput,
                                            uint hStdError,
                                            IDebugEventCallback2 pCallback,
                                            out IDebugProcess2 ppProcess )
        {
            Logger.Debug( string.Empty );
            ppProcess = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Resumes process execution.
        /// </summary>
        /// <param name="pProcess">An IDebugProcess2 object that represents the process to be resumed.</param>
        /// <returns>If successful, returns S_OK; otherwise returns an error code.</returns>
        /// <remarks>This method is called after a process has been launched with a call to the IDebugEngineLaunch2::LaunchSuspended method.</remarks>
        public virtual int ResumeProcess( IDebugProcess2 pProcess )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Determines if a process can be terminated.
        /// </summary>
        /// <param name="pProcess">An IDebugProcess2 object that represents the process to be terminated.</param>
        /// <returns>If successful, returns S_OK; otherwise returns an error code. Returns S_FALSE if the DebugEngine cannot terminate the process, for example, because access is denied.</returns>
        /// <remarks>If this method returns S_OK, then it the IDebugEngineLaunch2::TerminateProcess method can be called to actually terminate the process.</remarks>
        public virtual int CanTerminateProcess( IDebugProcess2 pProcess )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Terminates a process.
        /// </summary>
        /// <param name="pProcess">An IDebugProcess2 object that represents the process to be terminated.</param>
        /// <returns>If successful, returns S_OK; otherwise returns an error code.</returns>
        /// <remarks>Call the IDebugEngineLaunch2::CanTerminateProcess method before calling this method.</remarks>
        public virtual int TerminateProcess( IDebugProcess2 pProcess )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}