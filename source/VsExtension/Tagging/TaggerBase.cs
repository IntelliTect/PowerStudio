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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public abstract class TaggerBase<T> : ITagger<T>
            where T : ITag
    {
        protected TaggerBase( ITextBuffer buffer )
        {
            Buffer = buffer;
            Snapshot = Buffer.CurrentSnapshot;
            Buffer.Changed += BufferChanged;
        }

        protected ITextBuffer Buffer { get; private set; }
        protected ITextSnapshot Snapshot { get; set; }

        private void BufferChanged( object sender, TextContentChangedEventArgs e )
        {
            // If this isn't the most up-to-date version of the buffer, then ignore it for now (we'll eventually get another change event).
            if ( e.After !=
                 Buffer.CurrentSnapshot )
            {
                return;
            }
            ReParse();
        }

        protected void OnTagsChanged( SnapshotSpanEventArgs args )
        {
            EventHandler<SnapshotSpanEventArgs> handler = TagsChanged;
            if ( handler != null )
            {
                handler( this, args );
            }
        }

        /// <summary>
        ///   Gets all the tags that overlap the <paramref name = "spans" />.
        /// </summary>
        /// <param name = "spans">The spans to visit.</param>
        /// <returns>
        ///   A <see cref = "T:Microsoft.VisualStudio.Text.Tagging.ITagSpan`1" /> for each tag.
        /// </returns>
        /// <remarks>
        ///   <para>
        ///     Taggers are not required to return their tags in any specific order.
        ///   </para>
        ///   <para>
        ///     The recommended way to implement this method is by using generators ("yield return"),
        ///     which allows lazy evaluation of the entire tagging stack.
        ///   </para>
        /// </remarks>
        public abstract IEnumerable<ITagSpan<T>> GetTags( NormalizedSnapshotSpanCollection spans );

#pragma warning disable 0067
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore 0067

        protected abstract void ReParse();

        protected virtual IEnumerable<PSToken> GetTokens( ITextSnapshot textSnapshot, bool includeErrors )
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            Collection<PSToken> tokens = PSParser.Tokenize( text, out errors );
            if ( includeErrors )
            {
                return tokens.Union( errors.Select( error => error.Token ) ).ToList();
            }
            return tokens;
        }
    }
}