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
    /// This interface is sent by the debug engine (DE) to indicate the results of searching for symbols for a module in the debuggee
    /// </summary>
    public class SymbolSearchEvent : AsynchronousEvent, IDebugSymbolSearchEvent2
    {
        public SymbolSearchEvent( DebugModuleBase module, string debugMessage, enum_MODULE_INFO_FLAGS symbolFlags )
        {
            Module = module;
            DebugMessage = debugMessage;
            ModuleInfoFlags = symbolFlags;
        }

        public DebugModuleBase Module { get; private set; }
        public string DebugMessage { get; private set; }
        public enum_MODULE_INFO_FLAGS ModuleInfoFlags { get; private set; }

        #region IDebugSymbolSearchEvent2 Members

        /// <summary>
        /// Called by an event handler to retrieve results about a symbol load process.
        /// </summary>
        /// <param name="pModule">An IDebugModule3 object representing the module for which the symbols were loaded.</param>
        /// <param name="pbstrDebugMessage">Returns a string containing any error messages from the module. If there is no error, then this string will just contain the module's name but it is never empty.</param>
        /// <param name="pdwModuleInfoFlags">A combination of flags from the MODULE_INFO_FLAGS enumeration indicating whether any symbols were loaded.</param>
        /// <returns>If successful, returns S_OK; otherwise returns an error code.</returns>
        /// <remarks>When a handler receives the IDebugSymbolSearchEvent2 event after an attempt is made to load debugging symbols for a module, the handler can call this method to determine the results of that load.</remarks>
        public virtual int GetSymbolSearchInfo( out IDebugModule3 pModule,
                                                ref string pbstrDebugMessage,
                                                enum_MODULE_INFO_FLAGS[] pdwModuleInfoFlags )
        {
            pModule = Module;
            pbstrDebugMessage = DebugMessage;
            pdwModuleInfoFlags[0] = ModuleInfoFlags;

            return VSConstants.S_OK;
        }

        #endregion
    }
}