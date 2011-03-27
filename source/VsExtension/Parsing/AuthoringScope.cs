/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;

namespace PowerStudio.VsExtension.Parsing
{
    public class AuthoringScope : Microsoft.VisualStudio.Package.AuthoringScope
    {
        private readonly object _ParseResult;
        private readonly IAstResolver _Resolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthoringScope"/> class.
        /// </summary>
        /// <param name="parseResult">The parse result.</param>
        public AuthoringScope( object parseResult )
        {
            _ParseResult = parseResult;

            // how should this be set?
            _Resolver = new Resolver();
        }

        // ParseReason.QuickInfo
        public override string GetDataTipText( int line, int col, out TextSpan span )
        {
            span = new TextSpan();
            return null;
        }

        // ParseReason.CompleteWord
        // ParseReason.DisplayMemberList
        // ParseReason.MemberSelect
        // ParseReason.MemberSelectAndHilightBraces
        public override Microsoft.VisualStudio.Package.Declarations GetDeclarations( IVsTextView view,
                                                                                     int line,
                                                                                     int col,
                                                                                     TokenInfo info,
                                                                                     ParseReason reason )
        {
            IList<Declaration> declarations;
            switch ( reason )
            {
                case ParseReason.CompleteWord:
                    declarations = _Resolver.FindCompletions( _ParseResult, line, col );
                    break;
                case ParseReason.DisplayMemberList:
                case ParseReason.MemberSelect:
                case ParseReason.MemberSelectAndHighlightBraces:
                    declarations = _Resolver.FindMembers( _ParseResult, line, col );
                    break;
                default:
                    throw new ArgumentException( "reason" );
            }

            return new Declarations( declarations );
        }

        // ParseReason.GetMethods
        public override Microsoft.VisualStudio.Package.Methods GetMethods( int line, int col, string name )
        {
            return new Methods( _Resolver.FindMethods( _ParseResult, line, col, name ) );
        }

        // ParseReason.Goto
        public override string Goto( VSConstants.VSStd97CmdID cmd,
                                     IVsTextView textView,
                                     int line,
                                     int col,
                                     out TextSpan span )
        {
            // throw new System.NotImplementedException();
            span = new TextSpan();
            return null;
        }
    }
}