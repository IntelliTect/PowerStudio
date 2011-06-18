﻿#region License

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
    /// This interface is sent by the debug DebugEngine (DE) to the session debug manager (SDM) when a program is attached to.
    /// </summary>
    public class ProgramCreateEvent : AsynchronousEvent, IDebugProgramCreateEvent2
    {
    }
}