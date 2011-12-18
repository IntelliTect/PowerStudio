#region License

// 
// Copyright (c) 2011, PowerStudio.DebugEngine Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// When a program has halted, the session debug manager (SDM) obtains a stack frame from the DE with a call to IDebugThread2::EnumFrameInfo. The SDM then calls IDebugStackFrame2::GetExpressionContext to get the IDebugExpressionContext2 interface. This is followed by a call to IDebugExpressionContext2::ParseText to create the IDebugExpression2 interface, which represents the parsed expression ready to be evaluated.
    /// 
    /// The SDM calls either IDebugExpression2::EvaluateSync or IDebugExpression2::EvaluateAsync to actually evaluate the expression and produce a value.
    ///     
    /// In an implementation of IDebugExpressionContext2::ParseText, the DE uses COM's CoCreateInstance function to instantiate an expression evaluator and get an IDebugExpressionEvaluator interface (see the Example in the IDebugExpressionEvaluator interface). The DE then calls IDebugExpressionEvaluator::Parse to obtain an IDebugParsedExpression interface. This interface is used in the implementation of IDebugExpression2::EvaluateSync and IDebugExpression2::EvaluateAsync to perform the evaluation.
    /// </remarks>
    public abstract class DebugExpression : IDebugExpression2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugExpression2

        /// <summary>
        /// This method evaluates the expression asynchronously.
        /// </summary>
        /// <param name="dwFlags">A combination of flags from the EVALFLAGS enumeration that control expression evaluation.</param>
        /// <param name="pExprCallback">This parameter is always a null value.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise returns an error code. A typical error code is:
        /// 
        /// E_EVALUATE_BUSY_WITH_EVALUATION:  Another expression is currently being evaluated, and simultaneous expression evaluation is not supported.
        /// </returns>
        /// <remarks>
        /// This method should return immediately after it has started the expression evaluation. When the expression is successfully evaluated, an IDebugExpressionEvaluationCompleteEvent2 must be sent to the IDebugEventCallback2 event callback as supplied through IDebugProgram2::Attach or IDebugEngine2::Attach.
        /// </remarks>
        public int EvaluateAsync( enum_EVALFLAGS dwFlags, IDebugEventCallback2 pExprCallback )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// This method cancels asynchronous expression evaluation as started by a call to the IDebugExpression2::EvaluateAsync method.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>When asynchronous expression evaluation is cancelled, do not sent an IDebugExpressionEvaluationCompleteEvent2 event to the event callback passed to the IDebugProgram2::Attach or IDebugEngine2::Attach methods.</remarks>
        public int Abort()
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// This method evaluates the expression synchronously.
        /// </summary>
        /// <param name="dwFlags">A combination of flags from the EVALFLAGS enumeration that control expression evaluation.</param>
        /// <param name="dwTimeout">Maximum time, in milliseconds, to wait before returning from this method. Use INFINITE to wait indefinitely.</param>
        /// <param name="pExprCallback">This parameter is always a null value.</param>
        /// <param name="ppResult">Returns the IDebugProperty2 object that contains the result of the expression evaluation.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise returns an error code. Some typical error codes are:
        /// Error                               Description
        /// E_EVALUATE_BUSY_WITH_EVALUATION     Another expression is currently being evaluated, and simultaneous expression evaluation is not supported.
        /// E_EVALUATE_TIMEOUT                  Evaluation timed out.
        /// </returns>
        /// <remarks>For synchronous evaluation, it is not necessary to send an event back to Visual Studio upon completion of the evaluation.</remarks>
        public int EvaluateSync( enum_EVALFLAGS dwFlags,
                                 uint dwTimeout,
                                 IDebugEventCallback2 pExprCallback,
                                 out IDebugProperty2 ppResult )
        {
            Logger.Debug( string.Empty );
            ppResult = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}