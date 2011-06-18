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
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace DebugEngine
{
    public abstract class DebugModuleBase : IDebugModule3
    {
        public abstract string Name { get; }

        #region Implementation of IDebugModule2

        /// <summary>
        /// Gets information about this module.
        /// </summary>
        /// <param name="dwFields"> A combination of flags from the MODULE_INFO_FIELDS enumeration that specify which fields of pInfo are to be filled out.</param>
        /// <param name="pinfo">A MODULE_INFO structure that is filled in with a description of the module.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The MODULE_INFO structure contains the name of the module that is displayed in the Modules window.</remarks>
        public virtual int GetInfo( enum_MODULE_INFO_FIELDS dwFields, MODULE_INFO[] pinfo )
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// OBSOLETE. DO NOT USE. Reloads the symbols for this module.
        /// </summary>
        /// <param name="pszUrlToSymbols">The path to the symbol store.</param>
        /// <param name="pbstrDebugMessage">Returns an informational message, such as a status or error message, that is displayed to the right of the module name in the Modules window.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. A debug engine should always return E_FAIL.</returns>
        /// <remarks>This method is no longer supported. Implement the IDebugModule3::LoadSymbols method instead.</remarks>
        [Obsolete( "This method is no longer supported. Implement the IDebugModule3::LoadSymbols method instead.", true )]
        public int ReloadSymbols_Deprecated( string pszUrlToSymbols, out string pbstrDebugMessage )
        {
            pbstrDebugMessage = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugModule3

        /// <summary>
        /// Retrieves a list of paths that are searched for symbols as well as the results of searching each path.
        /// </summary>
        /// <param name="dwFields">A combination of flags from the SYMBOL_SEARCH_INFO_FIELDS enumeration specifying which fields of pInfo are to be filled in.</param>
        /// <param name="pinfo">A MODULE_SYMBOL_SEARCH_INFO structure whose members are to be filled in with the specified information. If this is a null value, this method returns E_INVALIDARG.</param>
        /// <returns>
        /// If the method succeeds, it returns S_OK; otherwise, it returns an error code.
        /// Note: The returned string (in the MODULE_SYMBOL_SEARCH_INFO structure) could be empty even if S_OK is returned. In this case, there was no search information to return.
        /// </returns>
        /// <remarks>
        /// If the bstrVerboseSearchInfo field of the MODULE_SYMBOL_SEARCH_INFO structure is not empty, then it contains a list of paths searched and the results of that search. The list is formatted with a path, followed by ellipses ("..."), followed by the result. If there is more than one path result pair, then each pair is separated by a "\r\n" (carriage-return/linefeed) pair. The pattern looks like this:
        /// <code>
        ///     {path}...{result}\r\n{path}...{result}\r\n{path}...{result}
        /// </code>
        /// Note that the last entry does not have a \r\n sequence.
        /// </remarks>
        public virtual int GetSymbolInfo( enum_SYMBOL_SEARCH_INFO_FIELDS dwFields, MODULE_SYMBOL_SEARCH_INFO[] pinfo )
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Loads the symbols for the current module.
        /// </summary>
        /// <returns>If the method succeeds, it returns S_OK. If it fails, it returns an error code.</returns>
        /// <remarks>
        /// This method loads the symbols from the current search path (which can be altered by calling the IDebugEngine3::SetSymbolPath method).
        /// 
        /// This method is preferred over the IDebugModule2::ReloadSymbols_Deprecated method.
        /// </remarks>
        public virtual int LoadSymbols()
        {
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves information on whether the module represents user code or not.
        /// </summary>
        /// <param name="pfUser"> Nonzero (TRUE) if module represents user code, zero (FALSE) if it does not.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns error code.</returns>
        public virtual int IsUserCode( out int pfUser )
        {
            pfUser = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Marks the module as being user code or not.
        /// </summary>
        /// <param name="fIsUserCode">Nonzero (TRUE) if the module should be considered user code, zero (FALSE) if it should not.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns error code.</returns>
        public virtual int SetJustMyCodeState( int fIsUserCode )
        {
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}