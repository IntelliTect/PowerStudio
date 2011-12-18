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
    public abstract class DebugPortNotify : IDebugPortNotify2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugPortNotify2

        /// <summary>
        /// Registers a program that can be debugged with the port it is running on.
        /// </summary>
        /// <param name="pProgramNode">An IDebugProgramNode2 object that represents the program to be registered.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>A program node can be unregistered from the port by calling the IDebugPortNotify2::RemoveProgramNode method.</remarks>
        public virtual int AddProgramNode( IDebugProgramNode2 pProgramNode )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Unregisters a program that can be debugged from the port it is running on.
        /// </summary>
        /// <param name="pProgramNode">An IDebugProgramNode2 object that represents the program to be unregistered.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        public virtual int RemoveProgramNode( IDebugProgramNode2 pProgramNode )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}