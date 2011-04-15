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
using System.Collections.ObjectModel;
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

        protected override void ReParse()
        {
            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;

            IEnumerable<PSToken> tokens = GetTokens( newSnapshot, true );
            List<TokenTag> tags = ( from token in tokens
                                    let tokenSpan =
                                            new SnapshotSpan( newSnapshot, new Span( token.Start, token.Length ) )
                                    select new TokenTag { TokenType = token.Type, Span = tokenSpan } ).ToList();

            Snapshot = newSnapshot;
            Tags = tags.AsReadOnly();
            OnTagsChanged(
                    new SnapshotSpanEventArgs( new SnapshotSpan( newSnapshot, Span.FromBounds( 0, Snapshot.Length ) ) ) );
        }
    }
}