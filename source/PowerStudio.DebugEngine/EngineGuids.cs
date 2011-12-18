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
    public static class EngineGuids
    {
        public static readonly Guid ComPlusNativeEngine = new Guid( "92EF0900-2251-11D2-B72E-0000F87572EF" );
        public static readonly Guid WorkflowDebugEngine = new Guid( "6589AE11-3B51-494A-AC77-91DA1B53F35A" );
        public static readonly Guid ComPlusOnlyEngine = new Guid( "449EC4CC-30D2-4032-9256-EE18EB41B62B" );
        public static readonly Guid ComPlusOnlyEngine2 = new Guid( "5FFF7536-0C87-462D-8FD2-7971D948E6DC" );
        public static readonly Guid ComPlusOnlyEngine4 = new Guid( "FB0D4648-F776-4980-95F8-BB7F36EBC1EE" );
        public static readonly Guid NativeOnlyEngine = new Guid( "3B476D35-A401-11D2-AAD4-00C04F990171" );
        public static readonly Guid ScriptEngine = new Guid( "F200A7E7-DEA5-11D0-B854-00A0244A1DE2" );
        public static readonly Guid SilverlightEngine = new Guid( "032F4B8C-7045-4B24-ACCF-D08C9DA108FE" );
        public static readonly Guid ComPlusSqlLocalEngine = new Guid( "E04BDE58-45EC-48DB-9807-513F78865212" );
        public static readonly Guid ProteusEngine = new Guid( "3e38fd9a-56e2-4526-a86c-ba742db3b5ed" );
        public static readonly Guid SqlEngine2 = new Guid( "1202F5B4-3522-4149-BAD8-58B2079D704F" );
        public static readonly Guid PowerGUIEngine = new Guid( "C7F9F131-53AB-4FD0-8517-E54D124EA392" );
        public const string PowerStudioEngineGuidString = "462BC237-17AA-4626-9949-E7DD8C790681";
        public static readonly Guid PowerStudioEngineGuid = new Guid(PowerStudioEngineGuidString);
    }
}