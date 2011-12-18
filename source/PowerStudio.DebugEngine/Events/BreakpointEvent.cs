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
    /// This Event is sent when a breakpoint is hit in the debuggee
    /// </summary>
    public class BreakpointEvent : StoppingEvent, IDebugBreakpointEvent2
    {
        public BreakpointEvent( IEnumDebugBoundBreakpoints2 boundBreakpoints )
        {
            if ( boundBreakpoints == null )
            {
                throw new ArgumentNullException( "boundBreakpoints" );
            }
            BoundBreakpoints = boundBreakpoints;
        }

        public IEnumDebugBoundBreakpoints2 BoundBreakpoints { get; private set; }

        #region IDebugBreakpointEvent2 Members

        /// <summary>
        /// Creates an enumerator for all the breakpoints that fired at the current code location.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugBoundBreakpoints2 object that enumerates all the breakpoints associated with the current code location.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>Not all breakpoints at a particular location may fire at a particular time (for example, a breakpoint with a condition will not fire until that condition is met).</remarks>
        public virtual int EnumBreakpoints( out IEnumDebugBoundBreakpoints2 ppEnum )
        {
            ppEnum = BoundBreakpoints;
            return VSConstants.S_OK;
        }

        #endregion
    }
}