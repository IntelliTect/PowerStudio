#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using Microsoft.VisualStudio.Debugger.Interop;

#endregion

namespace PowerStudio.Debugger
{
    public class StackFrameCollection : IEnumDebugFrameInfo2
    {
        #region Implementation of IEnumDebugFrameInfo2

        /// <summary>
        /// Returns the next set of elements from the enumeration.
        /// </summary>
        /// <param name="celt">The number of elements to retrieve. Also specifies the maximum size of the rgelt array.</param>
        /// <param name="rgelt">Array of FRAMEINFO elements to be filled in.</param>
        /// <param name="pceltFetched">Returns the number of elements actually returned in rgelt.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if fewer than the requested number of elements could be returned; otherwise, returns an error code.</returns>
        public int Next( uint celt, FRAMEINFO[] rgelt, ref uint pceltFetched )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Skips over the specified number of elements.
        /// </summary>
        /// <param name="celt">Number of elements to skip.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if celt is greater than the number of remaining elements; otherwise, returns an error code.</returns>
        /// <remarks>If celt specifies a value greater than the number of remaining elements, the enumeration is set to the end and S_FALSE is returned.</remarks>
        public int Skip( uint celt )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets the enumeration to the first element.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>After this method is called, the next call to the IEnumDebugFrameInfo2::Next method returns the first element of the enumeration.</remarks>
        public int Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a copy of the current enumeration as a separate object.
        /// </summary>
        /// <param name="ppEnum">Returns a copy of this enumeration as a separate object.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The copy of the enumeration has the same state as the original at the time this method is called. However, the copy's and the original's states are separate and can be changed individually.</remarks>
        public int Clone( out IEnumDebugFrameInfo2 ppEnum )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the number of elements in the enumeration.
        /// </summary>
        /// <param name="pcelt">Returns the number of elements in the enumeration.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method is not part of the customary COM enumeration interface which specifies that only the Next, Clone, Skip, and Reset methods need to be implemented.</remarks>
        public int GetCount( out uint pcelt )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}