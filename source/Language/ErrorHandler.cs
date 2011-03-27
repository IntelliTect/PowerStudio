/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.Collections.Generic;
using QUT.Gppg;

namespace PowerStudio.Language
{
    public class ErrorHandler : IErrorHandler
    {
        private const int MinErrorLevel = 2;

        private readonly List<Error> _Errors;
        private int _NumberOfErrors;
        private int _NumberOfWarnings;

        public ErrorHandler()
        {
            _Errors = new List<Error>( 8 );
        }

        public bool HasErrors
        {
            get { return _NumberOfErrors > 0; }
        }

        public bool HasWarnings
        {
            get { return _NumberOfWarnings > 0; }
        }

        #region IErrorHandler Members

        public int NumberOfErrors
        {
            get { return _NumberOfErrors; }
        }

        public int NumberOfWarnings
        {
            get { return _NumberOfWarnings; }
        }

        public void AddError( string message, int line, int column, int length, int severity )
        {
            bool isWarning = severity < MinErrorLevel;
            _Errors.Add( new Error( message, line, column, length, isWarning ) );
            if ( isWarning )
            {
                _NumberOfWarnings++;
            }
            else
            {
                _NumberOfErrors++;
            }
        }

        #endregion

        public List<Error> SortedErrorList()
        {
            if ( _Errors.Count > 0 )
            {
                _Errors.Sort();
                return _Errors;
            }
            return null;
        }

        public void AddError( string message, LexLocation span, int severity )
        {
            AddError( message, span.StartLine, span.StartColumn, span.EndColumn - span.StartColumn + 1, severity );
        }

        public void AddError( string message, int line, int column, int length )
        {
            _Errors.Add( new Error( message, line, column, length, false ) );
            _NumberOfErrors++;
        }

        public void AddWarning( string message, int line, int column, int length )
        {
            _Errors.Add( new Error( message, line, column, length, true ) );
            _NumberOfWarnings++;
        }
    }
}