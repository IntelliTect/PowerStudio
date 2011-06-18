#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace DebugEngine.Enumerators
{
    public class PendingBreakpointsEnumerator : EnumeratorBase<IDebugPendingBreakpoint2, IEnumDebugPendingBreakpoints2>,
                                                IEnumDebugPendingBreakpoints2
    {
        public PendingBreakpointsEnumerator( IDebugPendingBreakpoint2[] breakpoints )
                : base( breakpoints )
        {
        }

        #region IEnumDebugPendingBreakpoints2 Members

        public virtual int Next( uint celt, IDebugPendingBreakpoint2[] rgelt, ref uint celtFetched )
        {
            return Next( celt, rgelt, out celtFetched );
        }

        #endregion
    }
}