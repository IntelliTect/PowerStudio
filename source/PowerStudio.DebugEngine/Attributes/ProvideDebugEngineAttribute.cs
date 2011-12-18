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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.Shell;
using NLog;

#endregion

namespace PowerStudio.DebugEngine.Attributes
{
    [AttributeUsage( AttributeTargets.Class, Inherited = true, AllowMultiple = false )]
    public partial class ProvideDebugEngineAttribute : RegistrationAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _EngineRegKey;

        private readonly Dictionary<string, object> _Entries = new Dictionary<string, object>();

        public ProvideDebugEngineAttribute( Type objectType )
        {
            if ( objectType == null )
            {
                throw new ArgumentNullException( "objectType" );
            }
            ObjectType = objectType;
            _EngineRegKey = string.Format( CultureInfo.InvariantCulture,
                                           @"AD7Metrics\Engine\{0}",
                                           ObjectType.GUID.ToString( "B" ) );
        }

        public Type ObjectType { get; set; }

        #region Overrides of RegistrationAttribute

        /// <include file="doc\RegistrationAttribute.uex" path="docs/doc[@for="Register"]"/><devdoc>Called to register this attribute with the given context.  The context
        ///                  contains the location where the registration information should be placed.
        ///                  It also contains such as the type being registered, and path information.
        ///                  This method is called both for registration and unregistration.  The difference is
        ///                  that unregistering just uses a hive that reverses the changes applied to it.
        ///              </devdoc>
        public override void Register( RegistrationContext context )
        {
            using ( Key key = CreateKey( context ) )
            {
                SetDefaultKey( key, context.ComponentType, context.ComponentType.AssemblyQualifiedName );

                SetName( key, context.ComponentType );

                SetEngineKeys( key, context );

                SetPortSuppliers( context, key );

                SetIncompatibleEngineLists( context, key );
            }
        }

        /// <include file="doc\RegistrationAttribute.uex" path="docs/doc[@for="Unregister"]"/><devdoc>Called to unregister this attribute with the given context.  The context
        ///                 contains the location where the registration information should be removed.
        ///                 It also contains things such as the type being unregistered, and path information.
        ///             </devdoc>
        public override void Unregister( RegistrationContext context )
        {
            context.RemoveKey( _EngineRegKey );
        }

        #endregion

        public virtual Key CreateKey( RegistrationContext context )
        {
            return context.CreateKey( _EngineRegKey );
        }

        public virtual Key CreateSubkey( Key key, string subKey )
        {
            return key.CreateSubkey( subKey );
        }

        public virtual void SetDefaultKey( Key key, Type componentType, string description )
        {
            var descriptionAttribute = GetAttribute<DescriptionAttribute>( componentType );
            if ( descriptionAttribute != null &&
                 !string.IsNullOrEmpty( descriptionAttribute.Description ) )
            {
                Logger.Debug( "Description attribute found." );
                description = descriptionAttribute.Description;
            }
            Logger.Debug("Setting description to {0}", description);
            key.SetValue( string.Empty, description );
        }

        public virtual void SetName( Key key, Type componentType )
        {
            var displayNameAttribute = GetAttribute<DisplayNameAttribute>( componentType );
            if ( displayNameAttribute == null ||
                 string.IsNullOrEmpty( displayNameAttribute.DisplayName ) )
            {
                return;
            }
            Logger.Debug("Display Name attribute found. Setting name to {0}",
                         displayNameAttribute.DisplayName);
            key.SetValue( "Name", displayNameAttribute.DisplayName );
        }

        public void SetEngineKeys( Key key, RegistrationContext context )
        {
            Guid clsIdGuid;
            if ( ClsIdType != null )
            {
                Logger.Debug( "ClsIdType found. Setting CLSID to {0}", ClsIdType.GUID );
                clsIdGuid = ClsIdType.GUID;
            }
            else
            {
                Logger.Debug( "ClsIdType not found. Using Guid from component: {0}", context.ComponentType.GUID );
                clsIdGuid = context.ComponentType.GUID;
            }

            Logger.Debug( "Setting CLSID to {0}.", clsIdGuid.ToString( "B" ) );
            key.SetValue("CLSID", clsIdGuid.ToString("B"));

            foreach ( var entry in _Entries )
            {
                Logger.Debug( "Entry for {0} found, set to {1}.", entry.Key, entry.Value );
                SetValue( key, entry.Key, entry.Value, entry.Value.GetType() );
            }
        }

