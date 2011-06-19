#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using DebugEngine;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace PowerStudio.Debugger
{
    public class PowerShellModule : DebugModuleBase
    {
        public PowerShellModule( string fileName )
        {
            FileName = fileName;
        }

        public virtual string FileName { get; private set; }

        #region Overrides of DebugModuleBase

        public override string Name
        {
            get { return FileName; }
        }

        /// <summary>
        /// Gets information about this module.
        /// </summary>
        /// <param name="dwFields">A combination of flags from the MODULE_INFO_FIELDS enumeration that specify which fields of pInfo are to be filled out.</param>
        /// <param name="pinfo">A MODULE_INFO structure that is filled in with a description of the module.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns an error code.
        /// </returns>
        public override int GetInfo( enum_MODULE_INFO_FIELDS dwFields, MODULE_INFO[] pinfo )
        {
            var info = new MODULE_INFO();

            if ( ( dwFields & enum_MODULE_INFO_FIELDS.MIF_NAME ) != 0 )
            {
                info.m_bstrName = FileName;
                info.dwValidFields |= enum_MODULE_INFO_FIELDS.MIF_NAME;
            }

            if ( ( dwFields & enum_MODULE_INFO_FIELDS.MIF_URL ) != 0 )
            {
                info.m_bstrUrl = FileName;
                info.dwValidFields |= enum_MODULE_INFO_FIELDS.MIF_URL;
            }

            if ( ( dwFields & enum_MODULE_INFO_FIELDS.MIF_FLAGS ) != 0 )
            {
                info.m_dwModuleFlags = enum_MODULE_FLAGS.MODULE_FLAG_SYMBOLS;
                info.dwValidFields |= enum_MODULE_INFO_FIELDS.MIF_FLAGS;
            }

            if ( ( dwFields & enum_MODULE_INFO_FIELDS.MIF_URLSYMBOLLOCATION ) != 0 )
            {
                info.m_bstrUrlSymbolLocation = @".\";
                info.dwValidFields |= enum_MODULE_INFO_FIELDS.MIF_URLSYMBOLLOCATION;
            }

            pinfo[0] = info;

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Retrieves information on whether the module represents user code or not.
        /// </summary>
        /// <param name="pfUser">Nonzero (TRUE) if module represents user code, zero (FALSE) if it does not.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise, returns error code.
        /// </returns>
        public override int IsUserCode(out int pfUser)
        {
            pfUser = 1;
            return VSConstants.S_OK;
        }

        #endregion
    }
}