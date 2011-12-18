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
    public abstract class DebugStackFrame : IDebugStackFrame2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugStackFrame2

        /// <summary>
        /// Gets the code context for this stack frame.
        /// </summary>
        /// <param name="ppCodeCxt">Returns an IDebugCodeContext2 object that represents the current instruction pointer in this stack frame.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetCodeContext( out IDebugCodeContext2 ppCodeCxt )
        {
            Logger.Debug( string.Empty );
            ppCodeCxt = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the document context for this stack frame.
        /// </summary>
        /// <param name="ppCxt">Returns an IDebugDocumentContext2 object that represents the current position in a source document.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// This method is faster than calling the IDebugStackFrame2::GetCodeContext method and then calling the IDebugCodeContext2::GetDocumentContext method on the code context. However, it is not guaranteed that every debug engine (DE) will implement this method.
        /// </remarks>
        public virtual int GetDocumentContext( out IDebugDocumentContext2 ppCxt )
        {
            Logger.Debug( string.Empty );
            ppCxt = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the name of the stack frame.
        /// </summary>
        /// <param name="pbstrName">Returns the name of the stack frame.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The name of a stack frame is typically the name of the method being executed.</remarks>
        public virtual int GetName( out string pbstrName )
        {
            Logger.Debug( string.Empty );
            pbstrName = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets a description of the stack frame.
        /// </summary>
        /// <param name="dwFieldSpec">A combination of flags from the FRAMEINFO_FLAGS enumeration that specifies which fields of the pFrameInfo parameter are to be filled in.</param>
        /// <param name="nRadix">The radix to be used in formatting any numerical information.</param>
        /// <param name="pFrameInfo">A FRAMEINFO structure that is filled in with the description of the stack frame.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetInfo( enum_FRAMEINFO_FLAGS dwFieldSpec, uint nRadix, FRAMEINFO[] pFrameInfo )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets a machine-dependent representation of the range of physical addresses associated with a stack frame.
        /// </summary>
        /// <param name="paddrMin">Returns the lowest physical address associated with this stack frame.</param>
        /// <param name="paddrMax">Returns the highest physical address associated with this stack frame.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>
        /// The information returned by this method is used by the session debug manager (SDM) to sort stack frames.
        /// 
        /// It is assumed that the call stack grows down, that is, that new stack frames are added at increasingly lower memory addresses. A run-time architecture must provide physical stack ranges that match this assumption.
        /// </remarks>
        public virtual int GetPhysicalStackRange( out ulong paddrMin, out ulong paddrMax )
        {
            Logger.Debug( string.Empty );
            paddrMin = 0;
            paddrMax = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets an evaluation context for expression evaluation within the current context of a stack frame and thread.
        /// </summary>
        /// <param name="ppExprCxt">Returns an IDebugExpressionContext2 object that represents a context for expression evaluation.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>Generally, an expression evaluation context can be thought of as a scope for performing expression evaluation. Call the IDebugExpressionContext2::ParseText method to parse an expression and then call the resulting IDebugExpression2::EvaluateSync or IDebugExpression2::EvaluateAsync methods to evaluate the parsed expression.</remarks>
        public virtual int GetExpressionContext( out IDebugExpressionContext2 ppExprCxt )
        {
            Logger.Debug( string.Empty );
            ppExprCxt = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the language associated with this stack frame.
        /// </summary>
        /// <param name="pbstrLanguage">Returns the name of the language that implements the method associated with this stack frame.</param>
        /// <param name="pguidLanguage">Returns the GUID of the language.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetLanguageInfo( ref string pbstrLanguage, ref Guid pguidLanguage )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets a description of the properties of a stack frame.
        /// </summary>
        /// <param name="ppProperty">Returns an IDebugProperty2 object that describes the properties of this stack frame.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>Calling the IDebugProperty2::EnumChildren method with appropriate filters can retrieve the local variables, method parameters, registers, and "this" pointer associated with the stack frame.</remarks>
        public virtual int GetDebugProperty( out IDebugProperty2 ppProperty )
        {
            Logger.Debug( string.Empty );
            ppProperty = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Creates an enumerator for properties associated with the stack frame, such as local variables.
        /// </summary>
        /// <param name="dwFields">A combination of flags from the DEBUGPROP_INFO_FLAGS enumeration that specifies which fields in the enumerated DEBUG_PROPERTY_INFO structures are to be filled in.</param>
        /// <param name="nRadix">The radix to be used in formatting any numerical information.</param>
        /// <param name="guidFilter">A GUID of a filter used to select which DEBUG_PROPERTY_INFO structures are to be enumerated, such as guidFilterLocals.</param>
        /// <param name="dwTimeout">Maximum time, in milliseconds, to wait before returning from this method. Use INFINITE to wait indefinitely.</param>
        /// <param name="pcelt">Returns the number of properties enumerated. This is the same as calling the IEnumDebugPropertyInfo2::GetCount method.</param>
        /// <param name="ppEnum">Returns an IEnumDebugPropertyInfo2 object containing a list of the desired properties.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>Because this method allows all selected properties to be retrieved with a single call, it is faster than sequentially calling the IDebugStackFrame2::GetDebugProperty and IDebugProperty2::EnumChildren methods.</remarks>
        public virtual int EnumProperties( enum_DEBUGPROP_INFO_FLAGS dwFields,
                                           uint nRadix,
                                           ref Guid guidFilter,
                                           uint dwTimeout,
                                           out uint pcelt,
                                           out IEnumDebugPropertyInfo2 ppEnum )
        {
            Logger.Debug( string.Empty );
            pcelt = 0;
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the thread associated with a stack frame.
        /// </summary>
        /// <param name="ppThread">Returns an IDebugThread2 object that represents the thread.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int GetThread( out IDebugThread2 ppThread )
        {
            Logger.Debug( string.Empty );
            ppThread = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}