        public T GetValue<T>( string name )
        {
            return (T) GetValue( name, typeof (T) );
        }

        public object GetValue( string name, Type type )
        {
            object value;
            if ( !_Entries.TryGetValue( name, out value ) )
            {
                value = type.IsValueType ? Activator.CreateInstance( type ) : null;
            }
            return value;
        }

        public void SetValue( Key key, string name, object value, Type propertyType )
        {
            if ( ReferenceEquals( value, null ) )
            {
                return;
            }
            if (value is Type)
            {
                Type type = (Type) value;
                if (type.GUID == Guid.Empty)
                {
                    throw new ArgumentException( string.Format( "The type {0} must have a defined Guid.", type ) );
                }
                string valueString = type.GUID.ToString( "B" );
                Logger.Debug("Setting root {0} entry for {1} found, set to {2}.", key.ToString(), name, valueString);
                key.SetValue( name, valueString );
            }else if ( propertyType == typeof (Guid) )
            {
                string valueString = ( (Guid) value ).ToString( "B" );
                Logger.Debug( "Setting root {0} entry for {1} found, set to {2}.", key.ToString(), name, valueString );
                key.SetValue( name, valueString);
            }
            else if ( propertyType.IsValueType )
            {
                // bool, int, etc are stored as dword
                int valueString = Convert.ToInt32( value );
                Logger.Debug("Setting root {0} entry for {1} found, set to {2}.", key.ToString(), name, valueString);
                key.SetValue( name, valueString );
            }
            else
            {
                Logger.Debug("Setting root {0} entry for {1} found, set to {2}.", key.ToString(), name, value);
                key.SetValue( name, value );
            }
        }

        public virtual void SetPortSuppliers( RegistrationContext context, Key key )
        {
            var portSuppliers =
                    context.ComponentType.GetCustomAttributes( typeof (ProvidePortSupplierAttribute), true ) as
                    ProvidePortSupplierAttribute[];

            if (portSuppliers == null ||
                 portSuppliers.Length == 0)
            {
                Logger.Debug("No port supplier attributes found.");
                return;
            }
            else
            {
                Logger.Debug( "{0} port suppliers were found.", portSuppliers.Length );
            }


            Guid filterGuid;
            if ( PortSupplierType == null )
            {
                Logger.Debug("No PortSupplierType was not set.");
                filterGuid = Guid.Empty;
            }
            else
            {
                Logger.Debug( "PortSupplierType was set with Guid: {0}.", PortSupplierType.GUID.ToString( "B" ) );
                filterGuid = PortSupplierType.GUID;
            }

            var candidates = portSuppliers.Where(item => item.Guid != filterGuid).ToList();

            if ( candidates.Count == 0 )
            {
                Logger.Debug( "No port suppliers were specified other than the PortSupplierType." );
                return;
            }

            using ( Key childKey = CreateSubkey( key, "PortSupplier" ) )
            {
                int i = 0;
                foreach (ProvidePortSupplierAttribute portSupplierAttribute in candidates)
                {
                    string value = portSupplierAttribute.Guid.ToString( "B" );
                    Logger.Debug( "Adding port supplier {0} to index {1}", value, i);

                    childKey.SetValue( i.ToString(), value );
                    i++;
                }
            }
        }

        public virtual void SetIncompatibleEngineLists( RegistrationContext context, Key key )
        {
            var incompatibleEngineSource =
                    context.ComponentType.GetCustomAttributes( typeof (IncompatibleDebugEngineAttribute), true ) as
                    IncompatibleDebugEngineAttribute[];
            List<IncompatibleDebugEngineAttribute> incompatibleEngines =
                    incompatibleEngineSource.Where( item => item.AutoSelect == false ).ToList();
            List<IncompatibleDebugEngineAttribute> autoSelectIncompatibleEngines =
                    incompatibleEngineSource.Where( item => item.AutoSelect ).ToList();
            SetIncompatibleEngineList( incompatibleEngines, key, "IncompatibleList" );
            SetIncompatibleEngineList( autoSelectIncompatibleEngines, key, "AutoSelectIncompatibleList" );
        }

