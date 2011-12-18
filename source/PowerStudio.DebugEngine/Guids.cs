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
    public static class Guids
    {
        public static readonly Guid FilterRegistersGuid = new Guid("223ae797-bd09-4f28-8241-2763bdc5f713");
        public static readonly Guid FilterLocalsGuid = new Guid("b200f725-e725-4c53-b36a-1ec27aef12ef");
        public static readonly Guid FilterAllLocalsGuid = new Guid("196db21f-5f22-45a9-b5a3-32cddb30db06");
        public static readonly Guid FilterArgsGuid = new Guid("804bccea-0475-4ae7-8a46-1862688ab863");
        public static readonly Guid FilterLocalsPlusArgsGuid = new Guid("e74721bb-10c0-40f5-807f-920d37f95419");
        public static readonly Guid FilterAllLocalsPlusArgsGuid = new Guid("939729a8-4cb0-4647-9831-7ff465240d5f");
        public static readonly Guid CppLanguageGuid = new Guid("3a12d0b7-c26c-11d0-b442-00a0244a1dd2");
    }
}