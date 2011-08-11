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
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.Debugger
{
    [ComVisible( true )]
    [Guid( EngineGuids.PowerStudioEngineGuidString )]
    public class PowerShellDebugEngine : DebugEngineBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellProcess Process { get; private set; }
        public PowerShellProgramNode Program { get; private set; }

        /// <summary>
        /// Attaches a debug DebugEngine (DE) to a program or programs. Called by the session debug manager (SDM) when the DE is running in-process to the SDM.
        /// </summary>
        /// <param name="rgpPrograms">An array of IDebugProgram2 objects that represent programs to be attached to. These are port programs.</param>
        /// <param name="rgpProgramNodes">An array of IDebugProgramNode2 objects that represent program nodes, one for each program. The program nodes in this array represent the same programs as in pProgram. The program nodes are given so that the DE can identify the programs to attach to.</param>
        /// <param name="celtPrograms">Number of programs and/or program nodes in the pProgram and rgpProgramNodes arrays.</param>
        /// <param name="pCallback">The IDebugEventCallback2 object to be used to send debug events to the SDM.</param>
        /// <param name="dwReason">A value from the ATTACH_REASON enumeration that specifies the reason for attaching these programs. For more information, see the Remarks section.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int Attach( IDebugProgram2[] rgpPrograms,
                                    IDebugProgramNode2[] rgpProgramNodes,
                                    uint celtPrograms,
                                    IDebugEventCallback2 pCallback,
                                    enum_ATTACH_REASON dwReason )
        {
            Logger.Debug( string.Empty );
            if ( celtPrograms != 1 )
            {
                Debug.Fail( "PowerStudio Engine only expects to see one program in a process." );
                throw new ArgumentException( "celtPrograms" );
            }

            CreateOrInitializeEventSource( pCallback );
            Program = new PowerShellProgramNode( Process );

            EngineEventSource.OnDebugEngineCreate();
            EngineEventSource.OnProgramCreate( Program );
            EngineEventSource.OnLoadComplete();

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Gets the GUID of the debug DebugEngine (DE).
        /// </summary>
        /// <param name="pguidEngine">Returns the GUID of the DE.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int GetEngineId( out Guid pguidEngine )
        {
            Logger.Debug( string.Empty );
            pguidEngine = EngineGuids.PowerStudioEngineGuid;
            return VSConstants.S_OK;
        }

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
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int LaunchSuspended( string pszServer,
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
            Process = new PowerShellProcess( pPort );

            ppProcess = Process;
            CreateOrInitializeEventSource( pCallback );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Resumes process execution.
        /// </summary>
        /// <param name="pProcess">An IDebugProcess2 object that represents the process to be resumed.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise returns an error code.
        /// </returns>
        public override int ResumeProcess( IDebugProcess2 pProcess )
        {
            Logger.Debug( string.Empty );
            var process = pProcess as PowerShellProcess;
            if ( process == null )
            {
                return VSConstants.E_UNEXPECTED;
            }

            IDebugPort2 port;
            pProcess.GetPort( out port );

            var defaultPort = (IDebugDefaultPort2) port;

            IDebugPortNotify2 portNotify;
            defaultPort.GetPortNotify( out portNotify );

            portNotify.AddProgramNode( Program );

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Creates the or initialize event source.
        /// </summary>
        /// <param name="pCallback">The p callback.</param>
        protected virtual void CreateOrInitializeEventSource( IDebugEventCallback2 pCallback )
        {
            Logger.Debug( string.Empty );
            if ( EngineEventSource == null )
            {
                EngineEventSource = new DebugEngineEventSource( this, pCallback );
            }
        }
    }
}