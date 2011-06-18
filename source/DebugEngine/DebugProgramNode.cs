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
    public abstract class DebugProgramNode : IDebugProgramNode2
    {
        #region Implementation of IDebugProgramNode2

        /// <summary>
        /// Gets the name of the program.
        /// </summary>
        /// <param name="pbstrProgramName">Returns the name of the program.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The name of a program is not the same thing as the path to the program, although the name of the program may be part of such a path.</remarks>
        public virtual int GetProgramName( out string pbstrProgramName )
        {
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
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}