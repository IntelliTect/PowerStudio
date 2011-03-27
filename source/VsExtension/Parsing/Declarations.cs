/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.Collections.Generic;

namespace PowerStudio.VsExtension.Parsing
{
    public class Declarations : Microsoft.VisualStudio.Package.Declarations
    {
        private readonly IList<Declaration> declarations;

        public Declarations( IList<Declaration> declarations )
        {
            this.declarations = declarations;
        }

        public override int GetCount()
        {
            return declarations.Count;
        }

        public override string GetDescription( int index )
        {
            return declarations[index].Description;
        }

        public override string GetDisplayText( int index )
        {
            return declarations[index].DisplayText;
        }

        public override int GetGlyph( int index )
        {
            return declarations[index].Glyph;
        }

        public override string GetName( int index )
        {
            if ( index >= 0 )
            {
                return declarations[index].Name;
            }

            return null;
        }
    }
}