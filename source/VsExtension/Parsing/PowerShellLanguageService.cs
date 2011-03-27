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
    public class PowerShellLanguageService : LanguageService
    {
        public override string Name
        {
            get { return Configuration.Name; }
        }

        public override string GetFormatFilterList()
        {
            return "PowerShell File (*.ps1)\n*.ps1";
        }

        public override void OnIdle( bool periodic )
        {
            // from IronPythonLanguage sample
            // this appears to be necessary to get a parse request with ParseReason = Check?
            var src = (PsSource) GetSource( LastActiveTextView );
            if ( src != null &&
                 src.LastParseTime >= Int32.MaxValue >> 12 )
            {
                src.LastParseTime = 0;
            }
            base.OnIdle( periodic );
        }


        public override Microsoft.VisualStudio.Package.AuthoringScope ParseSource( ParseRequest req )
        {
            var source = (PsSource) GetSource( req.FileName );
            bool yyparseResult = false;

            // req.DirtySpan seems to be set even though no changes have occurred
            // source.IsDirty also behaves strangely
            // might be possible to use source.ChangeCount to sync instead

            if ( req.DirtySpan.iStartIndex != req.DirtySpan.iEndIndex
                 ||
                 req.DirtySpan.iStartLine != req.DirtySpan.iEndLine )
            {
                var handler = new ErrorHandler();
                var scanner = new Scanner(); // string interface
                var parser = new PowerShellParser( scanner ); // use noarg constructor

                parser.SetHandler( handler );
                scanner.SetSource( req.Text, 0 );

                parser.MBWInit( req );
                yyparseResult = parser.Parse();

                // store the parse results
                // source.ParseResult = aast;
                source.ParseResult = null;
                source.Braces = parser.Braces;

                // for the time being, just pull errors back from the error handler
                if ( handler.NumberOfErrors > 0 )
                {
                    foreach ( Error error in handler.SortedErrorList() )
                    {
                        var span = new TextSpan();
                        span.iStartLine = span.iEndLine = error.Line - 1;
                        span.iStartIndex = error.Column;
                        span.iEndIndex = error.Column + error.Length;
                        req.Sink.AddError( req.FileName, error.Message, span, Severity.Error );
                    }
                }
            }

            switch ( req.Reason )
            {
                case ParseReason.Check:
                case ParseReason.HighlightBraces:
                case ParseReason.MatchBraces:
                case ParseReason.MemberSelectAndHighlightBraces:
                    // send matches to sink
                    // this should (probably?) be filtered on req.Line / col
                    if ( source.Braces != null )
                    {
                        foreach ( var brace in source.Braces )
                        {
                            if ( brace.Length == 2 )
                            {
                                req.Sink.MatchPair( brace[0], brace[1], 1 );
                            }
                            else if ( brace.Length >= 3 )
                            {
                                req.Sink.MatchTriple( brace[0], brace[1], brace[2], 1 );
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return new Parsing.AuthoringScope( source.ParseResult );
        }

        #region Custom Colors

        public override int GetColorableItem( int index, out IVsColorableItem item )
        {
            if ( index <= Configuration.ColorableItems.Count )
            {
                item = Configuration.ColorableItems[index - 1];
                return VSConstants.S_OK;
            }
            throw new ArgumentNullException( "index" );
        }

        public override int GetItemCount( out int count )
        {
            count = Configuration.ColorableItems.Count;
            return VSConstants.S_OK;
        }

        #endregion

        #region MPF Accessor and Factory specialisation

        private LanguagePreferences preferences;
        private IScanner scanner;

        public override LanguagePreferences GetLanguagePreferences()
        {
            if ( preferences == null )
            {
                preferences = new LanguagePreferences( Site,
                                                       typeof (PowerShellLanguageService).GUID,
                                                       Name );
                preferences.Init();
            }

            return preferences;
        }

        public override Source CreateSource( IVsTextLines buffer )
        {
            return new PsSource( this, buffer, GetColorizer( buffer ) );
        }

        public override IScanner GetScanner( IVsTextLines buffer )
        {
            if ( scanner == null )
            {
                scanner = new LineScanner();
            }

            return scanner;
        }

        #endregion
    }
}