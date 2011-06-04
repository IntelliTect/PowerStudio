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

namespace PowerStudio.LanguageService.Tagging.Taggers
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
                yield return new TokenTagSpan<T>( tag );
            }
        }

        /// <summary>
        ///   Occurs when [tags changed].
        /// </summary>
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        #endregion

        /// <summary>
        ///   Occurs when a non-empty text edit is applied. If the snapshot changes then we need to handle it.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "Microsoft.VisualStudio.Text.TextContentChangedEventArgs" /> instance containing the event data.</param>
        protected virtual void BufferChanged( object sender, TextContentChangedEventArgs e )
        {
            // If this isn't the most up-to-date version of the buffer, then ignore it for now (we'll eventually get another change event).
            if ( Buffer.CurrentSnapshot !=
                 e.After )
            {
                return;
            }
            Parse();
        }

        /// <summary>
        ///   Raises the <see cref = "E:TagsChanged" /> event.
        /// </summary>
        /// <param name = "args">The <see cref = "Microsoft.VisualStudio.Text.SnapshotSpanEventArgs" /> instance containing the event data.</param>
        protected virtual void OnTagsChanged( SnapshotSpanEventArgs args )
        {
            EventHandler<SnapshotSpanEventArgs> handler = TagsChanged;
            if ( handler != null )
            {
                handler( this, args );
            }
        }

        /// <summary>
        ///   Determines whether given token is in the target span translating to the current snaphot
        ///   if needed.
        /// </summary>
        /// <param name = "tag">The tag.</param>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "span">The span.</param>
        /// <returns>
        ///   <c>true</c> if the tag is in the span for the snapshot; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsTokenInSpan( T tag, ITextSnapshot snapshot, SnapshotSpan span )
        {
            return IsSpanContainedInTargetSpan( snapshot, span, tag.Span );
        }

        /// <summary>
        ///   Parses this instance for the current buffer. All tags are parsed and then any
        ///   changes are published through the <see cref = "E:TagsChanged" /> event. This also
        ///   updates the current snapshot for this tagger.
        /// </summary>
        protected virtual void Parse()
        {
            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;
            List<T> tags = GetTags( newSnapshot );
            PublishTagChanges( newSnapshot, tags );
        }

        protected abstract List<T> GetTags( ITextSnapshot snapshot );

        /// <summary>
        ///   Publishes the tag changes.
        /// </summary>
        /// <remarks>
        ///   Once the tags are parsed from the current document, they are passed into this method
        ///   in order to determine if anything has changed. Once the change detection is done,
        ///   it replaces <see cref = "Snapshot" /> for the tagger with the parameter passed in as
        ///   well as the <see cref = "Tags" />. If there were any changes, the <see cref = "E:TagsChanged" />
        ///   is raised with the combined snapshot of all changes made.
        /// </remarks>
        /// <param name = "newSnapshot">The new snapshot.</param>
        /// <param name = "newTags">The new tags.</param>
        protected virtual void PublishTagChanges( ITextSnapshot newSnapshot, List<T> newTags )
        {
            ReadOnlyCollection<T> currentTags = Tags;
            var oldSpans =
                    new List<Span>( currentTags.Select( tag => tag.Span.TranslateTo( newSnapshot,
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

        /// <summary>
        ///   Gets the tokens by parsing the text snapshot, optionally including any errors.
        /// </summary>
        /// <param name = "textSnapshot">The text snapshot.</param>
        /// <param name = "includeErrors">if set to <c>true</c> [include errors].</param>
        /// <returns></returns>
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

        /// <summary>
        ///   Gets the error tokens by parsing the text snapshot.
        /// </summary>
        /// <param name = "textSnapshot">The text snapshot.</param>
        /// <returns></returns>
        protected virtual IEnumerable<PSParseError> GetErrorTokens( ITextSnapshot textSnapshot )
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            PSParser.Tokenize( text, out errors );
            return errors;
        }

        protected virtual bool IsSpanContainedInTargetSpan( ITextSnapshot snapshot,
                                                            SnapshotSpan sourceSpan,
                                                            SnapshotSpan targetSpan )
        {
            if ( snapshot != sourceSpan.Snapshot )
            {
                // need to map to the new snapshot before we can detect overlap
                sourceSpan = sourceSpan.TranslateTo( snapshot, SpanTrackingMode.EdgeExclusive );
            }
            if ( snapshot != targetSpan.Snapshot )
            {
                // need to map to the new snapshot before we can detect overlap
                targetSpan = targetSpan.TranslateTo( snapshot, SpanTrackingMode.EdgeExclusive );
            }
            return sourceSpan.Contains( targetSpan );
        }

        protected virtual bool IsSnapshotPointContainedInSpan( ITextSnapshot snapshot,
                                                               SnapshotPoint snapshotPoint,
                                                               SnapshotSpan sourceSpan )
        {
            if ( snapshot != sourceSpan.Snapshot )
            {
                // need to map to the new snapshot before we can detect overlap
                sourceSpan = sourceSpan.TranslateTo( snapshot, SpanTrackingMode.EdgeExclusive );
            }
            if ( snapshot != snapshotPoint.Snapshot )
            {
                // need to map to the new snapshot before we can detect overlap
                snapshotPoint = snapshotPoint.TranslateTo( snapshot, PointTrackingMode.Positive );
            }
            return sourceSpan.Contains( snapshotPoint );
        }

        /// <summary>
        ///   Creates a <see cref = "SnapshotSpan" /> for the given text snapshot and token.
        /// </summary>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "token">The token.</param>
        /// <returns></returns>
        protected virtual SnapshotSpan CreateSnapshotSpan( ITextSnapshot snapshot, PSToken token )
        {
            return new SnapshotSpan( snapshot, new Span( token.Start, token.Length ) );
        }

        /// <summary>
        ///   Creates a <see cref = "SnapshotSpan" /> for the given text snapshot covering all text between
        ///   the start and end tokens - (startToken, endToken)
        /// </summary>
        /// <param name = "snapshot">The current snapshot.</param>
        /// <param name = "startToken">The start token of the span.</param>
        /// <param name = "endToken">The end token of the span.</param>
        /// <returns></returns>
        protected virtual SnapshotSpan CreateSnapshotSpan( ITextSnapshot snapshot, PSToken startToken, PSToken endToken )
        {
            SnapshotSpan startSnapshot = CreateSnapshotSpan( snapshot, startToken );
            SnapshotSpan endSnapshot = CreateSnapshotSpan( snapshot, endToken );
            return new SnapshotSpan( startSnapshot.Start, endSnapshot.End );
        }
    }
}