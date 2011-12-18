#region License

// 
// Copyright (c) 2011, PowerStudio.DebugEngine Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    public abstract class DebugThread : IDebugThread2, IDebugThread100
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugThread2

        /// <summary>
        /// Retrieves a list of the stack frames for this thread.
        /// </summary>
        /// <param name="dwFieldSpec">A combination of flags from the FRAMEINFO_FLAGS enumeration that specifies which fields of the FRAMEINFO structures are to be filled out. Specify the FIF_FUNCNAME_FORMAT flag to format the function name into a single string.</param>
        /// <param name="nRadix">Radix used in formatting numerical information in the enumerator.</param>
        /// <param name="ppEnum">Returns an IEnumDebugFrameInfo2 object that contains a list of FRAMEINFO structures describing the stack frame.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The thread's frames are enumerated in order, with the current frame enumerated first and the oldest frame enumerated last.</remarks>
        public virtual int EnumFrameInfo( enum_FRAMEINFO_FLAGS dwFieldSpec, uint nRadix, out IEnumDebugFrameInfo2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the name of a thread.
        /// </summary>
        /// <param name="pbstrName">Returns the name of the thread.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The retrieved name is always a name that can be displayed and this name describes the thread. The thread name might be derived from a run-time architecture that supports named threads, or it might be a name derived from the debug engine. Alternatively, the name of the thread can be set by a call to the IDebugThread2::SetThreadName method.</remarks>
        public virtual int GetName( out string pbstrName )
        {
            Logger.Debug( string.Empty );
            pbstrName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets the name of the thread.
        /// </summary>
        /// <param name="pszName">The name of the thread.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>To get the thread name, call the IDebugThread2::GetName method.</remarks>
        public virtual int SetThreadName( string pszName )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the program in which a thread is running.
        /// </summary>
        /// <param name="ppProgram">Returns an IDebugProgram2 object that represents the program this thread is running in.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetProgram( out IDebugProgram2 ppProgram )
        {
            Logger.Debug( string.Empty );
            ppProgram = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Determines whether the current instruction pointer can be set to the given stack frame.
        /// </summary>
        /// <param name="pStackFrame">Reserved for future use; set to a null value. If this is a null value, use the current stack frame.</param>
        /// <param name="pCodeContext">An IDebugCodeContext2 object that describes the code location about to be executed and its context.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>If this method returns S_OK, then call the IDebugThread2::SetNextStatement method to actually set the next statement.</remarks>
        public virtual int CanSetNextStatement( IDebugStackFrame2 pStackFrame, IDebugCodeContext2 pCodeContext )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets the current instruction pointer to the given code context.
        /// </summary>
        /// <param name="pStackFrame">Reserved for future use; set to a null value.</param>
        /// <param name="pCodeContext">An IDebugCodeContext2 object that describes the code location about to be executed and its context.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code. The following table shows other possible values.
        /// Value                                            Description
        /// E_CANNOT_SET_NEXT_STATEMENT_ON_NONLEAF_FRAME     The next statement cannot be in a stack frame deeper on the frame stack.
        /// E_CANNOT_SETIP_TO_DIFFERENT_FUNCTION             The next statement is not associated with any frame in the stack.
        /// E_CANNOT_SET_NEXT_STATEMENT_ON_EXCEPTION         Some debug engines cannot set the next statement after an exception.
        /// </returns>
        /// <remarks>The instruction pointer indicates the next instruction or statement to execute. This method is used to retry a line of source code or to force execution to continue in another function, for example.</remarks>
        public virtual int SetNextStatement( IDebugStackFrame2 pStackFrame, IDebugCodeContext2 pCodeContext )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the system thread identifier.
        /// </summary>
        /// <param name="pdwThreadId">Returns the system thread identifier.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A thread ID is used to identify a thread among all other threads in a process.</remarks>
        public virtual int GetThreadId( out uint pdwThreadId )
        {
            Logger.Debug( string.Empty );
            pdwThreadId = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Suspends a thread.
        /// </summary>
        /// <param name="pdwSuspendCount">Returns the suspend count after the suspend operation.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// Each call to this method increments the suspend count above 0. This suspend count is displayed in the Threads debug window.
        /// 
        /// For each call to this method, there must be a later call to the IDebugThread2::Resume method.
        /// </remarks>
        public virtual int Suspend( out uint pdwSuspendCount )
        {
            Logger.Debug( string.Empty );
            pdwSuspendCount = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Resumes execution of a thread.
        /// </summary>
        /// <param name="pdwSuspendCount">Returns the suspend count after the resume operation.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// Each call to this method decrements the suspend count until it reaches 0 at which time, execution is actually resumed. This suspend count is displayed in the Threads debug window.
        /// 
        /// For each call to this method, there must be a previous call to the IDebugThread2::Suspend method. The suspend count determines how many times the IDebugThread2::Suspend method has been called so far.
        /// </remarks>
        public virtual int Resume( out uint pdwSuspendCount )
        {
            Logger.Debug( string.Empty );
            pdwSuspendCount = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the properties that describe this thread.
        /// </summary>
        /// <param name="dwFields">A combination of flags from the THREADPROPERTY_FIELDS enumeration that determines which fields of ptp are to be filled in.</param>
        /// <param name="ptp">A THREADPROPERTIES structure that that is filled in with the properties of the thread.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The information returned from this method is typically shown in the Threads debug window.</remarks>
        public virtual int GetThreadProperties( enum_THREADPROPERTY_FIELDS dwFields, THREADPROPERTIES[] ptp )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Debug engines do not implement this method.
        /// </summary>
        /// <param name="pStackFrame">An IDebugStackFrame2 object that represents the stack frame.</param>
        /// <param name="ppLogicalThread">Returns an IDebugLogicalThread2 interface that represents the associated logical thread. A debug engine implementation should set this to a null value.</param>
        /// <returns>Debug engine implementations always return E_NOTIMPL.</returns>
        public virtual int GetLogicalThread( IDebugStackFrame2 pStackFrame, out IDebugLogicalThread2 ppLogicalThread )
        {
            Logger.Debug( string.Empty );
            ppLogicalThread = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugThread100

        public virtual int GetThreadProperties100( uint dwFields, THREADPROPERTIES100[] ptp )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        public virtual int GetFlags( out uint pFlags )
        {
            Logger.Debug( string.Empty );
            pFlags = 0;
            return VSConstants.E_NOTIMPL;
        }

        public virtual int SetFlags( uint flags )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        public virtual int CanDoFuncEval()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        public virtual int GetThreadDisplayName( out string bstrDisplayName )
        {
            Logger.Debug( string.Empty );
            bstrDisplayName = null;
            return VSConstants.E_NOTIMPL;
        }

        public virtual int SetThreadDisplayName( string bstrDisplayName )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}