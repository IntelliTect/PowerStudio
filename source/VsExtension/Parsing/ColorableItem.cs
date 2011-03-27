/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.TextManager.Interop;

namespace PowerStudio.VsExtension.Parsing
{
    public class ColorableItem : IVsColorableItem
    {
        private readonly COLORINDEX _Background;
        private readonly string _DisplayName;
        private readonly uint _FontFlags = (uint) FONTFLAGS.FF_DEFAULT;
        private readonly COLORINDEX _Foreground;

        public ColorableItem( string displayName,
                              COLORINDEX foreground,
                              COLORINDEX background,
                              bool bold,
                              bool strikethrough )
        {
            _DisplayName = displayName;
            _Background = background;
            _Foreground = foreground;

            if ( bold )
            {
                _FontFlags = _FontFlags | (uint) FONTFLAGS.FF_BOLD;
            }
            if ( strikethrough )
            {
                _FontFlags = _FontFlags | (uint) FONTFLAGS.FF_STRIKETHROUGH;
            }
        }

        #region IVsColorableItem Members

        public int GetDefaultColors( COLORINDEX[] piForeground, COLORINDEX[] piBackground )
        {
            if ( null == piForeground )
            {
                throw new ArgumentNullException( "piForeground" );
            }
            if ( 0 == piForeground.Length )
            {
                throw new ArgumentOutOfRangeException( "piForeground" );
            }
            piForeground[0] = _Foreground;

            if ( null == piBackground )
            {
                throw new ArgumentNullException( "piBackground" );
            }
            if ( 0 == piBackground.Length )
            {
                throw new ArgumentOutOfRangeException( "piBackground" );
            }
            piBackground[0] = _Background;

            return VSConstants.S_OK;
        }

        public int GetDefaultFontFlags( out uint pdwFontFlags )
        {
            pdwFontFlags = _FontFlags;
            return VSConstants.S_OK;
        }

        public int GetDisplayName( out string pbstrName )
        {
            pbstrName = _DisplayName;
            return VSConstants.S_OK;
        }

        #endregion
    }
}