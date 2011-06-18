#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using DebugEngine.Events;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace DebugEngine
{
    public class DebugEngineEventSource : IDebugEngineEventSource
    {
        public DebugEngineEventSource( DebugEngineBase debugEngineBase, IDebugEventCallback2 debugEventCallback )
        {
            DebugEngineBase = debugEngineBase;
            DebugEventCallback = debugEventCallback;
        }

        public DebugEngineBase DebugEngineBase { get; private set; }
        public IDebugEventCallback2 DebugEventCallback { get; private set; }

        public virtual void OnAsyncBreakComplete()
        {
            var eventObject = new AsyncBreakCompleteEvent();
            OnDebugEvent( eventObject, Guids.IDebugBreakEvent2Guid );
        }

        public virtual void OnBreakpointBound( Breakpoint pendingBreakpoint, Breakpoint boundBreakpoint )
        {
            var eventObject = new BreakpointBoundEvent( pendingBreakpoint, boundBreakpoint );
            OnDebugEvent( eventObject, Guids.IDebugBreakpointBoundEvent2Guid );
        }

        public virtual void OnBreakpoint( IEnumDebugBoundBreakpoints2 boundBreakpoints )
        {
            var eventObject = new BreakpointEvent( boundBreakpoints );
            OnDebugEvent( eventObject, Guids.IDebugBreakpointEvent2Guid );
        }

        public virtual void OnDebugEngineCreate()
        {
            var eventObject = new DebugEngineCreateEvent( DebugEngineBase );
            OnDebugEvent( eventObject, Guids.EngineCreateEventGuid );
        }

        public virtual void OnLoadComplete()
        {
            var eventObject = new LoadCompleteEvent();
            OnDebugEvent( eventObject, Guids.IDebugLoadCompleteEvent2Guid );
        }

        public virtual void OnModuleLoad( DebugModuleBase debugModule,
                                          bool isLoading )
        {
            var eventObject = new ModuleLoadEvent( debugModule, isLoading );
            OnDebugEvent( eventObject, Guids.IDebugModuleLoadEvent2Guid );
        }

        public virtual void OnOutputDebugString( string outputString )
        {
            var eventObject = new OutputDebugStringEvent( outputString );
            OnDebugEvent( eventObject, Guids.IDebugOutputStringEvent2Guid );
        }

        public virtual void OnProgramCreate( IDebugProgram2 program )
        {
            OnDebugEvent( new ProgramCreateEvent(), Guids.IDebugProgramCreateEvent2Guid, program );
        }

        public virtual void OnProgramDestroy( uint exitCode )
        {
            var eventObject = new ProgramDestroyEvent( exitCode );
            OnDebugEvent( eventObject, Guids.IDebugProgramDestroyEvent2Guid );
        }

        public virtual void OnSymbolSearch( DebugModuleBase module,
                                            string debugMessage,
                                            enum_MODULE_INFO_FLAGS moduleInfoFlags )
        {
            var eventObject = new SymbolSearchEvent( module, debugMessage, moduleInfoFlags );
            OnDebugEvent( eventObject, Guids.IDebugSymbolSearchEvent2Guid );
        }

        public virtual void OnThreadCreate()
        {
            var eventObject = new ThreadCreateEvent();
            OnDebugEvent( eventObject, Guids.IDebugThreadCreateEvent2Guid );
        }

        public virtual void OnThreadDestroy( uint exitCode )
        {
            var eventObject = new ThreadDestroyEvent( exitCode );
            OnDebugEvent( eventObject, Guids.IDebugThreadDestroyEvent2Guid );
        }

        protected virtual void OnDebugEvent( IDebugEvent2 eventObject,
                                             Guid iidEvent,
                                             IDebugProgram2 program = null,
                                             IDebugThread2 thread = null,
                                             IDebugProcess2 process = null )
        {
            uint attributes;
            eventObject.GetAttributes( out attributes );
            DebugEventCallback.Event( DebugEngineBase, process, program, thread, eventObject, ref iidEvent, attributes );
        }
    }
}