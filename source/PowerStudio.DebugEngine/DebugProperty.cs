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
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Debugger.Interop;
using NLog;

#endregion

namespace PowerStudio.DebugEngine
{
    public abstract class DebugProperty : IDebugProperty2
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Implementation of IDebugProperty2

        /// <summary>
        /// Gets the DEBUG_PROPERTY_INFO structure that describes a property.
        /// </summary>
        /// <param name="dwFields">A combination of values from the DEBUGPROP_INFO_FLAGS enumeration that specifies which fields are to be filled out in the pPropertyInfo structure.</param>
        /// <param name="dwRadix">Radix to be used in formatting any numerical information.</param>
        /// <param name="dwTimeout">Specifies the maximum time, in milliseconds, to wait before returning from this method. Use INFINITE to wait indefinitely.</param>
        /// <param name="rgpArgs">Reserved for future use; set to a null value.</param>
        /// <param name="dwArgCount">Reserved for future use; set to zero.</param>
        /// <param name="pPropertyInfo">A DEBUG_PROPERTY_INFO structure that is filled in with the description of the property.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code.</returns>
        public virtual int GetPropertyInfo( enum_DEBUGPROP_INFO_FLAGS dwFields,
                                            uint dwRadix,
                                            uint dwTimeout,
                                            IDebugReference2[] rgpArgs,
                                            uint dwArgCount,
                                            DEBUG_PROPERTY_INFO[] pPropertyInfo )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets the value of a property from a given string.
        /// </summary>
        /// <param name="pszValue">A string containing the value to be set.</param>
        /// <param name="dwRadix">A radix to be used in interpreting any numerical information. This can be 0 to attempt to determine the radix automatically.</param>
        /// <param name="dwTimeout">Specifies the maximum time, in milliseconds, to wait before returning from this method. Use INFINITE to wait indefinitely.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise returns error code. The following table shows other possible values.
        /// Value                                   Description
        /// E_SETVALUE_VALUE_CANNOT_BE_SET          The string could not be converted into a property value, or the property value could not be set.
        /// E_SETVALUE_VALUE_IS_READONLY            The property is read-only.
        /// </returns>
        public virtual int SetValueAsString( string pszValue, uint dwRadix, uint dwTimeout )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Sets the value of this property to the value of the given reference.
        /// </summary>
        /// <param name="rgpArgs">An array of arguments to pass to the managed code property setter. If the property setter does not take arguments or if this IDebugProperty2 object does not refer to such a property setter, rgpArgs should be a null value. This parameter is typically a null value.</param>
        /// <param name="dwArgCount">The number of arguments in the rgpArgs array.</param>
        /// <param name="pValue">A reference, in the form of an IDebugReference2 object, to the value to use to set this property.</param>
        /// <param name="dwTimeout">How long to take to set the value, in milliseconds. A typical value is INFINITE. This affects the length of time that any possible evaluation can take.</param>
        /// <returns>
        /// If successful, returns S_OK; otherwise returns an error code, typically one of the following:
        /// Error                                    Description
        /// E_SETVALUEASREFERENCE_NOTSUPPORTED       Setting the value from a reference is not supported.
        /// E_SETVALUE_VALUE_CANNOT_BE_SET           The value cannot be set, as this property refers to a method.
        /// E_SETVALUE_VALUE_IS_READONLY             The value is read-only and cannot be set.
        /// E_NOTIMPL                                The method is not implemented.
        /// </returns>
        public virtual int SetValueAsReference( IDebugReference2[] rgpArgs,
                                                uint dwArgCount,
                                                IDebugReference2 pValue,
                                                uint dwTimeout )
        {
            Logger.Debug( string.Empty );
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Retrieves a list of the children of the property.
        /// </summary>
        /// <param name="dwFields">A combination of flags from the DEBUGPROP_INFO_FLAGS enumeration that specifies which fields in the enumerated DEBUG_PROPERTY_INFO structures are to be filled in.</param>
        /// <param name="dwRadix">Specifies the radix to be used in formatting any numerical information.</param>
        /// <param name="guidFilter">GUID of the filter used with the dwAttribFilter and pszNameFilter parameters to select which DEBUG_PROPERTY_INFO children are to be enumerated. For example, guidFilterLocals filters for local variables.</param>
        /// <param name="dwAttribFilter">A combination of flags from the DBG_ATTRIB_FLAGS enumeration that specifies what type of objects to enumerate, for example DBG_ATTRIB_METHOD for all methods that might be children of this property. Used in combination with the guidFilter and pszNameFilter parameters.</param>
        /// <param name="pszNameFilter">The name of the filter used with the guidFilter and dwAttribFilter parameters to select which DEBUG_PROPERTY_INFO children are to be enumerated. For example, setting this parameter to "MyX" filters for all children with the name "MyX."</param>
        /// <param name="dwTimeout">Specifies the maximum time, in milliseconds, to wait before returning from this method. Use INFINITE to wait indefinitely.</param>
        /// <param name="ppEnum">Returns an IEnumDebugPropertyInfo2 object containing a list of the child properties.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code.</returns>
        public virtual int EnumChildren( enum_DEBUGPROP_INFO_FLAGS dwFields,
                                         uint dwRadix,
                                         ref Guid guidFilter,
                                         enum_DBG_ATTRIB_FLAGS dwAttribFilter,
                                         string pszNameFilter,
                                         uint dwTimeout,
                                         out IEnumDebugPropertyInfo2 ppEnum )
        {
            Logger.Debug( string.Empty );
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the parent property of a property.
        /// </summary>
        /// <param name="ppParent">Returns an IDebugProperty2 object that represents the parent of the property.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code. Returns S_GETPARENT_NO_PARENT if there is no parent.</returns>
        public virtual int GetParent( out IDebugProperty2 ppParent )
        {
            Logger.Debug( string.Empty );
            ppParent = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the derived-most property of a property.
        /// </summary>
        /// <param name="ppDerivedMost">Returns an IDebugProperty2 object that represents the derived-most property.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code. Returns S_GETDERIVEDMOST_NO_DERIVED_MOST if there is no derived-most property to retrieve.</returns>
        /// <remarks>For example, if this property describes an object that implements ClassRoot but which is actually an instantiation of ClassDerived that is derived from ClassRoot, then this method returns an IDebugProperty2 object describing the ClassDerived object.</remarks>
        public virtual int GetDerivedMostProperty( out IDebugProperty2 ppDerivedMost )
        {
            Logger.Debug( string.Empty );
            ppDerivedMost = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the memory bytes that compose the value of a property.
        /// </summary>
        /// <param name="ppMemoryBytes">Returns an IDebugMemoryBytes2 object that can be used to retrieve the memory that contains the value of the property.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code. Returns S_GETMEMORYBYTES_NO_MEMORY_BYTES if there are no memory bytes to retrieve.</returns>
        public virtual int GetMemoryBytes( out IDebugMemoryBytes2 ppMemoryBytes )
        {
            Logger.Debug( string.Empty );
            ppMemoryBytes = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the memory context of the property value.
        /// </summary>
        /// <param name="ppMemory">Returns the IDebugMemoryContext2 object that represents the memory associated with this property.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code. Returns S_GETMEMORYCONTEXT_NO_MEMORY_CONTEXT if there is no memory context to retrieve.</returns>
        public virtual int GetMemoryContext( out IDebugMemoryContext2 ppMemory )
        {
            Logger.Debug( string.Empty );
            ppMemory = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets the size, in bytes, of the property value.
        /// </summary>
        /// <param name="pdwSize">Returns the size, in bytes, of the property value.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code. Returns S_GETSIZE_NO_SIZE if the property has no size.</returns>
        public virtual int GetSize( out uint pdwSize )
        {
            Logger.Debug( string.Empty );
            pdwSize = 0;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Returns a reference to the property's value.
        /// </summary>
        /// <param name="ppReference">Returns an IDebugReference2 object representing a reference to the property's value.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code, typically E_NOTIMPL or E_GETREFERENCE_NO_REFERENCE.</returns>
        public virtual int GetReference( out IDebugReference2 ppReference )
        {
            Logger.Debug( string.Empty );
            ppReference = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Gets extended information for the property.
        /// </summary>
        /// <param name="guidExtendedInfo">GUID that determines the type of extended information to be retrieved. See Remarks for details.</param>
        /// <param name="pExtendedInfo">Returns a VARIANT (C++) or object (C#) that can be used to retrieve the extended property information. For example, this parameter might return an IUnknown interface that can be queried for an IDebugDocumentText2 interface. See Remarks for details.</param>
        /// <returns>If successful, returns S_OK; otherwise returns error code. Returns S_GETEXTENDEDINFO_NO_EXTENDEDINFO if there is no extended information to retrieve.</returns>
        /// <remarks>
        /// This method exists for the purpose of retrieving information that does not lend itself to being retrieved by calling the IDebugProperty2::GetPropertyInfo method.
        /// 
        /// The following GUIDs are typically recognized by this method (the GUID values are specified for C# since the name is not available in any assembly). Additional GUIDs can be created for internal use.
        /// 
        /// Name                      GUID                                     Description
        /// guidDocument              {3f98de84-fee9-11d0-b47f-00a0244a1dd2}   Returns an IUnknown interface to the document. Typically, the IDebugDocumentText2 interface can be obtained from this IUnknown interface.
        /// guidCodeContext           {e2fc65e-56ce-11d1-b528-00aax004a8797}   Returns an IUnknown interface to the document context. Typically, the IDebugDocumentContext2 interface can be obtained from this IUnknown interface.
        /// guidCustomViewerSupported {d9c9da31-ffbe-4eeb-9186-23121e3c088c}   Returns a string containing the CLSID of a custom viewer, typically implemented by an expression evaluator.
        /// guidExtendedInfoSlot      {6df235ad-82c6-4292-9c97-7389770bc42f}   Returns a 32-bit number representing the desired slot number if this property represents a managed code local address.
        /// guidExtendedInfoSignature {b5fb6d46-f805-417f-96a3-8ba737073ffd}   Returns a string containing the signature of the variable associated with the property object.
        /// </remarks>
        public virtual int GetExtendedInfo( ref Guid guidExtendedInfo, out object pExtendedInfo )
        {
            Logger.Debug( string.Empty );
            pExtendedInfo = null;
            return VSConstants.E_NOTIMPL;
        }

        #endregion
    }
}