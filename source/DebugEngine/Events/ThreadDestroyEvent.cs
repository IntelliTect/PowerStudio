#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace DebugEngine.Events
{
    /// <summary>
    /// This interface is sent by the debug engine (DE) to the session debug manager (SDM) when a thread has exited.
    /// </summary>
    public class ThreadDestroyEvent : AsynchronousEvent, IDebugThreadDestroyEvent2
    {
        public ThreadDestroyEvent( uint exitCode )
        {
            ExitCode = exitCode;
        }

        public uint ExitCode { get; private set; }

        #region IDebugThreadDestroyEvent2 Members

        /// <summary>
        /// Gets the exit code for a thread.
        /// </summary>
        /// <param name="exitCode">Returns the thread's exit code.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetExitCode( out uint exitCode )
        {
            exitCode = ExitCode;
            return VSConstants.S_OK;
        }

        #endregion
    }
}