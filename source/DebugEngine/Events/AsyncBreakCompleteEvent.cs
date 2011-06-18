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
    /// <summary>
    /// This interface tells the session debug manager (SDM) that an asynchronous break has been successfully completed.
    /// </summary>
    public class AsyncBreakCompleteEvent : StoppingEvent, IDebugBreakEvent2
    {
    }
}