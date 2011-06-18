#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace PowerStudio.Debugger.Events
{
    public class DebugEventBase : IDebugEvent2
    {
        #region Implementation of IDebugEvent2

        /// <summary>
        /// Gets the attributes for this debug event.
        /// </summary>
        /// <param name="pdwAttrib">A combination of flags from the EVENTATTRIBUTES enumeration.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The IDebugEvent2 interface is common to all events. This method describes the type of event; for example, is the event synchronous or asynchronous and is it a stopping event.</remarks>
        public int GetAttributes( out uint pdwAttrib )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}