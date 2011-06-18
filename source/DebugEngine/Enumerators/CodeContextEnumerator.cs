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
    public class CodeContextEnumerator : EnumeratorBase<IDebugCodeContext2, IEnumDebugCodeContexts2>,
                                         IEnumDebugCodeContexts2
    {
        public CodeContextEnumerator( IDebugCodeContext2[] codeContexts )
                : base( codeContexts )
        {
        }

        #region IEnumDebugCodeContexts2 Members

        public virtual int Next( uint celt, IDebugCodeContext2[] rgelt, ref uint celtFetched )
        {
            return Next( celt, rgelt, out celtFetched );
        }

        #endregion
    }
}