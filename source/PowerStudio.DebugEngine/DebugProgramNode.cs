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
    public abstract class DebugProgramNode : IDebugProgramNode2,
                                             IDebugProgramNodeAttach2,
                                             IDebugProgram2,
                                             IDebugEngineProgram2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugProgramNode2

        /// <summary>
        /// Gets the name of the program.
        /// </summary>
        /// <param name="pbstrProgramName">Returns the name of the program.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The name of a program is not the same thing as the path to the program, although the name of the program may be part of such a path.</remarks>
        public virtual int GetProgramName( out string pbstrProgramName )
        {
            Logger.Debug( string.Empty );
            pbstrProgramName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the name of the process hosting the program.
        /// </summary>
        /// <param name="dwHostNameType">A value from the GETHOSTNAME_TYPE enumeration that specifies the type of name to return.</param>
        /// <param name="pbstrHostName">Returns the name of the hosting process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetHostName( enum_GETHOSTNAME_TYPE dwHostNameType, out string pbstrHostName )
        {
            Logger.Debug( string.Empty );
            pbstrHostName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the system process identifier for the process hosting the program.
        /// </summary>
        /// <param name="pHostProcessId">Returns the system process identifier for the hosting process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetHostPid( AD_PROCESS_ID[] pHostProcessId )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// DEPRECATED. DO NOT USE.
        /// </summary>
        /// <param name="pbstrHostMachineName">Returns the name of the machine in which the program is running.</param>
        /// <returns>An implementation should always return E_NOTIMPL.</returns>
        [Obsolete( "DEPRECATED. DO NOT USE.", true )]
        public int GetHostMachineName_V7( out string pbstrHostMachineName )
        {
            Logger.Debug( string.Empty );
            pbstrHostMachineName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// DEPRECATED. DO NOT USE.
        /// </summary>
        /// <param name="pMDMProgram">The IDebugProgram2 interface that represents the program to attach to.</param>
        /// <param name="pCallback">The IDebugEventCallback2 interface to be used to send debug events to the SDM.</param>
        /// <param name="dwReason">A value from the ATTACH_REASON enumeration that specifies the reason for attaching.</param>
        /// <returns>An implementation should always return E_NOTIMPL.</returns>
        [Obsolete( "DEPRECATED. DO NOT USE.", true )]
        public int Attach_V7( IDebugProgram2 pMDMProgram, IDebugEventCallback2 pCallback, uint dwReason )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the name and identifier of the debug engine (DE) running a program.
        /// </summary>
        /// <param name="pbstrEngine">Returns the name of the DE running the program (C++-specific: this can be a null pointer indicating that the caller is not interested in the name of the engine).</param>
        /// <param name="pguidEngine">Returns the globally unique identifier of the DE running the program (C++-specific: this can be a null pointer indicating that the caller is not interested in the GUID of the engine).</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetEngineInfo( out string pbstrEngine, out Guid pguidEngine )
        {
            Logger.Debug( string.Empty );
            pbstrEngine = null;
            pguidEngine = default( Guid );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// DEPRECATED. DO NOT USE.
        /// </summary>
        /// <returns>An implementation should always return E_NOTIMPL.</returns>
        [Obsolete( "DEPRECATED. DO NOT USE.", true )]
        public int DetachDebugger_V7()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugProgramNodeAttach2

        /// <summary>
        /// Attaches to the associated program or defers the attach process to the IDebugEngine2::Attach method.
        /// </summary>
        /// <param name="guidProgramId">GUID to assign to the associated program.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if the IDebugEngine2::Attach method should not be called. Otherwise, returns an error code.</returns>
        /// <remarks>
        /// This method is called during the attach process, before the IDebugEngine2::Attach method is called. The OnAttach method can perform the attach process itself (in which case, this method returns S_FALSE) or defer the attach process to the IDebugEngine2::Attach method (the OnAttach method returns S_OK). In either event, the OnAttach method can set the GUID of the program being debugged to the given GUID.
        /// </remarks>
        public virtual int OnAttach( ref Guid guidProgramId )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugProgram2

        /// <summary>
        /// Retrieves a list of the threads that are running in the program.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugThreads2 object that contains a list of the threads.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int EnumThreads( out IEnumDebugThreads2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the name of the program.
        /// </summary>
        /// <param name="pbstrName">Returns the name of the program.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The name returned by this method is always a friendly, user-displayable name that describes the program.</remarks>
        public virtual int GetName( out string pbstrName )
        {
            Logger.Debug( string.Empty );
            pbstrName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Get the process that this program is running in.
        /// </summary>
        /// <param name="ppProcess">Returns the IDebugProcess2 interface that represents the process.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// Unless a debug engine (DE) implements the IDebugEngineLaunch2 interface, the DE's implementation of this method should always return E_NOTIMPL because a DE cannot determine which process it is running in and therefore cannot satisfy an implementation of this method.
        /// 
        /// Implementing the IDebugEngineLaunch2 interface means that the DE must know how to create a process; therefore, the DE's implementation of the IDebugProgram2 interface is able to know what process it is running in.
        /// </remarks>
        public virtual int GetProcess( out IDebugProcess2 ppProcess )
        {
            Logger.Debug( string.Empty );
            ppProcess = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Terminates the program.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// If possible, the program will be terminated and unloaded from the process; otherwise, the debug engine (DE) will perform any necessary cleanup.[
        /// This method or the IDebugProcess2::Terminate method is called by the IDE, typically in response to the user halting all debugging. The implementation of this method should, ideally, terminate the program within the process. If this is not possible, the DE should prevent the program from running any more in this process (and do any necessary cleanup). If the IDebugProcess2::Terminate method was called by the IDE, the entire process will be terminated sometime after the IDebugProgram2::Terminate method is called.
        /// </remarks>
        public virtual int Terminate()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Attaches to the program.
        /// </summary>
        /// <param name="pCallback"> An IDebugEventCallback2 object to be used for debug event notification.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code. The following table shows some possible error codes.
        /// Value                                             Description
        /// E_ATTACH_DEBUGGER_ALREADY_ATTACHED                The specified program is already attached to the debugger.
        /// E_ATTACH_DEBUGGEE_PROCESS_SECURITY_VIOLATION      A security violation occurred during the attach procedure.
        /// E_ATTACH_CANNOT_ATTACH_TO_DESKTOP                 A desktop program cannot be attached to the debugger.
        /// </returns>
        /// <remarks>
        /// A debug engine (DE) never calls this method to attach to a program. If the DE runs in the program's address space, the IDebugProgramNodeAttach2::OnAttach method is called. If the DE runs in the session debug manager's (SDM) address space, the IDebugEngine2::Attach method is called.
        /// </remarks>
        public virtual int Attach( IDebugEventCallback2 pCallback )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Determines if a debug engine (DE) can detach from the program.
        /// </summary>
        /// <returns>If can detach, returns S_OK; otherwise, returns an error code. Returns S_FALSE if the DE cannot detach from the program.</returns>
        public virtual int CanDetach()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Detaches a debug engine from the program.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// A detached program continues running, but it is no longer part of the debug session. No more program debug events are sent once the debug engine is detached.
        /// </remarks>
        public virtual int Detach()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets a GUID for this program.
        /// </summary>
        /// <param name="pguidProgramId">Returns the GUID for this program.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A debug engine (DE) must return the program identifier originally passed to the IDebugProgramNodeAttach2::OnAttach or IDebugEngine2::Attach methods. This allows identification of the program across debugger components.</remarks>
        public virtual int GetProgramId( out Guid pguidProgramId )
        {
            Logger.Debug( string.Empty );
            pguidProgramId = default( Guid );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the program's properties.
        /// </summary>
        /// <param name="ppProperty">Returns an IDebugProperty2 object that represents the program's properties.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// The properties returned by this method are specific to the program. If the program needs to return more than one property, then the IDebugProperty2 object returned by this method is a container of additional properties and calling the IDebugProperty2::EnumChildren method returns a list of all properties.
        /// 
        /// A program may expose any number and type of additional properties that can be described through the IDebugProperty2 interface. An IDE might display the additional program properties through a generic property browser user interface.
        /// </remarks>
        public virtual int GetDebugProperty( out IDebugProperty2 ppProperty )
        {
            Logger.Debug( string.Empty );
            ppProperty = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Continues running this program from a stopped state. Any previous execution state (such as a step) is cleared, and the program starts executing again.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// When the user starts execution from a stopped state in some other program's thread, this method is called on this program. This method is also called when the user selects the Start command from the Debug menu in the IDE. The implementation of this method may be as simple as calling the IDebugThread2::Resume method on the current thread in the program.
        /// 
        /// Do not send a stopping event or an immediate (synchronous) event to IDebugEventCallback2::Event while handling this call; otherwise the debugger might stop responding.
        /// </remarks>
        [Obsolete( "This method is deprecated. Use the IDebugProcess3::Execute method instead.", true )]
        public int Execute()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Continues running this program from a stopped state. Any previous execution state (such as a step) is preserved, and the program starts executing again.
        /// </summary>
        /// <param name="pThread">An IDebugThread2 object that represents the thread.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// This method is called on this program regardless of how many programs are being debugged, or which program generated the stopping event. The implementation must retain the previous execution state (such as a step) and continue execution as though it had never stopped before completing its prior execution. That is, if a thread in this program was doing a step-over operation and was stopped because some other program stopped, and then this method was called, the program must complete the original step-over operation.
        /// 
        /// Do not send a stopping event or an immediate (synchronous) event to IDebugEventCallback2::Event while handling this call; otherwise the debugger might stop responding.
        /// </remarks>
        [Obsolete( "This method is deprecated. Use the IDebugProcess3::Continue method instead.", true )]
        public int Continue( IDebugThread2 pThread )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Performs a step.
        /// </summary>
        /// <param name="pThread">An IDebugThread2 object that represents the thread being stepped.</param>
        /// <param name="sk"> A value from the STEPKIND enumeration that specifies the kind of step.</param>
        /// <param name="Step">A value from the STEPUNIT enumeration that specifies the unit of step (for example, by statement or instruction).</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// In case there is any thread synchronization or communication between threads, other threads in the program should run when a particular thread is stepping.
        /// 
        /// Do not send a stopping event or an immediate (synchronous) event to IDebugEventCallback2::Event while handling this call; otherwise the debugger may hang.
        /// </remarks>
        [Obsolete( "This method is deprecated. Use the IDebugProcess3::Step method instead.", true )]
        public int Step( IDebugThread2 pThread, enum_STEPKIND sk, enum_STEPUNIT Step )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Requests that the program stop execution the next time one of its threads attempts to run.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// An IDebugBreakEvent2 event is sent when the program next attempts to run code after this method is called.
        /// 
        /// This method is asynchronous in that the method returns immediately without necessarily waiting for the program to stop.
        /// </remarks>
        public virtual int CauseBreak()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of the code contexts for a given position in a source file.
        /// </summary>
        /// <param name="pDocPos">An IDebugDocumentPosition2 object representing an abstract position in a source file known to the IDE.</param>
        /// <param name="ppEnum">Returns an IEnumDebugCodeContexts2 object that contains a list of the code contexts.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method allows the session debug manager (SDM) or IDE to map a source file position into a code position. More than one code context is returned if the source generates multiple blocks of code (for example, C++ templates).</remarks>
        public virtual int EnumCodeContexts( IDebugDocumentPosition2 pDocPos, out IEnumDebugCodeContexts2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves the memory bytes occupied by the program.
        /// </summary>
        /// <param name="ppMemoryBytes">Returns an IDebugMemoryBytes2 object that represents the memory bytes of the program.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// The memory bytes as represented by the IDebugMemoryBytes2 object is for the program's image in memory and not any memory that was allocated when the program was executed.
        /// </remarks>
        public virtual int GetMemoryBytes( out IDebugMemoryBytes2 ppMemoryBytes )
        {
            Logger.Debug( string.Empty );
            ppMemoryBytes = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the disassembly stream for this program or a part of this program.
        /// </summary>
        /// <param name="dwScope"> Specifies a value from the DISASSEMBLY_STREAM_SCOPE enumeration that defines the scope of the disassembly stream.</param>
        /// <param name="pCodeContext">An IDebugCodeContext2 object that represents the position of where to start the disassembly stream.</param>
        /// <param name="ppDisassemblyStream">Returns an IDebugDisassemblyStream2 object that represents the disassembly stream.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_DISASM_NOTSUPPORTED if disassembly is not supported for this particular architecture.</returns>
        /// <remarks>
        /// If the dwScopes parameter has the DSS_HUGE flag of the DISASSEMBLY_STREAM_SCOPE enumeration set, then the disassembly is expected to return a large number of disassembly instructions, for example, for an entire file or module. If the DSS_HUGE flag is not set, then the disassembly is expected to be confined to a small region, typically that of a single function.
        /// </remarks>
        public virtual int GetDisassemblyStream( enum_DISASSEMBLY_STREAM_SCOPE dwScope,
                                                 IDebugCodeContext2 pCodeContext,
                                                 out IDebugDisassemblyStream2 ppDisassemblyStream )
        {
            Logger.Debug( string.Empty );
            ppDisassemblyStream = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of the modules that this program has loaded and is executing.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugModules2 object that contains a list of the modules.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A module is a DLL or assembly and is typically listed in the Modules debug window.</remarks>
        public virtual int EnumModules( out IEnumDebugModules2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// This method gets the Edit and Continue (ENC) update for this program. A custom debug engine always returns E_NOTIMPL.
        /// </summary>
        /// <param name="ppUpdate">Returns an internal interface that can be used to update this program.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A custom debug engine should always return E_NOTIMPL.</remarks>
        public virtual int GetENCUpdate( out object ppUpdate )
        {
            Logger.Debug( string.Empty );
            ppUpdate = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of the code paths for a given position in a source file.
        /// </summary>
        /// <param name="pszHint">The word under the cursor in the Source or Disassembly view in the IDE.</param>
        /// <param name="pStart">An IDebugCodeContext2 object representing the current code context.</param>
        /// <param name="pFrame">An IDebugStackFrame2 object representing the stack frame associated with the current breakpoint.</param>
        /// <param name="fSource">Nonzero (TRUE) if in the Source view, or zero (FALSE) if in the Disassembly view.</param>
        /// <param name="ppEnum">Returns an IEnumCodePaths2 object containing a list of the code paths.</param>
        /// <param name="ppSafety">Returns an IDebugCodeContext2 object representing an additional code context to be set as a breakpoint in case the chosen code path is skipped. This can happen in the case of a short-circuited Boolean expression, for example.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A code path describes the name of a method or function that was called to get to the current point in the execution of the program. A list of code paths represents the call stack.</remarks>
        public virtual int EnumCodePaths( string pszHint,
                                          IDebugCodeContext2 pStart,
                                          IDebugStackFrame2 pFrame,
                                          int fSource,
                                          out IEnumCodePaths2 ppEnum,
                                          out IDebugCodeContext2 ppSafety )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            ppSafety = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Writes a dump to a file.
        /// </summary>
        /// <param name="DUMPTYPE">A value from the DUMPTYPE enumeration that specifies the type of dump, for example, short or long.</param>
        /// <param name="pszDumpUrl">The URL to write the dump to. Typically, this is in the form of file://c:\path\filename.ext, but may be any valid URL.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A program dump would typically include the current stack frame, the stack itself, a list of the threads running in the program, and possibly any memory that the program owns.</remarks>
        public virtual int WriteDump( enum_DUMPTYPE DUMPTYPE, string pszDumpUrl )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugEngineProgram2

        /// <summary>
        /// Stops all threads running in this program.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// This method is called when this program is being debugged in a multi-program environment. When a stopping event from some other program is received, this method is called on this program. The implementation of this method should be asynchronous; that is, not all threads should be required to be stopped before this method returns. The implementation of this method may be as simple as calling the IDebugProgram2::CauseBreak method on this program.
        /// 
        /// No debug event is sent in response to this method.
        /// </remarks>
        public virtual int Stop()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Watches for execution (or stops watching for execution) to occur on the given thread.
        /// </summary>
        /// <param name="pOriginatingProgram">An IDebugProgram2 object representing the program being stepped.</param>
        /// <param name="dwTid">Specifies the identifier of the thread to watch.</param>
        /// <param name="fWatch">Non-zero (TRUE) means start watching for execution on the thread identified by dwTid; otherwise, zero (FALSE) means stop watching for execution on dwTid.</param>
        /// <param name="dwFrame">Specifies a frame index that controls the step type. When this is value is zero (0), the step type is "step into" and the program should stop whenever the thread identified by dwTid executes. When dwFrame is non-zero, the step type is "step over" and the program should stop only if the thread identified by dwTid is running in a frame whose index is equal to or higher on the stack than dwFrame.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// When the session debug manager (SDM) steps a program, identified by the pOriginatingProgram parameter, it notifies all other attached programs by calling this method.
        /// 
        /// This method is applicable only to same-thread stepping.
        /// </remarks>
        public virtual int WatchForThreadStep( IDebugProgram2 pOriginatingProgram, uint dwTid, int fWatch, uint dwFrame )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Allows (or disallows) expression evaluation to occur on the given thread, even if the program has stopped.
        /// </summary>
        /// <param name="pOriginatingProgram">An IDebugProgram2 object representing the program that is evaluating an expression.</param>
        /// <param name="dwTid">Specifies the identifier of the thread.</param>
        /// <param name="dwEvalFlags">A combination of flags from the EVALFLAGS enumeration that specify how the evaluation is to be performed.</param>
        /// <param name="pExprCallback">An IDebugEventCallback2 object to be used to send debug events that occur during expression evaluation.</param>
        /// <param name="fWatch">If non-zero (TRUE), allows expression evaluation on the thread identified by dwTid; otherwise, zero (FALSE) disallows expression evaluation on that thread.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// When the session debug manager (SDM) asks a program, identified by the pOriginatingProgram parameter, to evaluate an expression, it notifies all other attached programs by calling this method.
        /// 
        /// Expression evaluation in one program may cause code to run in another, due to function evaluation or evaluation of any IDispatch properties. Because of this, this method allows expression evaluation to run and complete even though the thread may be stopped in this program.
        /// </remarks>
        public virtual int WatchForExpressionEvaluationOnThread( IDebugProgram2 pOriginatingProgram,
                                                                 uint dwTid,
                                                                 uint dwEvalFlags,
                                                                 IDebugEventCallback2 pExprCallback,
                                                                 int fWatch )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}