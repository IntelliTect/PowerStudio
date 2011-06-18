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

namespace DebugEngine.Events
{
    public abstract class SynchronousEvent : DebugEventBase
    {
        protected SynchronousEvent()
        {
            Attributes = enum_EVENTATTRIBUTES.EVENT_SYNCHRONOUS;
        }
    }
}