#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace DebugEngine.Events
{
    public abstract class DebugEventBase : IDebugEvent2
    {
        public enum_EVENTATTRIBUTES Attributes { get; protected set; }

        #region Implementation of IDebugEvent2

        /// <summary>
        /// Gets the attributes for this debug event.
        /// </summary>
        /// <param name="pdwAttrib">A combination of flags from the EVENTATTRIBUTES enumeration.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// The IDebugEvent2 interface is common to all events. This method describes the type of event; for example,
        /// is the event synchronous or asynchronous and is it a stopping event.
        /// </remarks>
        public virtual int GetAttributes( out uint pdwAttrib )
        {
            pdwAttrib = (uint) Attributes;
            return VSConstants.S_OK;
        }

        #endregion
    }
}