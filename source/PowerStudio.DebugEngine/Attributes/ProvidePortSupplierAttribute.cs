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
    public class ProvidePortSupplierAttribute : ProvideTypeAttribute
    {
        public ProvidePortSupplierAttribute( Type objectType )
                : base( objectType )
        {
            if ( !typeof (IDebugPortSupplier2).IsAssignableFrom( objectType ) )
            {
                string message = string.Format( "The specified type [{0}] must be a debug program provider.", objectType );
                throw new ArgumentException( message );
            }
        }

        public ProvidePortSupplierAttribute( string id )
                : base( id )
        {
        }
    }
}