#region License

// 
// Copyright (c) 2011, PowerStudio.DebugEngine Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace PowerStudio.DebugEngine.Events
{
    internal class ActivateDocumentEvent : AsynchronousEvent, IDebugActivateDocumentEvent2
    {
        public ActivateDocumentEvent( IDebugDocumentContext2 documentContext, IDebugDocument2 document = null )
        {
            if ( documentContext == null )
            {
                throw new ArgumentNullException( "documentContext" );
            }

            DocumentContext = documentContext;
            if ( document == null )
            {
                int result = documentContext.GetDocument( out document );
                if ( result != VSConstants.S_OK )
                {
                    throw new ArgumentException( "The document could not be resolved from the document context." );
                }
            }
            Document = document;
        }

        #region Implementation of IDebugActivateDocumentEvent2

        /// <summary>
        /// Gets the document to activate.
        /// </summary>
        /// <param name="ppDoc">Returns an IDebugDocument2 object that represents the document to be activated.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public int GetDocument( out IDebugDocument2 ppDoc )
        {
            ppDoc = Document;
            return VSConstants.S_OK;
        }

        /// <summary>
        /// Gets the document context that describes the position in the document that is to be made active by the debug package.
        /// </summary>
        /// <param name="ppDocContext">Returns an IDebugDocumentContext2 object that represents a position in a source file document.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This position might be used to show the caret, for example.</remarks>
        public int GetDocumentContext( out IDebugDocumentContext2 ppDocContext )
        {
            ppDocContext = DocumentContext;
            return VSConstants.S_OK;
        }

        #endregion

        public IDebugDocumentContext2 DocumentContext { get; private set; }
        public IDebugDocument2 Document { get; private set; }
    }
}