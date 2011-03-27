/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.Collections.Generic;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;
using PowerStudio.Language;
using QUT.Gppg;

namespace PowerStudio.VsExtension.Parsing
{
    public class PowerShellParser : Parser
    {
        private ParseRequest request;

        public PowerShellParser( AbstractScanner<LexValue, LexLocation> scanner )
                : base( scanner )
        {
        }

        public ParseRequest Request
        {
            get { return request; }
        }

        public AuthoringSink Sink
        {
            get { return request.Sink; }
        }

        public void MBWInit( ParseRequest request )
        {
            this.request = request;
            _braces = new List<TextSpan[]>();
        }

        // hidden regions - not working?
        public override void DefineRegion( TextSpan span )
        {
            Sink.AddHiddenRegion( span );
        }

        // error reporting
        public void ReportError( TextSpan span, string message, Severity severity )
        {
            Sink.AddError( request.FileName, message, span, severity );
        }

        #region Error Overloads (Severity)

        public void ReportError( TextSpan location, string message )
        {
            ReportError( location, message, Severity.Error );
        }

        public void ReportFatal( TextSpan location, string message )
        {
            ReportError( location, message, Severity.Fatal );
        }

        public void ReportWarning( TextSpan location, string message )
        {
            ReportError( location, message, Severity.Warning );
        }

        public void ReportHint( TextSpan location, string message )
        {
            ReportError( location, message, Severity.Hint );
        }

        #endregion
    }
}