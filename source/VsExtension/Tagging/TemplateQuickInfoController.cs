using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace PowerStudio.VsExtension.Tagging
{
    internal class TemplateQuickInfoController : IIntellisenseController
    {
        private readonly TemplateQuickInfoControllerProvider _componentContext;
        private readonly IList<ITextBuffer> _subjectBuffers;

        private IQuickInfoSession _session;
        private ITextView _textView;


        internal TemplateQuickInfoController( ITextView textView,
                                              IList<ITextBuffer> subjectBuffers,
                                              TemplateQuickInfoControllerProvider componentContext )
        {
            _textView = textView;
            _subjectBuffers = subjectBuffers;
            _componentContext = componentContext;

            _textView.MouseHover += OnTextViewMouseHover;
        }

        #region IIntellisenseController Members

        public void ConnectSubjectBuffer( ITextBuffer subjectBuffer )
        {
        }

        public void DisconnectSubjectBuffer( ITextBuffer subjectBuffer )
        {
        }

        public void Detach( ITextView textView )
        {
            if ( _textView == textView )
            {
                _textView.MouseHover -= OnTextViewMouseHover;
                _textView = null;
            }
        }

        #endregion

        private void OnTextViewMouseHover( object sender, MouseHoverEventArgs e )
        {
            SnapshotPoint? point = GetMousePosition( new SnapshotPoint( _textView.TextSnapshot, e.Position ) );

            if ( point != null )
            {
                ITrackingPoint triggerPoint = point.Value.Snapshot.CreateTrackingPoint( point.Value.Position,
                                                                                        PointTrackingMode.Positive );

                // Find the broker for this buffer

                if ( !_componentContext.QuickInfoBroker.IsQuickInfoActive( _textView ) )
                {
                    _session = _componentContext.QuickInfoBroker.CreateQuickInfoSession( _textView, triggerPoint, true );
                    _session.Start();
                }
            }
        }


        private SnapshotPoint? GetMousePosition( SnapshotPoint topPosition )
        {
            // Map this point down to the appropriate subject buffer.

            return _textView.BufferGraph.MapDownToFirstMatch
                    (
                            topPosition,
                            PointTrackingMode.Positive,
                            snapshot => _subjectBuffers.Contains( snapshot.TextBuffer ),
                            PositionAffinity.Predecessor
                    );
        }
    }
}