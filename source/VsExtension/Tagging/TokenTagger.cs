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
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class TokenTagger : TaggerBase<TokenTag>
    {
        public TokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
            ReParse();
        }

        private List<TokenTag> Tags { get; set; }

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
        public override IEnumerable<ITagSpan<TokenTag>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            if ( spans.Count == 0 ||
                 Buffer.CurrentSnapshot.Length == 0 )
            {
                //there is no content in the buffer
                yield break;
            }
            List<TokenTag> tags = Tags;
            foreach ( TokenTag tokenTag in tags )
            {
                yield return new TagSpan<TokenTag>( tokenTag.Span, tokenTag );
            }
        }

        protected override void ReParse()
        {
            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;

            IEnumerable<PSToken> tokens = GetTokens( newSnapshot, true );
            List<TokenTag> tags = ( from token in tokens
                                    let tokenSpan =
                                            new SnapshotSpan( newSnapshot, new Span( token.Start, token.Length ) )
                                    select new TokenTag { TokenType = token.Type, Span = tokenSpan } ).ToList();

            Snapshot = newSnapshot;
            Tags = tags;
            OnTagsChanged(
                    new SnapshotSpanEventArgs( new SnapshotSpan( newSnapshot, Span.FromBounds( 0, Snapshot.Length ) ) ) );
        }
    }
}