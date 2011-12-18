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
    public class ProgramEnumerator : EnumeratorBase<IDebugProgram2, IEnumDebugPrograms2>, IEnumDebugPrograms2
    {
        public ProgramEnumerator( IDebugProgram2[] data )
                : base( data )
        {
        }

        #region IEnumDebugPrograms2 Members

        public virtual int Next( uint celt, IDebugProgram2[] rgelt, ref uint celtFetched )
        {
            return Next( celt, rgelt, out celtFetched );
        }

        #endregion
    }
}