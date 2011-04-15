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

        #region ITagger<T> Members

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
            ITextSnapshot currentSnapshot = Snapshot;
            SnapshotSpan span =
                    new SnapshotSpan( spans[0].Start, spans[spans.Count - 1].End )
                            .TranslateTo( currentSnapshot, SpanTrackingMode.EdgeExclusive );

            foreach ( T tag in from tag in tags
                               where IsTokenInSpan( tag, currentSnapshot, span )
                               select tag )
            {
                yield return new TagSpan<T>( tag.Span, tag );
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        #endregion

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

        protected virtual bool IsTokenInSpan( T tag, ITextSnapshot snapshot, SnapshotSpan span )
        {
            return span.Contains( tag.Span.TranslateTo( snapshot, SpanTrackingMode.EdgeExclusive ) );
        }

        protected virtual void PublishTagChanges( ITextSnapshot newSnapshot, List<T> newTags )
        {
            var oldSpans =
                    new List<Span>( Tags.Select( tag => tag.Span.TranslateTo( newSnapshot,
                                                                              SpanTrackingMode.EdgeExclusive )
                                                                .Span ) );
            var newSpans =
                    new List<Span>( newTags.Select( tag => tag.Span.Span ) );

            var oldSpanCollection = new NormalizedSpanCollection( oldSpans );
            var newSpanCollection = new NormalizedSpanCollection( newSpans );

            //the changed regions are regions that appear in one set or the other, but not both.
            NormalizedSpanCollection removed =
                    NormalizedSpanCollection.Difference( oldSpanCollection, newSpanCollection );

            int changeStart = int.MaxValue;
            int changeEnd = -1;

            if ( removed.Count > 0 )
            {
                changeStart = removed[0].Start;
                changeEnd = removed[removed.Count - 1].End;
            }

            if ( newSpans.Count > 0 )
            {
                changeStart = Math.Min( changeStart, newSpans[0].Start );
                changeEnd = Math.Max( changeEnd, newSpans[newSpans.Count - 1].End );
            }

            Snapshot = newSnapshot;
            Tags = newTags.AsReadOnly();

            if ( changeStart <= changeEnd )
            {
                var snapshot = new SnapshotSpan( newSnapshot, Span.FromBounds( changeStart, changeEnd ) );
                OnTagsChanged( new SnapshotSpanEventArgs( snapshot ) );
            }
        }

        protected virtual void Parse()
        {
            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;
            List<T> tags = GetTags( newSnapshot );
            PublishTagChanges( newSnapshot, tags );
        }

        protected abstract List<T> GetTags( ITextSnapshot snapshot );

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

        protected virtual IEnumerable<PSParseError> GetErrorTokens( ITextSnapshot textSnapshot )
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            PSParser.Tokenize( text, out errors );
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