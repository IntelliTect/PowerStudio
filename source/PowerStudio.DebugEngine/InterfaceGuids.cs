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

#endregion

namespace PowerStudio.DebugEngine
{
    public static class InterfaceGuids
    {
        public static readonly Guid IActivateDocumentEvent2Guid = new Guid("58f36c3d-7d07-4eba-a041-62f63e188037");
        public static readonly Guid IDebugBreakEvent2Guid = new Guid( "C7405D1D-E24B-44E0-B707-D8A5A4E1641B" );
        public static readonly Guid IDebugBreakpointBoundEvent2Guid = new Guid( "1DDDB704-CF99-4B8A-B746-DABB01DD13A0" );
        public static readonly Guid IDebugBreakpointEvent2Guid = new Guid( "501C1E21-C557-48B8-BA30-A1EAB0BC4A74" );
        public static readonly Guid IDebugLoadCompleteEvent2Guid = new Guid( "B1844850-1349-45D4-9F12-495212F5EB0B" );
        public static readonly Guid IDebugModuleLoadEvent2Guid = new Guid( "989DB083-0D7C-40D1-A9D9-921BF611A4B2" );
        public static readonly Guid IDebugOutputStringEvent2Guid = new Guid( "569C4BB1-7B82-46FC-AE28-4536DDAD753E" );
        public static readonly Guid IDebugProgramCreateEvent2Guid = new Guid( "96CD11EE-ECD4-4E89-957E-B5D496FC4139" );
        public static readonly Guid IDebugProgramDestroyEvent2Guid = new Guid( "E147E9E3-6440-4073-A7B7-A65592C714B5" );
        public static readonly Guid IDebugSymbolSearchEvent2Guid = new Guid( "638F7C54-C160-4c7b-B2D0-E0337BC61F8C" );
        public static readonly Guid IDebugThreadCreateEvent2Guid = new Guid( "2090CCFC-70C5-491D-A5E8-BAD2DD9EE3EA" );
        public static readonly Guid IDebugThreadDestroyEvent2Guid = new Guid( "2C3B7532-A36F-4A6E-9072-49BE649B8541" );
        public static readonly Guid EngineCreateEventGuid = new Guid( "46128429-894B-4C8E-B3CC-84A10515A48D" );
        public const string DefaultPortSupplierGuidString = "708C1ECA-FF48-11D2-904F-00C04FA302A1";
    }
}