        public virtual void SetIncompatibleEngineList(
                ICollection<IncompatibleDebugEngineAttribute> incompatibleEngines,
                Key key,
                string listName )
        {
            if ( incompatibleEngines.Count == 0 )
            {
                Logger.Debug( "No incompatible engines specified." );
                return;
            }

            using ( Key childKey = CreateSubkey( key, listName ) )
            {
                foreach ( IncompatibleDebugEngineAttribute engineAttribute in incompatibleEngines )
                {
                    string id = engineAttribute.Guid.ToString( "B" );
                    Logger.Debug( "Adding incompatible debug engine {0} to list '{1}' with Guid {2}.",
                                  engineAttribute.Name,
                                  listName,
                                  id );
                    childKey.SetValue( engineAttribute.Name, id );
                }
            }
        }
    }

    public partial class ProvideDebugEngineAttribute
    {
        public bool AddressBP
        {
            get { return GetValue<bool>( "AddressBP" ); }
            set { _Entries["AddressBP"] = value; }
        }

        public bool AlwaysLoadLocal
        {
            get { return GetValue<bool>( "AlwaysLoadLocal" ); }
            set { _Entries["AlwaysLoadLocal"] = value; }
        }

        public bool AlwaysLoadProgramProviderLocal
        {
            get { return GetValue<bool>( "AlwaysLoadProgramProviderLocal" ); }
            set { _Entries["AlwaysLoadProgramProviderLocal"] = value; }
        }

        public bool Attach
        {
            get { return GetValue<bool>( "Attach" ); }
            set { _Entries["Attach"] = value; }
        }

        public int AutoSelectPriority
        {
            get { return GetValue<int>( "AutoSelectPriority" ); }
            set { _Entries["AutoSelectPriority"] = value; }
        }

        public bool CallStackBP
        {
            get { return GetValue<bool>( "CallStackBP" ); }
            set { _Entries["CallStackBP"] = value; }
        }

        public Type ClsIdType
        {
            get { return GetValue<Type>( "CLSID" ); }
            set { _Entries["CLSID"] = value; }
        }

        public bool ConditionalBP
        {
            get { return GetValue<bool>( "ConditionalBP" ); }
            set { _Entries["ConditionalBP"] = value; }
        }

        public bool DataBP
        {
            get { return GetValue<bool>( "DataBP" ); }
            set { _Entries["DataBP"] = value; }
        }

        public bool Disassembly
        {
            get { return GetValue<bool>( "Disassembly" ); }
            set { _Entries["Disassembly"] = value; }
        }

        public bool DumpWriting
        {
            get { return GetValue<bool>( "DumpWriting" ); }
            set { _Entries["DumpWriting"] = value; }
        }

        public bool Embedded
        {
            get { return GetValue<bool>( "Embedded" ); }
            set { _Entries["Embedded"] = value; }
        }

        public bool EnableFuncEvalQuickAbort
        {
            get { return GetValue<bool>( "EnableFuncEvalQuickAbort" ); }
            set { _Entries["EnableFuncEvalQuickAbort"] = value; }
        }

        public bool EnableTracing
        {
            get { return GetValue<bool>( "EnableTracing" ); }
            set { _Entries["EnableTracing"] = value; }
        }

        public bool ENC
        {
            get { return GetValue<bool>( "ENC" ); }
            set { _Entries["ENC"] = value; }
        }

        public bool EncUseNativeBuilder
        {
            get { return GetValue<bool>( "EncUseNativeBuilder" ); }
            set { _Entries["EncUseNativeBuilder"] = value; }
        }

        public bool EngineCanWatchProcess
        {
            get { return GetValue<bool>( "EngineCanWatchProcess" ); }
            set { _Entries["EngineCanWatchProcess"] = value; }
        }

        public bool Exceptions
        {
            get { return GetValue<bool>( "Exceptions" ); }
            set { _Entries["Exceptions"] = value; }
        }

        public bool ExcludeManualSelect
        {
            get { return GetValue<bool>( "ExcludeManualSelect" ); }
            set { _Entries["ExcludeManualSelect"] = value; }
        }

        public bool FuncEvalAbortLoggingLevel
        {
            get { return GetValue<bool>( "FuncEvalAbortLoggingLevel" ); }
            set { _Entries["FuncEvalAbortLoggingLevel"] = value; }
        }

        public string FuncEvalQuickAbortDlls
        {
            get { return GetValue<string>( "FuncEvalQuickAbortDlls" ); }
            set { _Entries["FuncEvalQuickAbortDlls"] = value; }
        }

        public string FuncEvalQuickAbortExcludeList
        {
            get { return GetValue<string>( "FuncEvalQuickAbortExcludeList" ); }
            set { _Entries["FuncEvalQuickAbortExcludeList"] = value; }
        }

        public bool FunctionBP
        {
            get { return GetValue<bool>( "FunctionBP" ); }
            set { _Entries["FunctionBP"] = value; }
        }

        public bool HitCountBP
        {
            get { return GetValue<bool>( "HitCountBP" ); }
            set { _Entries["HitCountBP"] = value; }
        }

        public bool Interop
        {
            get { return GetValue<bool>( "Interop" ); }
            set { _Entries["Interop"] = value; }
        }

        public bool JITDebug
        {
            get { return GetValue<bool>( "JITDebug" ); }
            set { _Entries["JITDebug"] = value; }
        }

        public bool JustMyCodeStepping
        {
            get { return GetValue<bool>( "JustMyCodeStepping" ); }
            set { _Entries["JustMyCodeStepping"] = value; }
        }

        public bool LoadProgramProviderUnderWOW64
        {
            get { return GetValue<bool>( "LoadProgramProviderUnderWOW64" ); }
            set { _Entries["LoadProgramProviderUnderWOW64"] = value; }
        }

        public string Name
        {
            get { return GetValue<string>( "Name" ); }
            set { _Entries["Name"] = value; }
        }

        public bool NativeInteropOK
        {
            get { return GetValue<bool>( "NativeInteropOK" ); }
            set { _Entries["NativeInteropOK"] = value; }
        }

        public Type PortSupplierType
        {
            get { return GetValue<Type>("PortSupplier"); }
            set { _Entries["PortSupplier"] = value; }
        }

        public Type ProgramProviderType
        {
            get { return GetValue<Type>("ProgramProvider"); }
            set { _Entries["ProgramProvider"] = value; }
        }

        public bool Registers
        {
            get { return GetValue<bool>( "Registers" ); }
            set { _Entries["Registers"] = value; }
        }

        public bool RemoteDebugging
        {
            get { return GetValue<bool>( "RemoteDebugging" ); }
            set { _Entries["RemoteDebugging"] = value; }
        }

        public bool RequireFullTrustForSourceServer
        {
            get { return GetValue<bool>( "RequireFullTrustForSourceServer" ); }
            set { _Entries["RequireFullTrustForSourceServer"] = value; }
        }

        public Guid Runtime
        {
            get { return GetValue<Guid>( "Runtime" ); }
            set { _Entries["Runtime"] = value; }
        }

        public bool SetNextStatement
        {
            get { return GetValue<bool>( "SetNextStatement" ); }
            set { _Entries["SetNextStatement"] = value; }
        }

        public bool SqlClr
        {
            get { return GetValue<bool>( "SqlClr" ); }
            set { _Entries["SqlClr"] = value; }
        }

        public bool SuspendThread
        {
            get { return GetValue<bool>( "SuspendThread" ); }
            set { _Entries["SuspendThread"] = value; }
        }

        public bool UseShimAPI
        {
            get { return GetValue<bool>( "UseShimAPI" ); }
            set { _Entries["UseShimAPI"] = value; }
        }
    }

    public partial class ProvideDebugEngineAttribute
    {
        public virtual bool HasAttribute<T>( ICustomAttributeProvider provider ) where T : Attribute
        {
            return provider.IsDefined( typeof (T), true );
        }

        public virtual T GetAttribute<T>( ICustomAttributeProvider provider ) where T : Attribute
        {
            if ( !HasAttribute<T>( provider ) )
            {
                return default( T );
            }
            return provider.GetCustomAttributes( typeof (T), true ).Cast<T>().Single();
        }

        public virtual IEnumerable<T> GetAttributes<T>( ICustomAttributeProvider provider ) where T : Attribute
        {
            if ( !HasAttribute<T>( provider ) )
            {
                return Enumerable.Empty<T>();
            }
            return provider.GetCustomAttributes( typeof (T), true ).Cast<T>();
        }
    }
}