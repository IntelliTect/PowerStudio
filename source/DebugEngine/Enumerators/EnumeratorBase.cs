#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio;

#endregion

namespace DebugEngine.Enumerators
{
    public abstract class EnumeratorBase<TItem, TEnumerator>
            where TEnumerator : class
    {
        protected EnumeratorBase( TItem[] data )
        {
            Data = data;
            Position = 0;
        }

        protected TItem[] Data { get; private set; }
        private uint Position { get; set; }

        /// <summary>
        /// Returns the next set of elements from the enumeration.
        /// </summary>
        /// <param name="celt">The number of elements to retrieve. Also specifies the maximum size of the rgelt array.</param>
        /// <param name="rgelt">Array of T elements to be filled in.</param>
        /// <param name="celtFetched">Returns the number of elements actually returned in rgelt.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if fewer than the requested number of elements could be returned; otherwise, returns an error code.</returns>
        public virtual int Next( uint celt, TItem[] rgelt, out uint celtFetched )
        {
            return Move( celt, rgelt, out celtFetched );
        }

        /// <summary>
        /// Skips over the specified number of elements.
        /// </summary>
        /// <param name="celt">Number of elements to skip.</param>
        /// <returns>If successful, returns S_OK. Returns S_FALSE if celt is greater than the number of remaining elements; otherwise, returns an error code.</returns>
        /// <remarks>If celt specifies a value greater than the number of remaining elements, the enumeration is set to the end and S_FALSE is returned.</remarks>
        public virtual int Skip( uint celt )
        {
            uint celtFetched;

            return Move( celt, null, out celtFetched );
        }

        /// <summary>
        /// Resets the enumeration to the first element.
        /// </summary>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>After this method is called, the next call to the ::Next method returns the first element of the enumeration.</remarks>
        public virtual int Reset()
        {
            lock ( this )
            {
                Position = 0;

                return VSConstants.S_OK;
            }
        }

        /// <summary>
        /// Returns a copy of the current enumeration as a separate object.
        /// </summary>
        /// <param name="ppEnum">Returns a copy of this enumeration as a separate object.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>The copy of the enumeration has the same state as the original at the time this method is called. However, the copy's and the original's states are separate and can be changed individually.</remarks>
        public virtual int Clone( out TEnumerator ppEnum )
        {
            ppEnum = null;
            return VSConstants.E_NOTIMPL;
        }

        /// <summary>
        /// Returns the number of elements in the enumeration.
        /// </summary>
        /// <param name="pcelt">Returns the number of elements in the enumeration.</param>
        /// <returns>If successful, returns S_OK; otherwise, returns an error code.</returns>
        /// <remarks>This method is not part of the customary COM enumeration interface which specifies that only the Next, Clone, Skip, and Reset methods need to be implemented.</remarks>
        public virtual int GetCount( out uint pcelt )
        {
            pcelt = (uint) Data.Length;
            return VSConstants.S_OK;
        }

        private int Move( uint celt, TItem[] rgelt, out uint celtFetched )
        {
            lock ( this )
            {
                int hr = VSConstants.S_OK;
                celtFetched = (uint) Data.Length - Position;

                if ( celt > celtFetched )
                {
                    hr = VSConstants.S_FALSE;
                }
                else if ( celt < celtFetched )
                {
                    celtFetched = celt;
                }

                if ( rgelt != null )
                {
                    for ( int c = 0; c < celtFetched; c++ )
                    {
                        rgelt[c] = Data[Position + c];
                    }
                }

                Position += celtFetched;

                return hr;
            }
        }
    }
}