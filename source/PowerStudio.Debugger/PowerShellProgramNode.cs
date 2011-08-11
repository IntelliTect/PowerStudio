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
using IntelliTect.DebugEngine;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.Debugger
{
    public class PowerShellProgramNode : DebugProgramNode
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public PowerShellProgramNode( PowerShellProcess process )
        {
            Logger.Debug( string.Empty );
            Process = process;
        }

        public PowerShellProcess Process { get; private set; }

        /// <summary>
        /// Gets the name and identifier of the debug engine (DE) running a program.
        /// </summary>
        /// <param name="pbstrEngine">Returns the name of the DE running the program (C++-specific: this can be a null pointer indicating that the caller is not interested in the name of the engine).</param>
        /// <param name="pguidEngine">Returns the globally unique identifier of the DE running the program (C++-specific: this can be a null pointer indicating that the caller is not interested in the GUID of the engine).</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int GetEngineInfo( out string pbstrEngine, out Guid pguidEngine )
        {
            Logger.Debug( string.Empty );
            pbstrEngine = Properties.Resources.EngineName;
            pguidEngine = EngineGuids.PowerStudioEngineGuid;

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Gets the system process identifier for the process hosting the program.
        /// </summary>
        /// <param name="pHostProcessId">Returns the system process identifier for the hosting process.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int GetHostPid( AD_PROCESS_ID[] pHostProcessId )
        {
            Logger.Debug( string.Empty );
            pHostProcessId[0].ProcessIdType = (uint) enum_AD_PROCESS_ID.AD_PROCESS_ID_GUID;
            pHostProcessId[0].guidProcessId = Process.Id;

            return VSConstants.S_OK;
        }
    }
}