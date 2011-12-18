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

#endregion

namespace PowerStudio.DebugEngine.Events
{
    /// <summary>
    /// The debug engine (DE) sends this interface to the session debug manager (SDM) to set the status bar message during symbol loads.
    /// </summary>
    public class BeforeSymbolSearchEvent : AsynchronousEvent, IDebugBeforeSymbolSearchEvent2
    {
        public BeforeSymbolSearchEvent( string moduleName )
        {
            if ( string.IsNullOrEmpty( moduleName ) )
            {
                throw new ArgumentNullException( "moduleName" );
            }
            ModuleName = moduleName;
        }

        #region Implementation of IDebugBeforeSymbolSearchEvent2

        /// <summary>
        /// Retrieves the name of the module currently being debugged.
        /// </summary>
        /// <param name="pbstrModuleName">Name of the module.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public int GetModuleName( out string pbstrModuleName )
        {
            pbstrModuleName = ModuleName;
            return VSConstants.S_OK;
        }

        #endregion

        public string ModuleName { get; private set; }
    }
}