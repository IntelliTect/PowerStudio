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
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class BraceMatchingTagger : TaggerBase<BraceMatchingTag>
    {
        public BraceMatchingTagger( ITextView view, ITextBuffer buffer )
                : base( buffer )
        {
            View = view;
            View.Caret.PositionChanged += CaretPositionChanged;
            View.LayoutChanged += ViewLayoutChanged;
            Parse();
        }

        private ITextView View { get; set; }
        private SnapshotPoint? CurrentChar { get; set; }

        protected override bool IsTokenInSpan( BraceMatchingTag tag, ITextSnapshot snapshot, SnapshotSpan span )
        {
            if ( !CurrentChar.HasValue )
            {
                return false;
            }

            SnapshotPoint currentChar = CurrentChar.Value;
            if ( snapshot != currentChar.Snapshot )
            {
                currentChar = currentChar.TranslateTo( snapshot, PointTrackingMode.Positive );
            }

            if ( tag.Span.Contains( currentChar ) ||
                 tag.Match.Span.Contains( currentChar ) )
            {
                return base.IsTokenInSpan( tag, snapshot, span ) ||
                       base.IsTokenInSpan( tag.Match, snapshot, span );
            }
            return false;
        }

        protected override List<BraceMatchingTag> GetTags( ITextSnapshot snapshot )
        {
            var braces = new List<BraceMatchingTag>();
            var stack = new Stack<PSToken>();
            IEnumerable<PSToken> tokens = GetTokens( snapshot, true );
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
                        var start = new BraceMatchingTag( PredefinedTextMarkerTags.BraceHighlight )
                                    {
                                            Span = AsSnapshotSpan( snapshot, startToken ),
                                    };

                        var end = new BraceMatchingTag( PredefinedTextMarkerTags.BraceHighlight )
                                  {
                                          Span = AsSnapshotSpan( snapshot, token ),
                                          Match = start
                                  };
                        start.Match = end;
                        braces.Add( start );
                        braces.Add( end );
                    }
                        break;
                    default:
                        break;
                }
            }
            return braces;
        }

        private void ViewLayoutChanged( object sender, TextViewLayoutChangedEventArgs e )
        {
            if ( e.NewSnapshot !=
                 e.OldSnapshot ) //make sure that there has really been a change
            {
                UpdateAtCaretPosition( View.Caret.Position );
            }
        }

        private void CaretPositionChanged( object sender, CaretPositionChangedEventArgs e )
        {
            UpdateAtCaretPosition( e.NewPosition );
        }

        private void UpdateAtCaretPosition( CaretPosition caretPosition )
        {
            CurrentChar = caretPosition.Point.GetPoint( Buffer, caretPosition.Affinity );

            if ( !CurrentChar.HasValue )
            {
                return;
            }

            var span = new SnapshotSpan( Buffer.CurrentSnapshot, 0, Buffer.CurrentSnapshot.Length );
            OnTagsChanged( new SnapshotSpanEventArgs( span ) );
        }
    }
}