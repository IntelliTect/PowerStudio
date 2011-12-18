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
    public class ModuleEnumerator : EnumeratorBase<IDebugModule2, IEnumDebugModules2>, IEnumDebugModules2
    {
        public ModuleEnumerator( IDebugModule2[] modules )
                : base( modules )
        {
        }

        #region IEnumDebugModules2 Members

        public virtual int Next( uint celt, IDebugModule2[] rgelt, ref uint celtFetched )
        {
            return Next( celt, rgelt, out celtFetched );
        }

        #endregion
    }
}