#region License

// 
// Copyright (c) 2011, PowerStudio.DebugEngine Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace PowerStudio.DebugEngine.Enumerators
{
    public class FrameInfoEnumerator : EnumeratorBase<FRAMEINFO, IEnumDebugFrameInfo2>, IEnumDebugFrameInfo2
    {
        public FrameInfoEnumerator( FRAMEINFO[] threads )
                : base( threads )
        {
        }

        #region IEnumDebugFrameInfo2 Members

        public virtual int Next( uint celt, FRAMEINFO[] rgelt, ref uint celtFetched )
        {
            return Next( celt, rgelt, out celtFetched );
        }

        #endregion
    }
}