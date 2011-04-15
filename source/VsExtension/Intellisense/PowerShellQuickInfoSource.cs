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
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using PowerStudio.VsExtension.Tagging;

#endregion

namespace PowerStudio.VsExtension.Intellisense
{
    public class PowerShellQuickInfoSource : IQuickInfoSource
    {
        private readonly ITagAggregator<ErrorTokenTag> _Aggregator;
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
                                          ITagAggregator<ErrorTokenTag> aggregator,
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
            if ( _Disposed )
            {
                throw new ObjectDisposedException( "PowerShellQuickInfoSource" );
            }

            ITextSnapshot currentSnapshot = _Buffer.CurrentSnapshot;
            SnapshotPoint? triggerPoint = session.GetTriggerPoint( currentSnapshot );

            if ( !triggerPoint.HasValue )
            {
                applicableToSpan = null;
                return;
            }

            var bufferSpan = new SnapshotSpan( currentSnapshot, 0, currentSnapshot.Length );
            foreach ( var tagSpan in _Aggregator.GetTags( bufferSpan ) )
            {
                foreach ( SnapshotSpan span in tagSpan.Span
                        .GetSpans( _Buffer )
                        .Where( span => span.Contains( triggerPoint.Value ) ) )
                {
                    applicableToSpan = currentSnapshot.CreateTrackingSpan( span, SpanTrackingMode.EdgeExclusive );
                    quickInfoContent.Add( tagSpan.Tag.ToolTipContent );
                    return;
                }
            }
            applicableToSpan = null;
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