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
using Microsoft.VisualStudio.Shell.Interop;
using NLog;

#endregion

namespace PowerStudio.Debugger
{
    public class PowerShellDebuggerEvents : IVsDebuggerEvents, IDebugEventCallback2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IVsDebuggerEvents

        public int OnModeChange( DBGMODE dbgmodeNew )
        {
            Logger.Debug( string.Empty );
            return VSConstants.S_OK;
        }

        #endregion

        #region Implementation of IDebugEventCallback2

        public int Event( IDebugEngine2 pEngine,
                          IDebugProcess2 pProcess,
                          IDebugProgram2 pProgram,
                          IDebugThread2 pThread,
                          IDebugEvent2 pEvent,
                          ref Guid riidEvent,
                          uint dwAttrib )
        {
            Logger.Debug( string.Empty );

            return VSConstants.S_OK;
        }

        #endregion
    }
}