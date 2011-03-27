/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.Collections.Generic;
using Microsoft.VisualStudio.TextManager.Interop;
using QUT.Gppg;

namespace PowerStudio.Language
{
    public partial class Parser
    {
        protected IList<TextSpan[]> _braces;

        public Parser( AbstractScanner<LexValue, LexLocation> scanner )
                : base( scanner )
        {
        }

        public virtual IList<TextSpan[]> Braces
        {
            get { return _braces; }
        }

        // brace matching, pairs and triples
        public void DefineMatch( int priority, params TextSpan[] locations )
        {
            if ( locations.Length == 2 )
            {
                Braces.Add(new[]
                            {
                                    locations[0],
                                    locations[1]
                            } );
            }
            else if ( locations.Length >= 3 )
            {
                Braces.Add(new[]
                            {
                                    locations[0],
                                    locations[1],
                                    locations[2]
                            } );
            }
        }

        public void DefineMatch( params TextSpan[] locations )
        {
            DefineMatch( 0, locations );
        }

        // hidden regions - not working?
        public virtual void DefineRegion( TextSpan span )
        {
            
        }

        #region TextSpan Conversion

        public TextSpan TextSpan( int startLine, int startIndex, int endIndex )
        {
            return TextSpan( startLine, startIndex, startLine, endIndex );
        }

        public TextSpan TextSpan( int startLine, int startIndex, int endLine, int endIndex )
        {
            TextSpan ts;
            ts.iStartLine = startLine - 1;
            ts.iStartIndex = startIndex;
            ts.iEndLine = endLine - 1;
            ts.iEndIndex = endIndex;
            return ts;
        }

        #endregion
    }
}