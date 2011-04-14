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
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class OutliningTagger : TaggerBase<IOutliningRegionTag>
    {
        private string ellipsis = "..."; //the characters that are displayed when the region is collapsed
        private string endHide = "}"; //the characters that end the outlining region
        //string hoverText = "hover text"; //the contents of the tooltip for the collapsed span
        private List<Region> regions;
        private string startHide = "{"; //the characters that start the outlining region

        public OutliningTagger( ITextBuffer buffer )
                : base( buffer )
        {
            regions = new List<Region>();
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
            List<Region> currentRegions = regions;
            ITextSnapshot currentSnapshot = Snapshot;
            SnapshotSpan entire =
                    new SnapshotSpan( spans[0].Start, spans[spans.Count - 1].End ).TranslateTo( currentSnapshot,
                                                                                                SpanTrackingMode.
                                                                                                        EdgeExclusive );
            int startLineNumber = entire.Start.GetContainingLine().LineNumber;
            int endLineNumber = entire.End.GetContainingLine().LineNumber;
            foreach ( Region region in currentRegions )
            {
                if ( region.StartLine <= endLineNumber &&
                     region.EndLine >= startLineNumber )
                {
                    ITextSnapshotLine startLine = currentSnapshot.GetLineFromLineNumber( region.StartLine );
                    ITextSnapshotLine endLine = currentSnapshot.GetLineFromLineNumber( region.EndLine );

                    //the region starts at the beginning of the "[", and goes until the *end* of the line that contains the "]".
                    yield return new TagSpan<IOutliningRegionTag>(
                            new SnapshotSpan( startLine.Start + region.StartOffset,
                                              endLine.End ),
                            new OutliningRegionTag( false, false, ellipsis, startLine.GetText() ) );
                }
            }
        }

        #endregion

        protected override void ReParse()
        {
            // TODO: Rewrite this with a stack.

            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;
            var newRegions = new List<Region>();

            //keep the current (deepest) partial region, which will have
            // references to any parent partial regions.
            PartialRegion currentRegion = null;

            foreach ( ITextSnapshotLine line in newSnapshot.Lines )
            {
                //int regionStart = -1;
                int startRegionStart = 0;
                int endRegionStart = 0;
                string text = line.GetText();

                //lines that contain a "[" denote the start of a new region.
                while ( startRegionStart != -1 &&
                        startRegionStart < text.Length )
                {
                    if (
                            ( endRegionStart =
                              text.IndexOf( endHide,
                                            startRegionStart == -1 ? 0 : startRegionStart,
                                            StringComparison.Ordinal ) ) !=
                            -1 )
                    {
                        int currentLevel = ( currentRegion != null ) ? currentRegion.Level : 1;
                        int closingLevel = currentLevel;

                        //the regions match
                        if ( currentRegion != null &&
                             currentLevel == closingLevel )
                        {
                            newRegions.Add( new Region
                                            {
                                                    Level = currentLevel,
                                                    StartLine = currentRegion.StartLine,
                                                    StartOffset = currentRegion.StartOffset,
                                                    EndLine = line.LineNumber
                                            } );

                            currentRegion = currentRegion.PartialParent;
                        }
                    }

                    if ( ( startRegionStart = text.IndexOf( startHide, startRegionStart, StringComparison.Ordinal ) ) !=
                         -1 )
                    {
                        int currentLevel = ( currentRegion != null ) ? currentRegion.Level : 1;
                        int newLevel = currentLevel + 1;

                        //levels are the same and we have an existing region;
                        //end the current region and start the next
                        if ( currentLevel == newLevel &&
                             currentRegion != null )
                        {
                            newRegions.Add( new Region
                                            {
                                                    Level = currentRegion.Level,
                                                    StartLine = currentRegion.StartLine,
                                                    StartOffset = currentRegion.StartOffset,
                                                    EndLine = line.LineNumber
                                            } );

                            currentRegion = new PartialRegion
                                            {
                                                    Level = newLevel,
                                                    StartLine = line.LineNumber,
                                                    StartOffset = startRegionStart,
                                                    PartialParent = currentRegion.PartialParent
                                            };
                        }
                                //this is a new (sub)region
                        else
                        {
                            currentRegion = new PartialRegion
                                            {
                                                    Level = newLevel,
                                                    StartLine = line.LineNumber,
                                                    StartOffset = startRegionStart,
                                                    PartialParent = currentRegion
                                            };
                        }
                    }
                    if ( startRegionStart != -1 )
                    {
                        startRegionStart++;
                    }
                }
            }
            //determine the changed span, and send a changed event with the new spans
            var oldSpans =
                    new List<Span>( regions.Select( r => AsSnapshotSpan( r, Snapshot )
                                                                 .TranslateTo( newSnapshot,
                                                                               SpanTrackingMode.EdgeExclusive )
                                                                 .Span ) );
            var newSpans =
                    new List<Span>( newRegions.Select( r => AsSnapshotSpan( r, newSnapshot ).Span ) );

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
            regions = newRegions;

            if ( changeStart <= changeEnd )
            {
                OnTagsChanged(
                        new SnapshotSpanEventArgs( new SnapshotSpan( Snapshot, Span.FromBounds( changeStart, changeEnd ) ) ) );
            }
        }

        private static SnapshotSpan AsSnapshotSpan( Region region, ITextSnapshot snapshot )
        {
            ITextSnapshotLine startLine = snapshot.GetLineFromLineNumber( region.StartLine );
            ITextSnapshotLine endLine = ( region.StartLine == region.EndLine )
                                                ? startLine
                                                : snapshot.GetLineFromLineNumber( region.EndLine );
            return new SnapshotSpan( startLine.Start + region.StartOffset, endLine.End );
        }

        #region Nested type: PartialRegion

        private class PartialRegion
        {
            public int StartLine { get; set; }
            public int StartOffset { get; set; }
            public int Level { get; set; }
            public PartialRegion PartialParent { get; set; }
        }

        #endregion

        #region Nested type: Region

        private class Region : PartialRegion
        {
            public int EndLine { get; set; }
        }

        #endregion
    }
}