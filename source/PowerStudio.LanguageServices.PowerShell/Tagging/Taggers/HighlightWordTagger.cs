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
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
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
                    .Throttle( WordHighlightDelay )
                    .Select( eventPattern => (CaretPositionChangedEventArgs) eventPattern.EventArgs )
                    .Subscribe( eventArgs => UpdateAtCaretPosition( eventArgs.NewPosition ) );
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
            findData.FindOptions = FindOptions.WholeWord; // Not case sensitive as PowerShell isn't.
            return TextSearchService.FindAll( findData );
        }

        protected virtual bool TryGetSelectedWord( SnapshotPoint currentRequest, ref TextExtent word )
        {
            if ( WordExtentIsValid( currentRequest, word ) )
            {
                return true;
            }

            if ( word.Span.Start != currentRequest ||
                 currentRequest == currentRequest.GetContainingLine().Start ||
                 char.IsWhiteSpace( ( currentRequest - 1 ).GetChar() ) )
            {
                return false;
            }

            // If the caret is at the end of a word, pick up the word.
            word = TextStructureNavigator.GetExtentOfWord( currentRequest - 1 );

            return WordExtentIsValid( currentRequest, word );
        }

        protected override bool IsTokenInSpan( HighlightWordTag tag, ITextSnapshot snapshot, SnapshotSpan span )
        {
            // our tokens are always available as we have the spans cached
            // and the span does not cover all matches.
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

            bool isPointWithinCurrentWord = IsPointWithinCurrentWord( current );
            if ( prior != current &&
                 !isPointWithinCurrentWord )
            {
                // reset to current word, delay will pick up the rest.
                RequestedPoint = current.Value;
                UpdateSelectionState( current.Value, new NormalizedSnapshotSpanCollection(), null );
                return;
            }

            if ( isPointWithinCurrentWord )
            {
                return;
            }

            RequestedPoint = current.Value;
            UpdateWordSelection();
            var span = new SnapshotSpan( Buffer.CurrentSnapshot, 0, Buffer.CurrentSnapshot.Length );
            OnTagsChanged( new SnapshotSpanEventArgs( span ) );
        }

        protected virtual bool IsPointWithinCurrentWord( SnapshotPoint? point )
        {
            return CurrentWord.HasValue &&
                   point.HasValue &&
                   CurrentWord.Value.Snapshot == View.TextSnapshot &&
                   point.Value >= CurrentWord.Value.Start &&
                   point.Value <= CurrentWord.Value.End;
        }

        protected virtual void UpdateWordSelection()
        {
            SnapshotPoint currentRequest = RequestedPoint;
            var wordSpans = new List<SnapshotSpan>();

            TextExtent selectedWord = TextStructureNavigator.GetExtentOfWord( currentRequest );

            bool success = TryGetSelectedWord( currentRequest, ref selectedWord );

            if ( !success )
            {
                UpdateSelectionState( currentRequest, new NormalizedSnapshotSpanCollection(), null );
                return;
            }

            SnapshotSpan selectedWordSpan = selectedWord.Span;

            if ( CurrentWord.HasValue &&
                 CurrentWord == selectedWordSpan )
            {
                return;
            }

            IEnumerable<SnapshotSpan> matches = FindAllMatches( selectedWordSpan );
            wordSpans.AddRange( matches );

            UpdateSelectionState( currentRequest, new NormalizedSnapshotSpanCollection( wordSpans ), selectedWordSpan );
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