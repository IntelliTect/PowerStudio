#region License

// 
// Copyright (c) 2011, PowerStudio.DebugEngine Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    public class Breakpoint : IDebugBreakpointResolution2,
                              IDebugBoundBreakpoint2,
                              IEnumDebugBoundBreakpoints2,
                              IDebugPendingBreakpoint2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugBreakpointResolution2

        /// <summary>
        /// Gets the type of the breakpoint represented by this resolution.
        /// </summary>
        /// <param name="pBPType">Returns a value from the BP_TYPE enumeration that specifies the type of this breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise returns an error code. Returns E_FAIL if the bpResLocation field in the associated BP_RESOLUTION_INFO structure is not valid.</returns>
        /// <remarks>The breakpoint may be a code or a data breakpoint, for example.</remarks>
        public virtual int GetBreakpointType( enum_BP_TYPE[] pBPType )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the breakpoint resolution information that describes this breakpoint.
        /// </summary>
        /// <param name="dwFields">A combination of flags from the BPRESI_FIELDS enumeration that determine which fields of the pBPResolutionInfo parameter are to be filled out.</param>
        /// <param name="pBPResolutionInfo">The BP_RESOLUTION_INFO structure to be filled in with information about this breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise returns an error code.</returns>
        public virtual int GetResolutionInfo( enum_BPRESI_FIELDS dwFields, BP_RESOLUTION_INFO[] pBPResolutionInfo )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugPendingBreakpoint2

        /// <summary>
        /// Determines whether this pending breakpoint can bind to a code location.
        /// </summary>
        /// <param name="ppErrorEnum">Returns an IEnumDebugErrorBreakpoints2 object that contains a list of IDebugErrorBreakpoint2 objects if there could be errors.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if the breakpoint cannot bind, in which case the errors are returned by the ppErrorEnum parameter. Otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        /// <remarks>This method is called to determine what would happen if this pending breakpoint was bound. Call the IDebugPendingBreakpoint2::Bind method to actually bind the pending breakpoint.</remarks>
        public virtual int CanBind( out IEnumDebugErrorBreakpoints2 ppErrorEnum )
        {
            Logger.Debug( string.Empty );
            ppErrorEnum = null;
            return VSConstants.S_FALSE;
        }

        /// <summary>
        /// Binds this pending breakpoint to one or more code locations.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        /// <remarks>
        /// When this method is called, a debug engine (DE) should attempt to bind this pending breakpoint to all code locations that match.
        /// 
        /// After this method returns, the caller needs to wait for events indicating that the pending breakpoint has bound or is in error before assuming that calls to the IDebugPendingBreakpoint2::EnumBoundBreakpoints or IDebugPendingBreakpoint2::EnumErrorBreakpoints.methods will enumerate all bound or error breakpoints, respectively.
        /// </remarks>
        public virtual int Bind()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the state of the pending breakpoint.
        /// </summary>
        /// <param name="pState">A PENDING_BP_STATE_INFO structure that is filled in with a description of this pending breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetState( PENDING_BP_STATE_INFO[] pState )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the breakpoint request that was used to create this pending breakpoint.
        /// </summary>
        /// <param name="ppBPRequest">Returns an IDebugBreakpointRequest2 object representing the breakpoint request that was used to create this pending breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        public virtual int GetBreakpointRequest( out IDebugBreakpointRequest2 ppBPRequest )
        {
            Logger.Debug( string.Empty );
            ppBPRequest = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Toggles the virtualized state of this pending breakpoint. When a pending breakpoint is virtualized, the debug engine will attempt to bind it every time new code loads into the program.
        /// </summary>
        /// <param name="fVirtualize">Set to nonzero (TRUE) to virtualize the pending breakpoint, or to zero (FALSE) to turn off virtualization.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        /// <remarks>A virtualized breakpoint is bound every time code is loaded.</remarks>
        public virtual int Virtualize( int fVirtualize )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Enumerates all breakpoints bound from this pending breakpoint.
        /// </summary>
        /// <param name="ppEnum">Returns an IEnumDebugBoundBreakpoints2 object that enumerates the bound breakpoints.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        public virtual int EnumBoundBreakpoints( out IEnumDebugBoundBreakpoints2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets a list of all error breakpoints that resulted from this pending breakpoint.
        /// </summary>
        /// <param name="bpErrorType">A combination of values from the BP_ERROR_TYPE enumeration that selects the type of errors to enumerate.</param>
        /// <param name="ppEnum">Returns an IEnumDebugErrorBreakpoints2 object that contains a list of IDebugErrorBreakpoint2 objects.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        public virtual int EnumErrorBreakpoints( enum_BP_ERROR_TYPE bpErrorType, out IEnumDebugErrorBreakpoints2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IEnumDebugBoundBreakpoints2

        /// <summary>
        /// Returns the next set of elements from the enumeration.
        /// </summary>
        /// <param name="celt">The number of elements to retrieve. Also specifies the maximum size of the rgelt array.</param>
        /// <param name="rgelt">Array of IDebugBoundBreakpoint2 elements to be filled in.</param>
        /// <param name="pceltFetched">Returns the number of elements actually returned in rgelt.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if fewer than the requested number of elements could be returned; otherwise, returns an error code.</returns>
        public virtual int Next( uint celt, IDebugBoundBreakpoint2[] rgelt, ref uint pceltFetched )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Skips over the specified number of elements.
        /// </summary>
        /// <param name="celt">Number of elements to skip.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if celt is greater than the number of remaining elements; otherwise, returns an error code.</returns>
        /// <remarks>If celt specifies a value greater than the number of remaining elements, the enumeration is set to the end and S_FALSE is returned.</remarks>
        public virtual int Skip( uint celt )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Resets the enumeration to the first element.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>After this method is called, the next call to the IEnumDebugBoundBreakpoints2::Next method returns the first element of the enumeration.</remarks>
        public virtual int Reset()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Returns a copy of the current enumeration as a separate object.
        /// </summary>
        /// <param name="ppEnum">Returns a copy of this enumeration as a separate object.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The copy of the enumeration has the same state as the original at the time this method is called. However, the copy's and the original's states are separate and can be changed individually.</remarks>
        public virtual int Clone( out IEnumDebugBoundBreakpoints2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Returns the number of elements in the enumeration.
        /// </summary>
        /// <param name="pcelt">Returns the number of elements in the enumeration.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method is not part of the customary COM enumeration interface which specifies that only the Next, Clone, Skip, and Reset methods need to be implemented.</remarks>
        public virtual int GetCount( out uint pcelt )
        {
            Logger.Debug( string.Empty );
            pcelt = 0;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugBoundBreakpoint2

        /// <summary>
        /// Gets the pending breakpoint from which the specified bound breakpoint was created.
        /// </summary>
        /// <param name="ppPendingBreakpoint">Returns the IDebugPendingBreakpoint2 object that represents the pending breakpoint that was used to create this bound breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A pending breakpoint can be thought of as a collection of all the necessary information needed to bind a breakpoint to code that can be applied to one or many programs.</remarks>
        public virtual int GetPendingBreakpoint( out IDebugPendingBreakpoint2 ppPendingBreakpoint )
        {
            Logger.Debug( string.Empty );
            ppPendingBreakpoint = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the state of this bound breakpoint.
        /// </summary>
        /// <param name="pState">Returns a value from the BP_STATE enumeration that describes the state of the breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetState( enum_BP_STATE[] pState )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the current hit count for this bound breakpoint.
        /// </summary>
        /// <param name="pdwHitCount">Returns the hit count.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the state of the bound breakpoint object is set to BPS_DELETED (part of the BP_STATE enumeration).</returns>
        public virtual int GetHitCount( out uint pdwHitCount )
        {
            Logger.Debug( string.Empty );
            pdwHitCount = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the pending breakpoint from which the specified bound breakpoint was created.
        /// </summary>
        /// <param name="ppBPResolution">Returns the IDebugPendingBreakpoint2 object that represents the pending breakpoint that was used to create this bound breakpoint.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A pending breakpoint can be thought of as a collection of all the necessary information needed to bind a breakpoint to code that can be applied to one or many programs.</remarks>
        public virtual int GetBreakpointResolution( out IDebugBreakpointResolution2 ppBPResolution )
        {
            Logger.Debug( string.Empty );
            ppBPResolution = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Toggles the enabled state of the pending breakpoint.
        /// </summary>
        /// <param name="fEnable">Set to nonzero (TRUE) to enable a pending breakpoint, or to zero (FALSE) to disable.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        /// <remarks>
        /// When a pending breakpoint is enabled or disabled, all breakpoints bound from it are set to the same state.
        /// This method may be called as many times as necessary, even if the breakpoint is already enabled or disabled.
        /// </remarks>
        public virtual int Enable( int fEnable )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets the hit count for the bound breakpoint.
        /// </summary>
        /// <param name="dwHitCount">The hit count to set.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the state of the bound breakpoint object is set to BPS_DELETED (part of the BP_STATE enumeration).</returns>
        public virtual int SetHitCount( uint dwHitCount )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets or changes the condition associated with the pending breakpoint.
        /// </summary>
        /// <param name="bpCondition"> A BP_CONDITION structure that specifies the condition to set.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>Any condition that was previously associated with the pending breakpoint is lost. All breakpoints bound from this pending breakpoint are called to set their condition to the value specified in the bpCondition parameter.</remarks>
        public virtual int SetCondition( BP_CONDITION bpCondition )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets or changes the pass count associated with the pending breakpoint.
        /// </summary>
        /// <param name="bpPassCount">A BP_PASSCOUNT structure that contains the pass count.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        /// <remarks>Any pass count that was previously associated with the pending breakpoint is lost. All breakpoints bound from this pending breakpoint are called to set their pass count to the bpPassCount parameter.</remarks>
        public virtual int SetPassCount( BP_PASSCOUNT bpPassCount )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Deletes this pending breakpoint and all breakpoints bound from it.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_BP_DELETED if the breakpoint has been deleted.</returns>
        public virtual int Delete()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}