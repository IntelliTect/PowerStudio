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
using System.IO;
using System.Runtime.InteropServices;
using PowerStudio.DebugEngine;
using PowerStudio.DebugEngine.PortSuppliers;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell.Interop;
using NLog;
using PowerStudio.Debugger;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Project
{
    public class PowerShellProjectConfig : ProjectConfig
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellProjectConfig( ProjectNode project, string configuration )
                : base( project, configuration )
        {
            Logger.Debug( string.Empty );
        }

#if DEBUG
        public override int DebugLaunch( uint grfLaunch )
        {
            Logger.Debug( string.Empty );

            var debugger = ProjectMgr.Site.GetService( typeof (SVsShellDebugger) ) as IVsDebugger;
            var shell = ProjectMgr.Site.GetService( typeof (SVsUIShell) ) as IVsUIShell;

            var info = new VsDebugTargetInfo();

            info.dlo = DEBUG_LAUNCH_OPERATION.DLO_CreateProcess;

            info.bstrExe = @"C:\script.ps1";
            info.bstrCurDir = Path.GetDirectoryName( info.bstrExe );
            info.bstrArg = null;
            info.bstrRemoteMachine = null;
            info.fSendStdoutToOutputWindow = 1;
            info.clsidCustom = EngineGuids.PowerStudioEngineGuid;
            info.clsidPortSupplier = typeof (DefaultPortSupplier).GUID;
            info.grfLaunch = 0; //grfLaunch;
            info.cbSize = (uint) Marshal.SizeOf( info );
            IntPtr pInfo = Marshal.AllocCoTaskMem( (int) info.cbSize );

            try
            {
                Marshal.StructureToPtr( info, pInfo, false );
                var eventManager = new PowerShellDebuggerEvents();

                if ( debugger.AdviseDebugEventCallback( eventManager ) !=
                     VSConstants.S_OK )
                {
                    Trace.WriteLine( "Failed to advise the UI of debug events." );
                    if ( pInfo != IntPtr.Zero )
                    {
                        Marshal.FreeCoTaskMem( pInfo );
                    }
                    return VSConstants.E_UNEXPECTED;
                }

                debugger.LaunchDebugTargets( 1, pInfo );
                //VsShellUtilities.LaunchDebugger(ProjectMgr.Site, info);

                string message;
                shell.GetErrorInfo( out message );

                if ( !String.IsNullOrWhiteSpace( message ) )
                {
                    Logger.Error( message );
                }
            }
            finally
            {
                if ( pInfo != IntPtr.Zero )
                {
                    Marshal.FreeCoTaskMem( pInfo );
                }
            }

            return VSConstants.S_OK;
        }
#endif

        /// <summary>
        /// Determines whether the debugger can be launched, given the state of the launch flags.
        /// </summary>
        /// <param name="flags">Flags that determine the conditions under which to launch the debugger.
        /// For valid grfLaunch values, see __VSDBGLAUNCHFLAGS or __VSDBGLAUNCHFLAGS2.</param>
        /// <param name="fCanLaunch">true if the debugger can be launched, otherwise false</param>
        /// <returns>
        /// S_OK if the method succeeds, otherwise an error code
        /// </returns>
        public override int QueryDebugLaunch( uint flags, out int fCanLaunch )
        {
            Logger.Debug( string.Empty );
            fCanLaunch = 1;
            return VSConstants.S_OK;
        }
    }
}