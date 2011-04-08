using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;

namespace PowerStudio.VsExtension.Tagging
{
    public class PsQuickInfoSource : IQuickInfoSource
    {
        private readonly PsQuickInfoSourceProvider _QuickInfoSourceProvider;
        private readonly ITagAggregator<ErrorTag> _aggregator;
        private readonly ITextBuffer _buffer;
        private bool _disposed;


        public PsQuickInfoSource( ITextBuffer buffer,
                                  ITagAggregator<ErrorTag> aggregator,
                                  PsQuickInfoSourceProvider quickInfoSourceProvider )
        {
            _aggregator = aggregator;
            _QuickInfoSourceProvider = quickInfoSourceProvider;
            _buffer = buffer;
        }

        #region IQuickInfoSource Members

        public void AugmentQuickInfoSession( IQuickInfoSession session,
                                             IList<object> quickInfoContent,
                                             out ITrackingSpan applicableToSpan )
        {
            applicableToSpan = null;

            if ( _disposed )
            {
                throw new ObjectDisposedException( "TestQuickInfoSource" );
            }

            var triggerPoint = (SnapshotPoint)session.GetTriggerPoint(_buffer.CurrentSnapshot);

            if (triggerPoint == null)
                return;
            IMappingTagSpan<ErrorTag> mappingTagSpan = null;
            //var spann = applicableToSpan.GetSpan(session.TextView.TextBuffer.CurrentSnapshot);
            foreach (
                    var tagSpan in
                            _aggregator.GetTags( new SnapshotSpan( _buffer.CurrentSnapshot,
                                                                   0,
                                                                   _buffer.CurrentSnapshot.Length ) ) )
            {
                foreach (SnapshotSpan span in tagSpan.Span.GetSpans(_buffer))
                {
                    if ( span.Contains( triggerPoint ) )
                    {

                        applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan( span,
                                                                                       SpanTrackingMode.EdgeExclusive );
                        quickInfoContent.Add( tagSpan.Tag.ToolTipContent );
                        return;
                    }
                }

                //SnapshotSpan span = tagSpan.Span.GetSpans( _buffer ).First();
                //if (span.Contains(triggerPoint))
                //{

                //    applicableToSpan = _buffer.CurrentSnapshot.CreateTrackingSpan( span, SpanTrackingMode.EdgeExclusive );
                //    quickInfoContent.Add( tagSpan.Tag.ToolTipContent );
                //    return;
                //}
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }

        #endregion

        private TextExtent FindExtentAtPoint( SnapshotPoint? subjectTriggerPoint )
        {
            ITextStructureNavigator navigator =
                    _QuickInfoSourceProvider.NavigatorService.GetTextStructureNavigator( _buffer );
            TextExtent extent = navigator.GetExtentOfWord( subjectTriggerPoint.Value );
            return extent;
        }
    }
}