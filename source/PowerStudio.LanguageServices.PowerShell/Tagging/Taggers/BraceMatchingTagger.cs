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
using PowerStudio.LanguageServices.Tagging;
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
{
    public class BraceMatchingTagger : ViewTaggerBase<BraceMatchingTag<PSToken>, PSToken>
    {
        public BraceMatchingTagger( ITextView view, ITextBuffer buffer )
                : base( view, buffer )
        {
            Parse();
        }

        /// <summary>
        ///   Determines whether given token is in the target span translating to the current snaphot
        ///   if needed.
        /// </summary>
        /// <param name = "tag">The tag.</param>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "span">The span.</param>
        /// <returns>
        ///   <c>true</c> if the tag is in the span for the snapshot; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsTokenInSpan( BraceMatchingTag<PSToken> tag, ITextSnapshot snapshot, SnapshotSpan span )
        {
            if ( !CurrentChar.HasValue )
            {
                return false;
            }

            SnapshotPoint currentChar = CurrentChar.Value;

            if ( IsSnapshotPointContainedInSpan( snapshot, currentChar, tag.Span ) ||
                 IsSnapshotPointContainedInSpan( snapshot, currentChar, tag.Match.Span ) )
            {
                return base.IsTokenInSpan( tag, snapshot, span ) ||
                       base.IsTokenInSpan( tag.Match, snapshot, span );
            }
            return false;
        }

        protected override List<BraceMatchingTag<PSToken>> GetTags( ITextSnapshot snapshot )
        {
            var braces = new List<BraceMatchingTag<PSToken>>();
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
                        var start = new BraceMatchingTag<PSToken>( PredefinedTextMarkerTags.BraceHighlight )
                                    {
                                            Span = CreateSnapshotSpan( snapshot, startToken ),
                                    };

                        var end = new BraceMatchingTag<PSToken>( PredefinedTextMarkerTags.BraceHighlight )
                                  {
                                          Span = CreateSnapshotSpan( snapshot, token ),
                                          Match = start
                                  };
                        start.Match = end;
                        braces.Add( start );
                        // TODO: Can we eliminate adding the end as tag? We look for both via the Match property.
                        braces.Add( end );
                    }
                        break;
                    default:
                        break;
                }
            }
            return braces;
        }

        protected override void UpdateAtCaretPosition( CaretPosition caretPosition )
        {
            base.UpdateAtCaretPosition( caretPosition );

            if ( !CurrentChar.HasValue )
            {
                return;
            }

            var span = new SnapshotSpan( Buffer.CurrentSnapshot, 0, Buffer.CurrentSnapshot.Length );
            OnTagsChanged( new SnapshotSpanEventArgs( span ) );
        }
    }
}