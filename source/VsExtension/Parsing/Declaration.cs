/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

namespace PowerStudio.VsExtension.Parsing
{
    public struct Declaration
    {
        public string Description;
        public string DisplayText;
        public int Glyph;
        public string Name;

        public Declaration( string description, string displayText, int glyph, string name )
        {
            Description = description;
            DisplayText = displayText;
            Glyph = glyph;
            Name = name;
        }
    }
}