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
using PowerStudio.DebugEngine.Events;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    public class DebugEngineEventSource : IDebugEngineEventSource
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DebugEngineEventSource( DebugEngineBase debugEngineBase, IDebugEventCallback2 debugEventCallback )
        {
            Logger.Debug( string.Empty );
            DebugEngineBase = debugEngineBase;
            DebugEventCallback = debugEventCallback;
        }

        public DebugEngineBase DebugEngineBase { get; private set; }
        public IDebugEventCallback2 DebugEventCallback { get; private set; }

        #region IDebugEngineEventSource Members

        public virtual void OnAsyncBreakComplete()
        {
            Logger.Debug( string.Empty );
            var eventObject = new AsyncBreakCompleteEvent();
            OnDebugEvent( eventObject, InterfaceGuids.IDebugBreakEvent2Guid );
        }

        public virtual void OnBreakpointBound( Breakpoint pendingBreakpoint, Breakpoint boundBreakpoint )
        {
            Logger.Debug( string.Empty );
            var eventObject = new BreakpointBoundEvent( pendingBreakpoint, boundBreakpoint );
            OnDebugEvent( eventObject, InterfaceGuids.IDebugBreakpointBoundEvent2Guid );
        }

        public virtual void OnBreakpoint( IEnumDebugBoundBreakpoints2 boundBreakpoints )
        {
            Logger.Debug( string.Empty );
            var eventObject = new BreakpointEvent( boundBreakpoints );
            OnDebugEvent( eventObject, InterfaceGuids.IDebugBreakpointEvent2Guid );
        }

        public virtual void OnDebugEngineCreate()
        {
            Logger.Debug( string.Empty );
            var eventObject = new DebugEngineCreateEvent( DebugEngineBase );
            OnDebugEvent( eventObject, InterfaceGuids.EngineCreateEventGuid );
        }

        public virtual void OnLoadComplete()
        {
            Logger.Debug( string.Empty );
            var eventObject = new LoadCompleteEvent();
            OnDebugEvent( eventObject, InterfaceGuids.IDebugLoadCompleteEvent2Guid );
        }

        public virtual void OnModuleLoad( DebugModuleBase debugModule,
                                          bool isLoading )
        {
            Logger.Debug( string.Empty );
            var eventObject = new ModuleLoadEvent( debugModule, isLoading );
            OnDebugEvent( eventObject, InterfaceGuids.IDebugModuleLoadEvent2Guid );
        }

        public virtual void OnOutputDebugString( string outputString )
        {
            Logger.Debug( string.Empty );
            var eventObject = new OutputDebugStringEvent( outputString );
            OnDebugEvent( eventObject, InterfaceGuids.IDebugOutputStringEvent2Guid );
        }

        public virtual void OnProgramCreate( IDebugProgram2 program )
        {
            Logger.Debug( string.Empty );
            OnDebugEvent( new ProgramCreateEvent(), InterfaceGuids.IDebugProgramCreateEvent2Guid, program );
        }

        public virtual void OnProgramDestroy( uint exitCode )
        {
            Logger.Debug( string.Empty );
            var eventObject = new ProgramDestroyEvent( exitCode );
            OnDebugEvent( eventObject, InterfaceGuids.IDebugProgramDestroyEvent2Guid );
        }

        public virtual void OnSymbolSearch( DebugModuleBase module,
                                            string debugMessage,
                                            enum_MODULE_INFO_FLAGS moduleInfoFlags )
        {
            Logger.Debug( string.Empty );
            var eventObject = new SymbolSearchEvent( module, debugMessage, moduleInfoFlags );
            OnDebugEvent( eventObject, InterfaceGuids.IDebugSymbolSearchEvent2Guid );
        }

        public virtual void OnThreadCreate()
        {
            Logger.Debug( string.Empty );
            var eventObject = new ThreadCreateEvent();
            OnDebugEvent( eventObject, InterfaceGuids.IDebugThreadCreateEvent2Guid );
        }

        public virtual void OnThreadDestroy( uint exitCode )
        {
            Logger.Debug( string.Empty );
            var eventObject = new ThreadDestroyEvent( exitCode );
            OnDebugEvent( eventObject, InterfaceGuids.IDebugThreadDestroyEvent2Guid );
        }

        #endregion

        public virtual void OnActivateDocumentEvent( IDebugDocumentContext2 documentContext,
                                                     IDebugDocument2 document = null )
        {
            Logger.Debug( string.Empty );
            var eventObject = new ActivateDocumentEvent( documentContext, document );
            OnDebugEvent( eventObject, InterfaceGuids.IActivateDocumentEvent2Guid );
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