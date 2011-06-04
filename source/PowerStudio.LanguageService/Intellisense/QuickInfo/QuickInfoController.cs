#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

#endregion

namespace PowerStudio.LanguageService.Intellisense.QuickInfo
{
    internal class QuickInfoController : IIntellisenseController
    {
        private readonly QuickInfoControllerProvider _ComponentContext;
        private readonly IList<ITextBuffer> _SubjectBuffers;

        private IQuickInfoSession _Session;
        private ITextView _TextView;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "QuickInfoController" /> class.
        /// </summary>
        /// <param name = "textView">The text view.</param>
        /// <param name = "subjectBuffers">The subject buffers.</param>
        /// <param name = "componentContext">The component context.</param>
        internal QuickInfoController( ITextView textView,
                                      IList<ITextBuffer> subjectBuffers,
                                      QuickInfoControllerProvider componentContext )
        {
            _TextView = textView;
            _SubjectBuffers = subjectBuffers;
            _ComponentContext = componentContext;
            _TextView.MouseHover += OnTextViewMouseHover;
        }

        #region IIntellisenseController Members

        /// <summary>
        ///   Called when a new subject <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" /> appears in the graph of buffers associated with
        ///   the <see cref = "T:Microsoft.VisualStudio.Text.Editor.ITextView" />, due to a change in projection or content type.
        /// </summary>
        /// <param name = "subjectBuffer">The newly-connected text buffer.</param>
        public void ConnectSubjectBuffer( ITextBuffer subjectBuffer )
        {
        }

        /// <summary>
        ///   Called when a subject <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" /> is removed from the graph of buffers associated with
        ///   the <see cref = "T:Microsoft.VisualStudio.Text.Editor.ITextView" />, due to a change in projection or content type.
        /// </summary>
        /// <param name = "subjectBuffer">The disconnected text buffer.</param>
        /// <remarks>
        ///   It is not guaranteed that
        ///   the subject buffer was previously connected to this controller.
        /// </remarks>
        public void DisconnectSubjectBuffer( ITextBuffer subjectBuffer )
        {
        }

        /// <summary>
        ///   Detaches the controller from the specified <see cref = "T:Microsoft.VisualStudio.Text.Editor.ITextView" />.
        /// </summary>
        /// <param name = "textView">The <see cref = "T:Microsoft.VisualStudio.Text.Editor.ITextView" /> from which the controller should detach.</param>
        public void Detach( ITextView textView )
        {
            if ( _TextView == textView )
            {
                _TextView.MouseHover -= OnTextViewMouseHover;
                _TextView = null;
            }
        }

        #endregion

        /// <summary>
        ///   Called when [text view mouse hover].
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "Microsoft.VisualStudio.Text.Editor.MouseHoverEventArgs" /> instance containing the event data.</param>
        private void OnTextViewMouseHover( object sender, MouseHoverEventArgs e )
        {
            SnapshotPoint? point = GetMousePosition( new SnapshotPoint( _TextView.TextSnapshot, e.Position ) );

            if ( !point.HasValue )
            {
                return;
            }

            ITrackingPoint triggerPoint = point.Value.Snapshot.CreateTrackingPoint( point.Value.Position,
                                                                                    PointTrackingMode.Positive );

            // Find the broker for this buffer

            if ( !_ComponentContext.QuickInfoBroker.IsQuickInfoActive( _TextView ) )
            {
                _Session = _ComponentContext.QuickInfoBroker.CreateQuickInfoSession( _TextView, triggerPoint, true );
                _Session.Start();
            }
        }

        /// <summary>
        ///   Gets the mouse position.
        /// </summary>
        /// <param name = "topPosition">The top position.</param>
        /// <returns></returns>
        private SnapshotPoint? GetMousePosition( SnapshotPoint topPosition )
        {
            // Map this point down to the appropriate subject buffer.

            return _TextView.BufferGraph.MapDownToFirstMatch
                    (
                            topPosition,
                            PointTrackingMode.Positive,
                            snapshot => _SubjectBuffers.Contains( snapshot.TextBuffer ),
                            PositionAffinity.Predecessor
                    );
        }
    }
}