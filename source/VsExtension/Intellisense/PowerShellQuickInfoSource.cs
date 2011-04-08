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
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Intellisense
{
    public class PowerShellQuickInfoSource : IQuickInfoSource
    {
        private readonly ITagAggregator<ErrorTag> _Aggregator;
        private readonly ITextBuffer _Buffer;
        private readonly PowerShellQuickInfoSourceProvider _QuickInfoSourceProvider;
        private bool _Disposed;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "PowerShellQuickInfoSource" /> class.
        /// </summary>
        /// <param name = "buffer">The buffer.</param>
        /// <param name = "aggregator">The aggregator.</param>
        /// <param name = "quickInfoSourceProvider">The quick info source provider.</param>
        public PowerShellQuickInfoSource( ITextBuffer buffer,
                                          ITagAggregator<ErrorTag> aggregator,
                                          PowerShellQuickInfoSourceProvider quickInfoSourceProvider )
        {
            _Aggregator = aggregator;
            _QuickInfoSourceProvider = quickInfoSourceProvider;
            _Buffer = buffer;
        }

        #region IQuickInfoSource Members

        /// <summary>
        ///   Determines which pieces of QuickInfo content should be part of the specified <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSession" />.
        /// </summary>
        /// <param name = "session">The session for which completions are to be computed.</param>
        /// <param name = "quickInfoContent">The QuickInfo content to be added to the session.</param>
        /// <param name = "applicableToSpan">The <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" /> to which this session applies.</param>
        /// <remarks>
        ///   Each applicable <see cref = "M:Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSource.AugmentQuickInfoSession(Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSession,System.Collections.Generic.IList{System.Object},Microsoft.VisualStudio.Text.ITrackingSpan@)" /> instance will be called in-order to (re)calculate a
        ///   <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSession" />. Objects can be added to the session by adding them to the quickInfoContent collection
        ///   passed-in as a parameter.  In addition, by removing items from the collection, a source may filter content provided by
        ///   <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSource" />s earlier in the calculation chain.
        /// </remarks>
        public void AugmentQuickInfoSession( IQuickInfoSession session,
                                             IList<object> quickInfoContent,
                                             out ITrackingSpan applicableToSpan )
        {
            try
            {
                applicableToSpan = null;

                if (_Disposed)
                {
                    throw new ObjectDisposedException("TestQuickInfoSource");
                }

                var triggerPoint = (SnapshotPoint)session.GetTriggerPoint(_Buffer.CurrentSnapshot);

                if (triggerPoint == default(SnapshotPoint))
                {
                    return;
                }

                foreach (
                        IMappingTagSpan<ErrorTag> tagSpan in
                                _Aggregator.GetTags(new SnapshotSpan(_Buffer.CurrentSnapshot,
                                                                       0,
                                                                       _Buffer.CurrentSnapshot.Length)))
                {
                    foreach (SnapshotSpan span in tagSpan.Span.GetSpans(_Buffer))
                    {
                        if (span.Contains(triggerPoint))
                        {
                            applicableToSpan = _Buffer.CurrentSnapshot.CreateTrackingSpan(span,
                                                                                           SpanTrackingMode.
                                                                                                   EdgeExclusive);
                            quickInfoContent.Add(tagSpan.Tag.ToolTipContent);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                applicableToSpan = null;
            }
        }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _Disposed = true;
        }

        #endregion
    }
}