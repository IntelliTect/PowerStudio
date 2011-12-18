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

#endregion

namespace PowerStudio.DebugEngine.Events
{
    /// <summary>
    /// This interface is sent by the debug engine (DE) to the session debug manager (SDM) to output a string for debug tracing.
    /// </summary>
    internal sealed class OutputDebugStringEvent : AsynchronousEvent, IDebugOutputStringEvent2
    {
        public OutputDebugStringEvent( string value )
        {
            Value = value;
        }

        public string Value { get; private set; }

        #region IDebugOutputStringEvent2 Members

        /// <summary>
        /// Gets the displayable message.
        /// </summary>
        /// <param name="pbstrString">Returns the displayable message.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public int GetString( out string pbstrString )
        {
            pbstrString = Value;
            return VSConstants.S_OK;
        }

        #endregion
    }
}