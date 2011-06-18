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
using DebugEngine.Enumerators;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace DebugEngine.Events
{
    /// <summary>
    /// This interface is sent when a pending breakpoint has been bound in the debuggee.
    /// </summary>
    public class BreakpointBoundEvent : AsynchronousEvent, IDebugBreakpointBoundEvent2
    {
        public BreakpointBoundEvent( Breakpoint pendingBreakpoint, Breakpoint boundBreakpoint )
        {
            if ( pendingBreakpoint == null )
            {
                throw new ArgumentNullException( "pendingBreakpoint" );
            }

            if ( boundBreakpoint == null )
            {
                throw new ArgumentNullException( "boundBreakpoint" );
            }

            PendingBreakpoint = pendingBreakpoint;
            BoundBreakpoint = boundBreakpoint;
        }

        public Breakpoint PendingBreakpoint { get; private set; }
        public Breakpoint BoundBreakpoint { get; private set; }

        #region IDebugBreakpointBoundEvent2 Members

        /// <summary>
        /// Gets the pending breakpoint that is being bound.
        /// </summary>
        /// <param name="ppPendingBP">Returns the IDebugPendingBreakpoint2 object that represents the pending breakpoint being bound.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public int GetPendingBreakpoint( out IDebugPendingBreakpoint2 ppPendingBP )
        {
            ppPendingBP = PendingBreakpoint;
            return VSConstants.S_OK;
        }

        /// <summary>
        /// Creates an enumerator of breakpoints that were bound on this event.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugBoundBreakpoints2 object that enumerates all the breakpoints bound from this event.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if there are no bound breakpoints; otherwise, returns an error code.</returns>
        /// <remarks>The list of bound breakpoints is for those bound to this event and might not be the entire list of breakpoints bound from a pending breakpoint. To get a list of all breakpoints bound to a pending breakpoint, call the IDebugBreakpointBoundEvent2::GetPendingBreakpoint method to get the associated IDebugPendingBreakpoint2 object and then call the IDebugPendingBreakpoint2::EnumBoundBreakpoints method to get an IEnumDebugBoundBreakpoints2 object which contains all the bound breakpoints for the pending breakpoint.</remarks>
        public int EnumBoundBreakpoints( out IEnumDebugBoundBreakpoints2 ppEnum )
        {
            var boundBreakpoints = new IDebugBoundBreakpoint2[1];
            boundBreakpoints[0] = BoundBreakpoint;
            ppEnum = new BoundBreakpointsEnumerator( boundBreakpoints );
            return VSConstants.S_OK;
        }

        #endregion
    }
}