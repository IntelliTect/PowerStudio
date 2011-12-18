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
    public class PropertyEnumerator : EnumeratorBase<DEBUG_PROPERTY_INFO, IEnumDebugPropertyInfo2>,
                                      IEnumDebugPropertyInfo2
    {
        public PropertyEnumerator( DEBUG_PROPERTY_INFO[] properties )
                : base( properties )
        {
        }
    }
}