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

namespace DebugEngine.Events
{
    /// <summary>
    /// This interface is sent by the debug engine (DE) to the session debug manager (SDM) when a module is loaded or unloaded.
    /// </summary>
    public class ModuleLoadEvent : AsynchronousEvent, IDebugModuleLoadEvent2
    {
        public ModuleLoadEvent( DebugModuleBase module, bool isLoading )
        {
            Module = module;
            IsLoading = isLoading;
        }

        public bool IsLoading { get; private set; }

        public DebugModuleBase Module { get; private set; }

        #region IDebugModuleLoadEvent2 Members

        /// <summary>
        /// Gets the module that is being loaded or unloaded.
        /// </summary>
        /// <param name="module">Returns an IDebugModule2 object that represents the module which is loading or unloading.</param>
        /// <param name="debugMessage">Returns an optional message describing this event. If this parameter is a null value, no message is requested.</param>
        /// <param name="fIsLoad">Nonzero (TRUE) if the module is loading and zero (FALSE) if the module is unloading. If this parameter is a null value, no status is requested.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetModule( out IDebugModule2 module, ref string debugMessage, ref int fIsLoad )
        {
            module = Module;

            if ( IsLoading )
            {
                debugMessage = String.Format( "Loaded '{0}'", Module.Name );
                fIsLoad = 1;
            }
            else
            {
                debugMessage = String.Concat( "Unloaded '{0}'", Module.Name );
                fIsLoad = 0;
            }

            return VSConstants.S_OK;
        }

        #endregion
    }
}