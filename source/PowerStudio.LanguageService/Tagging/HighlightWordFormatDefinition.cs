#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.ComponentModel.Composition;
using System.Drawing;
using System.Security;
using System.Security.AccessControl;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Win32;
using PowerStudio.Resources;
using Color = System.Windows.Media.Color;

#endregion

namespace PowerStudio.LanguageService.Tagging
{
    [Export( typeof (EditorFormatDefinition) )]
    [Name( PredefinedTextMarkerTags.WordHighlight )]
    [UserVisible( true )]
    public class HighlightWordFormatDefinition : MarkerFormatDefinition
    {
        public HighlightWordFormatDefinition()
        {
            LoadDefaultColors();
            LoadColorsFromRegistry();
            DisplayName = "Highlight Word";
            ZOrder = 5;
        }

        private void LoadDefaultColors()
        {
            const int a = 255;
            const int r = 173;
            const int g = 214;
            const int b = 255;
            BackgroundColor = Color.FromArgb( a, r, g, b );
            ForegroundColor = Color.FromArgb( a, r, g, b );
        }

        public void LoadColorsFromRegistry()
        {
            RegistryKey categoryKey;
            bool success = TryGetCategoryKey( out categoryKey );

            if ( !success )
            {
                return;
            }

            var backgroundColorEntry = (int) categoryKey.GetValue( "SelectedText Background" );
            var foregroundColorEntry = (int) categoryKey.GetValue( "SelectedText Foreground" );

            System.Drawing.Color backgroundDrawingColor = ColorTranslator.FromWin32( backgroundColorEntry );
            System.Drawing.Color foregroundDrawingColor = ColorTranslator.FromWin32( foregroundColorEntry );

            BackgroundColor = Color.FromArgb( backgroundDrawingColor.A,
                                              backgroundDrawingColor.R,
                                              backgroundDrawingColor.G,
                                              backgroundDrawingColor.B );
            ForegroundColor = Color.FromArgb( foregroundDrawingColor.A,
                                              foregroundDrawingColor.R,
                                              foregroundDrawingColor.G,
                                              foregroundDrawingColor.B );
        }

        private static bool TryGetCategoryKey( out RegistryKey registryKey )
        {
            const string keyName =
                    PsConstants.DefaultRegistryRoot + @"\\FontAndColors\\{358463D0-D084-400F-997E-A34FC570BC72}";

            try
            {
                const RegistryKeyPermissionCheck keyPermissionCheck = RegistryKeyPermissionCheck.ReadSubTree;
                const RegistryRights registryRights = RegistryRights.QueryValues | RegistryRights.ReadKey;
                registryKey = Registry.CurrentUser.OpenSubKey( keyName, keyPermissionCheck, registryRights );
                if ( registryKey != null )
                {
                    return true;
                }
                registryKey = Registry.LocalMachine.OpenSubKey( keyName, keyPermissionCheck, registryRights );
                return registryKey != null;
            }
            catch ( SecurityException )
            {
                // nothing I can do
                registryKey = null;
                return false;
            }
        }
    }
}