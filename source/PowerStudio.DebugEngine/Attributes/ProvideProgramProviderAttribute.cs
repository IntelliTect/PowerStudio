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
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace PowerStudio.DebugEngine.Attributes
{
    [AttributeUsage( AttributeTargets.Class, Inherited = true, AllowMultiple = true )]
    public sealed class ProvideProgramProviderAttribute : ProvideTypeAttribute
    {
        public ProvideProgramProviderAttribute( Type type )
                : base( type )
        {
            if ( !typeof (IDebugProgramProvider2).IsAssignableFrom( type ) )
            {
                string message = string.Format( "The specified type [{0}] must be a debug program provider.", type );
                throw new ArgumentException( message );
            }
        }
    }
}