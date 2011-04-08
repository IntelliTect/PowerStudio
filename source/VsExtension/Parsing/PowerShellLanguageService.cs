#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.TextManager.Interop;

#endregion

namespace PowerStudio.VsExtension.Parsing
{
    [Guid( PsConstants.PsLanuageServiceGuidString )]
    public class PowerShellLanguageService : IVsLanguageInfo
    {
        #region Implementation of IVsLanguageInfo

        /// <summary>
        ///   Returns the name of the programming language.
        /// </summary>
        /// <returns>
        ///   If the method succeeds, it returns <see cref = "F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        /// <param name = "bstrName">[out] Returns a BSTR that contains the language name.</param>
        public int GetLanguageName( out string bstrName )
        {
            bstrName = LanguageConfiguration.Name;
            return VSConstants.S_OK;
        }

        /// <summary>
        ///   Returns the file extensions belonging to this language.
        /// </summary>
        /// <returns>
        ///   If the method succeeds, it returns <see cref = "F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        /// <param name = "pbstrExtensions">[out] Returns a BSTR that contains the requested file extensions.</param>
        public int GetFileExtensions( out string pbstrExtensions )
        {
            pbstrExtensions = ".ps1;.psc1;.psd1;.psm1;.ps1xml";
            return VSConstants.S_OK;
        }

        /// <summary>
        ///   Returns the colorizer.
        /// </summary>
        /// <returns>
        ///   If the method succeeds, it returns <see cref = "F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        /// <param name = "pBuffer">[in] The <see cref = "T:Microsoft.VisualStudio.TextManager.Interop.IVsTextLines" /> interface for the requested colorizer.</param>
        /// <param name = "ppColorizer">[out] Returns an <see cref = "T:Microsoft.VisualStudio.TextManager.Interop.IVsColorizer" /> object.</param>
        public int GetColorizer( IVsTextLines pBuffer, out IVsColorizer ppColorizer )
        {
            ppColorizer = null;
            return VSConstants.E_FAIL;
        }

        /// <summary>
        ///   Allows a language to add adornments to a code editor.
        /// </summary>
        /// <returns>
        ///   If the method succeeds, it returns <see cref = "F:Microsoft.VisualStudio.VSConstants.S_OK" />. If it fails, it returns an error code.
        /// </returns>
        /// <param name = "pCodeWin">[in] The <see cref = "T:Microsoft.VisualStudio.TextManager.Interop.IVsCodeWindow" /> interface for the requested code editor manager.</param>
        /// <param name = "ppCodeWinMgr">[out] Returns an <see cref = "T:Microsoft.VisualStudio.TextManager.Interop.IVsCodeWindowManager" /> object.</param>
        public int GetCodeWindowManager( IVsCodeWindow pCodeWin, out IVsCodeWindowManager ppCodeWinMgr )
        {
            ppCodeWinMgr = null;
            return VSConstants.E_FAIL;
        }

        #endregion
    }
}