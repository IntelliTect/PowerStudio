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
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class ErrorTokenTagger : TaggerBase<ErrorTokenTag>
    {
        public ErrorTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
            ReParse();
        }

        protected override void ReParse()
        {
            ITextSnapshot newSnapshot = Buffer.CurrentSnapshot;

            int curLoc = 0;
            string text = newSnapshot.GetText();
            Collection<PSParseError> errors;
            Collection<PSToken> tokens = PSParser.Tokenize( text, out errors );
            List<ErrorTokenTag> tags = ( from error in errors
                                         let tokenSpan =
                                                 new SnapshotSpan( Snapshot,
                                                                   new Span( error.Token.Start + curLoc,
                                                                             error.Token.Length ) )
                                         select
                                                 new ErrorTokenTag( PredefinedErrorTypeNames.SyntaxError, error.Message )
                                                 { TokenType = error.Token.Type, Span = tokenSpan } ).ToList();

            Snapshot = newSnapshot;
            Tags = tags.AsReadOnly();
            OnTagsChanged( new SnapshotSpanEventArgs( new SnapshotSpan( Snapshot, Span.FromBounds( 0, curLoc ) ) ) );
        }
    }
}