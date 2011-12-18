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
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    public abstract class CodeDocumentContext : IDebugDocumentContext2,
                                                IDebugCodeContext2,
                                                IEnumDebugCodeContexts2,
                                                IDebugMemoryContext2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugMemoryContext2

        /// <summary>
        /// Retrieves the user-displayable name for this context.
        /// </summary>
        /// <param name="pbstrName">Returns the name of the memory context.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The name of a memory context is not normally used.</remarks>
        public virtual int GetName( out string pbstrName )
        {
            Logger.Debug( string.Empty );
            pbstrName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a CONTEXT_INFO structure that describes the context.
        /// </summary>
        /// <param name="dwFields"> A combination of flags from the CONTEXT_INFO_FIELDS enumeration that indicate which fields of the CONTEXT_INFO structure are to be fill in.</param>
        /// <param name="pinfo">The CONTEXT_INFO structure that is filled in.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetInfo( enum_CONTEXT_INFO_FIELDS dwFields, CONTEXT_INFO[] pinfo )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Adds the specified value to the current context and returns a new context.
        /// </summary>
        /// <param name="dwCount">The value to add to the current context.</param>
        /// <param name="ppMemCxt"> Returns a new IDebugMemoryContext2 object.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// A memory context is an address, so adding a value to an address produces a new address that requires a new context interface.
        /// 
        /// This method must always produce a new context, even if the resulting address is outside the memory space associated with this context. The only exception to this is if no memory can be allocated for the new context or if ppMemCxt is a null value (which is an error).
        /// </remarks>
        public virtual int Add( ulong dwCount, out IDebugMemoryContext2 ppMemCxt )
        {
            Logger.Debug( string.Empty );
            ppMemCxt = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Subtracts the specified value from the current context and returns a new context.
        /// </summary>
        /// <param name="dwCount">The number of memory bytes to decrement.</param>
        /// <param name="ppMemCxt">Returns a new IDebugMemoryContext2 object.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// A memory context is an address, so subtracting a value from an address produces a new address that requires a new context interface.
        /// 
        /// This method must always produce a new context, even if the resulting address is outside the memory space associated with this context. The only exception to this is if no memory can be allocated for the new context or if ppMemCxt is a null value (which is an error).
        /// </remarks>
        public virtual int Subtract( ulong dwCount, out IDebugMemoryContext2 ppMemCxt )
        {
            Logger.Debug( string.Empty );
            ppMemCxt = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Compares the memory context to each context in the given array in the manner indicated by compare flags, returning an index of the first context that matches.
        /// </summary>
        /// <param name="Compare">A value from the CONTEXT_COMPARE enumeration that determines the type of comparison.</param>
        /// <param name="rgpMemoryContextSet">An array of references to the IDebugMemoryContext2 objects to compare against.</param>
        /// <param name="dwMemoryContextSetLen">The number of contexts in the rgpMemoryContextSet array.</param>
        /// <param name="pdwMemoryContext">Returns the index of the first memory context that satisfies the comparison.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code. Returns E_COMPARE_CANNOT_COMPARE if the two contexts cannot be compared.</returns>
        /// <remarks>A debug engine (DE) does not have to support all types of comparisons, but it must support at least CONTEXT_EQUAL, CONTEXT_LESS_THAN, CONTEXT_GREATER_THAN and CONTEXT_SAME_SCOPE.</remarks>
        public virtual int Compare( enum_CONTEXT_COMPARE Compare,
                                    IDebugMemoryContext2[] rgpMemoryContextSet,
                                    uint dwMemoryContextSetLen,
                                    out uint pdwMemoryContext )
        {
            Logger.Debug( string.Empty );
            pdwMemoryContext = 0;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugCodeContext2

        /// <summary>
        /// Gets the document context that corresponds to this code context. The document context represents a position in the source file that corresponds to the source code that generated this instruction.
        /// </summary>
        /// <param name="ppSrcCxt">Returns the IDebugDocumentContext2 object that corresponds to the code context.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>Generally, the document context can be thought of as a position in a source file while the code context is a position of a code instruction in an execution stream.</remarks>
        public virtual int GetDocumentContext( out IDebugDocumentContext2 ppSrcCxt )
        {
            Logger.Debug( string.Empty );
            ppSrcCxt = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugCodeContext2 and IDebugDocumentContext2

        /// <summary>
        /// Gets the language information for this code context.
        /// </summary>
        /// <param name="pbstrLanguage">Returns a string that contains the name of the language, such as "C++."</param>
        /// <param name="pguidLanguage">Returns the GUID for the language of the code context, for example, guidCPPLang.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>At least one of the parameters must return a non-null value.</remarks>
        public virtual int GetLanguageInfo( ref string pbstrLanguage, ref Guid pguidLanguage )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IDebugDocumentContext2

        /// <summary>
        /// Gets the document that contains this document context.
        /// </summary>
        /// <param name="ppDocument">Returns an IDebugDocument2 object that represents the document that contains this document context.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetDocument( out IDebugDocument2 ppDocument )
        {
            Logger.Debug( string.Empty );
            ppDocument = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the displayable name of the document that contains this document context.
        /// </summary>
        /// <param name="gnType">A value from the GETNAME_TYPE enumeration that specifies the type of name to return.</param>
        /// <param name="pbstrFileName">Returns the name of the file.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method typically forwards the call to the IDebugDocument2::GetName method, unless the document context is written to store the document name itself.</remarks>
        public virtual int GetName( enum_GETNAME_TYPE gnType, out string pbstrFileName )
        {
            Logger.Debug( string.Empty );
            pbstrFileName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of all code contexts associated with this document context.
        /// </summary>
        /// <param name="ppEnumCodeCxts">Returns an IEnumDebugCodeContexts2 object that contains a list of code contexts.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A single document context can generate multiple code contexts when the document is using templates or include files.</remarks>
        public virtual int EnumCodeContexts( out IEnumDebugCodeContexts2 ppEnumCodeCxts )
        {
            ppEnumCodeCxts = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the file statement range of the document context.
        /// </summary>
        /// <param name="pBegPosition">A TEXT_POSITION structure that is filled in with the starting position. Set this argument to a null value if this information is not needed.</param>
        /// <param name="pEndPosition">A TEXT_POSITION structure that is filled in with the ending position. Set this argument to a null value if this information is not needed.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// A statement range is the range of the lines that contributed the code to which this document context refers.
        /// 
        /// To obtain the range of source code (including comments) within this document context, call the IDebugDocumentContext2::GetSourceRange method.
        /// </remarks>
        public virtual int GetStatementRange( TEXT_POSITION[] pBegPosition, TEXT_POSITION[] pEndPosition )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the source code range of this document context.
        /// </summary>
        /// <param name="pBegPosition">A TEXT_POSITION structure that is filled in with the starting position. Set this argument to a null value if this information is not needed.</param>
        /// <param name="pEndPosition">A TEXT_POSITION structure that is filled in with the ending position. Set this argument to a null value if this information is not needed.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// A source range is the entire range of source code, from the current statement back to just after the previous statement that contributed code. The source range is typically used for mixing source statements, including comments, with code in the disassembly window.
        /// 
        /// To get the range for just the code statements contained within this document context, call the IDebugDocumentContext2::GetStatementRange method.
        /// </remarks>
        public virtual int GetSourceRange( TEXT_POSITION[] pBegPosition, TEXT_POSITION[] pEndPosition )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Compares this document context to a given array of document contexts.
        /// </summary>
        /// <param name="Compare">A value from the DOCCONTEXT_COMPARE enumeration that specifies the type of comparison.</param>
        /// <param name="rgpDocContextSet">An array of IDebugDocumentContext2 objects that represent the document contexts being compared to.</param>
        /// <param name="dwDocContextSetLen">The length of the array of document contexts to compare.</param>
        /// <param name="pdwDocContext">Returns the index into the rgpDocContextSet array of the first document context that satisfies the comparison.</param>
        /// <returns>Returns S_OK if a match was found. Returns S_FALSE if no match was found. Otherwise, returns an error code.</returns>
        /// <remarks>
        /// The IDebugDocumentContext2 objects that are passed in the array must be implemented by the same debug engine that implements the IDebugDocumentContext2 object being called on; otherwise, the comparison is not valid.
        /// </remarks>
        public virtual int Compare( enum_DOCCONTEXT_COMPARE Compare,
                                    IDebugDocumentContext2[] rgpDocContextSet,
                                    uint dwDocContextSetLen,
                                    out uint pdwDocContext )
        {
            Logger.Debug( string.Empty );
            pdwDocContext = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Moves the document context by a given number of statements or lines.
        /// </summary>
        /// <param name="nCount">The number of statements or lines to move ahead, depending on the document context.</param>
        /// <param name="ppDocContext">Returns a new IDebugDocumentContext2 object with the new position.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int Seek( int nCount, out IDebugDocumentContext2 ppDocContext )
        {
            Logger.Debug( string.Empty );
            ppDocContext = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion

        #region Implementation of IEnumDebugCodeContexts2

        /// <summary>
        /// Returns the next set of elements from the enumeration.
        /// </summary>
        /// <param name="celt">The number of elements to retrieve. Also specifies the maximum size of the rgelt array.</param>
        /// <param name="rgelt">Array of IDebugCodeContext2 elements to be filled in.</param>
        /// <param name="pceltFetched">Returns the number of elements actually returned in rgelt.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if fewer than the requested number of elements could be returned; otherwise, returns an error code.</returns>
        public virtual int Next( uint celt, IDebugCodeContext2[] rgelt, ref uint pceltFetched )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Skips over the specified number of elements.
        /// </summary>
        /// <param name="celt">Number of elements to skip.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if celt is greater than the number of remaining elements; otherwise, returns an error code.</returns>
        /// <remarks>
        /// If celt specifies a value greater than the number of remaining elements, the enumeration is set to the end and S_FALSE is returned.
        /// </remarks>
        public virtual int Skip( uint celt )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Resets the enumeration to the first element.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>After this method is called, the next call to the IEnumDebugCodeContexts2::Next method returns the first element of the enumeration.</remarks>
        public virtual int Reset()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Returns a copy of the current enumeration as a separate object.
        /// </summary>
        /// <param name="ppEnum">Returns a copy of this enumeration as a separate object.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The copy of the enumeration has the same state as the original at the time this method is called. However, the copy's and the original's states are separate and can be changed individually.</remarks>
        public virtual int Clone( out IEnumDebugCodeContexts2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Returns the number of elements in the enumeration.
        /// </summary>
        /// <param name="pcelt">Returns the number of elements in the enumeration.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method is not part of the customary COM enumeration interface which specifies that only the Next, Clone, Skip, and Reset methods need to be implemented.</remarks>
        public virtual int GetCount( out uint pcelt )
        {
            Logger.Debug( string.Empty );
            pcelt = 0;
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}