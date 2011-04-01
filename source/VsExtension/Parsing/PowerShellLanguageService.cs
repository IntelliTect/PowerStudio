/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/
using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;
using PowerStudio.Language;
using ErrorHandler = PowerStudio.Language.ErrorHandler;

namespace PowerStudio.VsExtension.Parsing
{
    [Guid( "4841FFB1-678C-4F50-9ADB-600638A4F731" )]
    public class PowerShellLanguageService : IVsLanguageInfo
    {
        #region Implementation of IVsLanguageInfo

        /// <summary>
        /// Returns the name of the programming language.
        /// </summary>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK"/>. If it fails, it returns an error code.
        /// </returns>
        /// <param name="bstrName">[out] Returns a BSTR that contains the language name.</param>
        public int GetLanguageName( out string bstrName )
        {
            bstrName = Configuration.Name;
            return VSConstants.S_OK;
        }

        /// <summary>
        /// Returns the file extensions belonging to this language.
        /// </summary>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK"/>. If it fails, it returns an error code.
        /// </returns>
        /// <param name="pbstrExtensions">[out] Returns a BSTR that contains the requested file extensions.</param>
        public int GetFileExtensions( out string pbstrExtensions )
        {
            pbstrExtensions = ".ps1";
            return VSConstants.S_OK;
        }

        /// <summary>
        /// Returns the colorizer.
        /// </summary>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK"/>. If it fails, it returns an error code.
        /// </returns>
        /// <param name="pBuffer">[in] The <see cref="T:Microsoft.VisualStudio.TextManager.Interop.IVsTextLines"/> interface for the requested colorizer.</param><param name="ppColorizer">[out] Returns an <see cref="T:Microsoft.VisualStudio.TextManager.Interop.IVsColorizer"/> object.</param>
        public int GetColorizer( IVsTextLines pBuffer, out IVsColorizer ppColorizer )
        {
            ppColorizer = null;
            return VSConstants.E_FAIL;
        }

        /// <summary>
        /// Allows a language to add adornments to a code editor.
        /// </summary>
        /// <returns>
        /// If the method succeeds, it returns <see cref="F:Microsoft.VisualStudio.VSConstants.S_OK"/>. If it fails, it returns an error code.
        /// </returns>
        /// <param name="pCodeWin">[in] The <see cref="T:Microsoft.VisualStudio.TextManager.Interop.IVsCodeWindow"/> interface for the requested code editor manager.</param><param name="ppCodeWinMgr">[out] Returns an <see cref="T:Microsoft.VisualStudio.TextManager.Interop.IVsCodeWindowManager"/> object.</param>
        public int GetCodeWindowManager( IVsCodeWindow pCodeWin, out IVsCodeWindowManager ppCodeWinMgr )
        {
            ppCodeWinMgr = null;
            return VSConstants.E_FAIL;
        }

        #endregion
    }
}