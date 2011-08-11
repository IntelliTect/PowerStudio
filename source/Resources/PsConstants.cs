#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

namespace PowerStudio.Resources
{
    public static class PsConstants
    {
        public const string PsProjExtension = "psproj";
#if DEBUG
        public const string DefaultRegistryRoot = @"Software\\Microsoft\\VisualStudio\\10.0Exp";
#else
        public const string DefaultRegistryRoot = @"Software\\Microsoft\\VisualStudio\\10.0";
#endif

        public const string LanuageServiceGuid = "4C8462B0-F114-44F4-852B-1E293174F7CB";
        public const string ProjectSystemPackageGuid = "41964A92-39EA-4BBE-A8C2-9F3540A373D1";
        public const string ProjectPackageGuid = "4C20D2C8-5C4A-4D5D-A662-EF636AED49B5";
        public const string ProjectFactoryGuid = "445B713D-FB48-43BA-AC8D-40C1F6ADBCD0";
        public const string ProjectNodeGuid = "4B6B1A2B-8D86-4734-A9E9-69C55C6274C1";
        public const string ProjectFileItemGuid = "4D54A87D-7103-4215-BB93-FFBBBF503730";

        public const string GeneralPropertyPageGuid = "45AF33B3-2533-4780-AB68-6A48368B097E";
        public const string ProgramProviderGuid = "456E86AA-0434-4014-9A0C-92B9A5F6DF04";
        public const string PortSupplierGuid = "45488C3B-17CA-4B64-B3DE-AF25E1D53644";
        public const string PortGuid = "46A72263-D468-40C1-AD37-D3EA24C9F931";
    }
}