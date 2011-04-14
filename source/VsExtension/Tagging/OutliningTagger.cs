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
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class OutliningTagger : TaggerBase<IOutliningRegionTag>
    {
        private List<Region> _Regions;

        public OutliningTagger( ITextBuffer buffer )
                : base( buffer )
        {
            _Regions = new List<Region>();
            ReParse();
        }

        #region Implementation of ITagger<out IOutliningRegionTag>

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
        public override IEnumerable<ITagSpan<IOutliningRegionTag>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            if ( spans.Count == 0 )
            {
                yield break;
            }
            List<Region> currentRegions = _Regions;
            ITextSnapshot currentSnapshot = Snapshot;
            SnapshotSpan entire =
                    new SnapshotSpan( spans[0].Start, spans[spans.Count - 1].End )
                            .TranslateTo( currentSnapshot, SpanTrackingMode.EdgeExclusive );
            int startLineNumber = entire.Start.GetContainingLine().LineNumber;
            int endLineNumber = entire.End.GetContainingLine().LineNumber;
            foreach ( SnapshotSpan span in from region in currentRegions
                                           where region.StartLine <= endLineNumber && region.EndLine >= startLineNumber
                                           select region.Span )
            {
                var tag = new OutliningTag( currentSnapshot, span, false );
                yield return new TagSpan<IOutliningRegionTag>( span, tag );
            }
        }

        #endregion

        protected override void ReParse()
        {
            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;
            List<Region> newRegions = GetNewRegions( newSnapshot );

            //determine the changed span, and send a changed event with the new spans
            var oldSpans =
                    new List<Span>( _Regions.Select( region => region.Span.TranslateTo( newSnapshot,
                                                                              SpanTrackingMode.EdgeExclusive )
                                                                  .Span ) );
            var newSpans =
                    new List<Span>( newRegions.Select( region => region.Span.Span ) );

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
            _Regions = newRegions;

            if ( changeStart <= changeEnd )
            {
                OnTagsChanged(
                        new SnapshotSpanEventArgs( new SnapshotSpan( newSnapshot,
                                                                     Span.FromBounds( changeStart, changeEnd ) ) ) );
            }
        }

        private List<Region> GetNewRegions( ITextSnapshot newSnapshot )
        {
            const int lineThreshold = 2;
            var regions = new List<Region>();
            var stack = new Stack<PSToken>();
            IEnumerable<PSToken> tokens = GetTokens( newSnapshot, true );
            foreach ( PSToken token in tokens )
            {
                switch ( token.Type )
                {
                    case PSTokenType.GroupStart:
                    {
                        stack.Push( token );
                        break;
                    }
                    case PSTokenType.GroupEnd:
                    {
                        if ( stack.Count == 0 )
                        {
                            continue;
                        }
                        PSToken startToken = stack.Pop();
                        if ( token.EndLine - startToken.StartLine < lineThreshold )
                        {
                            continue;
                        }
                        regions.Add( new Region
                                     {
                                             StartLine = startToken.StartLine,
                                             EndLine = token.StartLine,
                                             Span = AsSnapshotSpan( newSnapshot, startToken, token )
                                     } );
                    }
                        break;
                    default:
                        break;
                }
            }
            return regions;
        }

        private static SnapshotSpan AsSnapshotSpan( ITextSnapshot snapshot, PSToken startToken, PSToken endToken )
        {
            var startSnapshot = new SnapshotSpan( snapshot, new Span( startToken.Start, startToken.Length ) );
            var endSnapshot = new SnapshotSpan( snapshot, new Span( endToken.Start, endToken.Length ) );
            return new SnapshotSpan( startSnapshot.Start, endSnapshot.End );
        }

        #region Nested type: Region

        private class Region
        {
            public int EndLine { get; set; }
            public SnapshotSpan Span { get; set; }
            public int StartLine { get; set; }
        }

        #endregion
    }
}