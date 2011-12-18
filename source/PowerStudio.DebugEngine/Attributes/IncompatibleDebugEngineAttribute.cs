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
    [AttributeUsage( AttributeTargets.Class, Inherited = true, AllowMultiple = true )]
    public abstract class IncompatibleDebugEngineAttribute : Attribute
    {
        protected IncompatibleDebugEngineAttribute()
                : this( false )
        {
        }

        private IncompatibleDebugEngineAttribute( bool autoSelect )
        {
            AutoSelect = autoSelect;
        }

        public abstract string Name { get; }
        public abstract Guid Guid { get; }
        public bool AutoSelect { get; set; }
    }
}