#region License

// 
// Copyright (c) 2011, PowerStudio.DebugEngine Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace PowerStudio.DebugEngine
{
    public interface IDebugEngineEventSource
    {
        void OnAsyncBreakComplete();
        void OnBreakpointBound( Breakpoint pendingBreakpoint, Breakpoint boundBreakpoint );
        void OnBreakpoint( IEnumDebugBoundBreakpoints2 boundBreakpoints );
        void OnDebugEngineCreate();
        void OnLoadComplete();

        void OnModuleLoad( DebugModuleBase debugModule, bool isLoading );

        void OnOutputDebugString( string outputString );
        void OnProgramCreate( IDebugProgram2 program );
        void OnProgramDestroy( uint exitCode );

        void OnSymbolSearch( DebugModuleBase module,
                             string debugMessage,
                             enum_MODULE_INFO_FLAGS moduleInfoFlags );

        void OnThreadCreate();
        void OnThreadDestroy( uint exitCode );
    }
}