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
using System.Globalization;
using Microsoft.VisualStudio.Shell;

#endregion

namespace PowerStudio.DebugEngine.Attributes
{
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = true, Inherited = true )]
    public class ProvideTypeAttribute : RegistrationAttribute
    {
        private readonly string _ClsidRegKey;

        public ProvideTypeAttribute( Type objectType )
        {
            if ( objectType == null )
            {
                throw new ArgumentNullException( "objectType" );
            }
            ObjectType = objectType;
            Guid = ObjectType.GUID;
            _ClsidRegKey = string.Format( CultureInfo.InvariantCulture,
                                          @"CLSID\{0}",
                                          ObjectType.GUID.ToString( "B" ) );
            RuntimeVersion = objectType.Assembly.ImageRuntimeVersion;
            ThreadingModel = ComThreadingModel.NotSpecified;
        }

        public ProvideTypeAttribute( string id )
        {
            if ( string.IsNullOrEmpty( id ) )
            {
                throw new ArgumentNullException( "id" );
            }
            Guid = new Guid( id );
            _ClsidRegKey = string.Format( CultureInfo.InvariantCulture,
                                          @"CLSID\{0}",
                                          Guid.ToString( "B" ) );
            ThreadingModel = ComThreadingModel.NotSpecified;
        }

        public virtual Type ObjectType { get; set; }
        public virtual Guid Guid { get; set; }
        public virtual RegistrationMethod RegisterUsing { get; set; }
        public virtual string RuntimeVersion { get; set; }
        public virtual ComThreadingModel ThreadingModel { get; set; }

        public override void Register( RegistrationContext context )
        {
            using ( Key key = context.CreateKey( _ClsidRegKey ) )
            {
                if ( ObjectType != null )
                {
                    key.SetValue( string.Empty, ObjectType.FullName );
                    key.SetValue("Class", ObjectType.FullName);
                }

                if (!string.IsNullOrEmpty(context.InprocServerPath))
                {
                    key.SetValue( "InprocServer32", context.InprocServerPath );
                }

                if ( context.RegistrationMethod !=
                     RegistrationMethod.Default )
                {
                    RegisterUsing = context.RegistrationMethod;
                }

                switch ( RegisterUsing )
                {
                    case RegistrationMethod.Default:
                    case RegistrationMethod.Assembly:
                        if (ObjectType != null)
                        {
                            key.SetValue( "Assembly", ObjectType.Assembly.FullName );
                        }
                        break;

                    case RegistrationMethod.CodeBase:
                        key.SetValue( "CodeBase", context.CodeBase );
                        break;
                }

                if ( !string.IsNullOrEmpty( RuntimeVersion ) )
                {
                    key.SetValue( "RuntimeVersion", RuntimeVersion );
                }

                if ( ThreadingModel == ComThreadingModel.NotSpecified )
                {
                    ThreadingModel = ComThreadingModel.Both;
                }

                key.SetValue( "ThreadingModel", ThreadingModel.ToString() );
            }
        }

        public override void Unregister( RegistrationContext context )
        {
            context.RemoveKey( _ClsidRegKey );
        }
    }
}