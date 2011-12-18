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
    /// The debug DebugEngine (DE) sends this interface to the session debug manager (SDM) when an instance of the DE is created.
    /// </summary>
    public class DebugEngineCreateEvent : AsynchronousEvent, IDebugEngineCreateEvent2
    {
        public DebugEngineCreateEvent( DebugEngineBase debugEngineBase )
        {
            if ( debugEngineBase == null )
            {
                throw new ArgumentNullException( "debugEngineBase" );
            }
            Engine = debugEngineBase;
        }

        private IDebugEngine2 Engine { get; set; }

        #region Implementation of IDebugEngineCreateEvent2

        /// <summary>
        /// Retrieves the object that represents the newly created debug engine (DE).
        /// </summary>
        /// <param name="pEngine">Returns an IDebugEngine2 object that represents the newly created DE.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public int GetEngine( out IDebugEngine2 pEngine )
        {
            pEngine = Engine;
            return VSConstants.S_OK;
        }

        #endregion
    }
}