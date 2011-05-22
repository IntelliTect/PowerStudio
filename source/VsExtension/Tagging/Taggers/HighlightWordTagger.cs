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
using System.Linq;
using System.Reactive.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Tagging.Taggers
{
    public class HighlightWordTagger : ViewTaggerBase<HighlightWordTag>
    {
        private static readonly TimeSpan WordHighlightDelay = TimeSpan.FromMilliseconds( 500 );
        private readonly object _UpdateLock = new object();

        public HighlightWordTagger( ITextView view,
                                    ITextBuffer buffer,
                                    ITextSearchService textSearchService,
                                    ITextStructureNavigator textStructureNavigator )
                : base( view, buffer )
        {
            TextSearchService = textSearchService;
            TextStructureNavigator = textStructureNavigator;
            WordSpans = new NormalizedSnapshotSpanCollection();
            CurrentWord = null;
            Observable.FromEventPattern( View.Caret, "PositionChanged" )
                    .DistinctUntilChanged()
                    .Throttle( WordHighlightDelay )
                    .Subscribe(
                            eventPattern =>
                            UpdateAtCaretPosition(
                                    ( (CaretPositionChangedEventArgs) eventPattern.EventArgs ).NewPosition ) );
            Parse();
        }

        private ITextSearchService TextSearchService { get; set; }
        private ITextStructureNavigator TextStructureNavigator { get; set; }
        private NormalizedSnapshotSpanCollection WordSpans { get; set; }
        private SnapshotSpan? CurrentWord { get; set; }
        private SnapshotPoint RequestedPoint { get; set; }

        protected override List<HighlightWordTag> GetTags( ITextSnapshot snapshot )
        {
            if ( CurrentWord == null )
            {
                return new List<HighlightWordTag>();
            }

            NormalizedSnapshotSpanCollection currentSpans = WordSpans;
            return currentSpans.Select( span => new HighlightWordTag { Span = span } ).ToList();
        }

        protected virtual IEnumerable<SnapshotSpan> FindAllMatches( SnapshotSpan currentWord )
        {
            var findData = new FindData( currentWord.GetText(), currentWord.Snapshot );
            findData.FindOptions = FindOptions.WholeWord;
            return TextSearchService.FindAll( findData );
        }

        protected virtual bool GetSelectedWord( SnapshotPoint currentRequest, ref TextExtent word )
        {
            if ( WordExtentIsValid( currentRequest, word ) )
            {
                return true;
            }

            // Before we retry, make sure it is worthwhile
            if ( word.Span.Start != currentRequest ||
                 currentRequest == currentRequest.GetContainingLine().Start ||
                 char.IsWhiteSpace( ( currentRequest - 1 ).GetChar() ) )
            {
                return false;
            }

            // Try again, one character previous. 
            // If the caret is at the end of a word, pick up the word.
            word = TextStructureNavigator.GetExtentOfWord( currentRequest - 1 );

            //If the word still isn't valid, we're done
            return WordExtentIsValid( currentRequest, word );
        }

        protected override bool IsTokenInSpan( HighlightWordTag tag, ITextSnapshot snapshot, SnapshotSpan span )
        {
            return true;
        }

        protected override void UpdateAtCaretPosition( CaretPosition caretPosition )
        {
            SnapshotPoint? prior = CurrentChar;
            base.UpdateAtCaretPosition( caretPosition );
            SnapshotPoint? current = CurrentChar;

            if ( !current.HasValue )
            {
                // reset with no selected words
                RequestedPoint = default( SnapshotPoint );
                UpdateSelectionState( default( SnapshotPoint ), new NormalizedSnapshotSpanCollection(), null );
                return;
            }

            if ( prior != current &&
                 !IsPointWithinCurrentWord( current ) )
            {
                // reset to current word, delay will pick up the rest.
                RequestedPoint = current.Value;
                UpdateSelectionState( current.Value, new NormalizedSnapshotSpanCollection(), null );
                return;
            }

            if ( IsPointWithinCurrentWord( current ) )
            {
                return;
            }

            RequestedPoint = current.Value;
            UpdateWordSelection();
            var span = new SnapshotSpan( Buffer.CurrentSnapshot, 0, Buffer.CurrentSnapshot.Length );
            OnTagsChanged( new SnapshotSpanEventArgs( span ) );
        }

        protected virtual bool IsPointWithinCurrentWord( SnapshotPoint? current )
        {
            return CurrentWord.HasValue &&
                   CurrentWord.Value.Snapshot == View.TextSnapshot &&
                   current.Value >= CurrentWord.Value.Start &&
                   current.Value <= CurrentWord.Value.End;
        }

        protected virtual void UpdateWordSelection()
        {
            SnapshotPoint currentRequest = RequestedPoint;
            var wordSpans = new List<SnapshotSpan>();

            TextExtent word = TextStructureNavigator.GetExtentOfWord( currentRequest );

            bool foundWord = GetSelectedWord( currentRequest, ref word );

            if ( !foundWord )
            {
                UpdateSelectionState( currentRequest, new NormalizedSnapshotSpanCollection(), null );
                return;
            }

            SnapshotSpan currentWord = word.Span;

            if ( CurrentWord.HasValue &&
                 currentWord == CurrentWord )
            {
                return;
            }

            IEnumerable<SnapshotSpan> matches = FindAllMatches( currentWord );
            wordSpans.AddRange( matches );

            UpdateSelectionState( currentRequest, new NormalizedSnapshotSpanCollection( wordSpans ), currentWord );
        }

        protected virtual bool WordExtentIsValid( SnapshotPoint currentRequest, TextExtent word )
        {
            return word.IsSignificant &&
                   currentRequest.Snapshot.GetText( word.Span )
                           .Any( c => char.IsLetter( c ) );
        }

        protected virtual void UpdateSelectionState( SnapshotPoint currentRequest,
                                                     NormalizedSnapshotSpanCollection newSpans,
                                                     SnapshotSpan? newCurrentWord )
        {
            lock ( _UpdateLock )
            {
                if ( currentRequest != RequestedPoint )
                {
                    return;
                }

                WordSpans = newSpans;
                CurrentWord = newCurrentWord;
                Parse();
            }
        }
    }
}