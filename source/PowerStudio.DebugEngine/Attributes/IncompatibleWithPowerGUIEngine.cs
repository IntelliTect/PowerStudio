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

namespace PowerStudio.DebugEngine.Attributes
{
    public sealed class IncompatibleWithPowerGUIEngine : IncompatibleDebugEngineAttribute
    {
        #region Overrides of IncompatibleDebugEngineAttribute

        public override string Name
        {
            get { return "PowerShell"; }
        }

        public override Guid Guid
        {
            get { return EngineGuids.PowerGUIEngine; }
        }

        #endregion
    }
}