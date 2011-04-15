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
            where T : ITokenTag
    {
        protected TaggerBase( ITextBuffer buffer )
        {
            Buffer = buffer;
            Snapshot = Buffer.CurrentSnapshot;
            Buffer.Changed += BufferChanged;
            Tags = Enumerable.Empty<T>().ToList().AsReadOnly();
        }

        protected ITextBuffer Buffer { get; private set; }
        protected ITextSnapshot Snapshot { get; set; }
        protected ReadOnlyCollection<T> Tags { get; set; }

        private void BufferChanged( object sender, TextContentChangedEventArgs e )
        {
            // If this isn't the most up-to-date version of the buffer, then ignore it for now (we'll eventually get another change event).
            if ( e.After !=
                 Buffer.CurrentSnapshot )
            {
                return;
            }
            Parse();
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
        public virtual IEnumerable<ITagSpan<T>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            if ( spans.Count == 0 ||
                 Buffer.CurrentSnapshot.Length == 0 )
            {
                //there is no content in the buffer
                yield break;
            }
            ReadOnlyCollection<T> tags = Tags;
            foreach ( T tokenTag in tags )
            {
                yield return new TagSpan<T>( tokenTag.Span, tokenTag );
            }
        }

#pragma warning disable 0067
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
#pragma warning restore 0067

        protected abstract void Parse();

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

        protected virtual IEnumerable<PSParseError> GetErrorTokens(ITextSnapshot textSnapshot)
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            PSParser.Tokenize(text, out errors);
            return errors;
        }

        protected virtual SnapshotSpan AsSnapshotSpan( ITextSnapshot snapshot, PSToken token )
        {
            return new SnapshotSpan( snapshot, new Span( token.Start, token.Length ) );
        }

        protected virtual SnapshotSpan AsSnapshotSpan( ITextSnapshot snapshot, PSToken startToken, PSToken endToken )
        {
            var startSnapshot = new SnapshotSpan( snapshot, new Span( startToken.Start, startToken.Length ) );
            var endSnapshot = new SnapshotSpan( snapshot, new Span( endToken.Start, endToken.Length ) );
            return new SnapshotSpan( startSnapshot.Start, endSnapshot.End );
        }
    }
}