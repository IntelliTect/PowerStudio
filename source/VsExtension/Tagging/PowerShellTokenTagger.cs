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
    public class PowerShellTokenTagger : PowerShellTagger<PowerShellTokenTag>
    {
        public PowerShellTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
        }

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
        public override IEnumerable<ITagSpan<PowerShellTokenTag>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            if ( spans.Count == 0 ||
                 Buffer.CurrentSnapshot.Length == 0 )
            {
                //there is no content in the buffer
                yield break;
            }
            foreach ( SnapshotSpan currentSpan in spans )
            {
                int curLoc = currentSpan.Start.Position;
                string text = currentSpan.GetText();
                Collection<PSParseError> errors;
                Collection<PSToken> tokens = PSParser.Tokenize( text, out errors );
                foreach ( PSToken token in tokens.Union( errors.Select( error => error.Token ) ) )
                {
                    var tokenSpan = new SnapshotSpan( currentSpan.Snapshot,
                                                      new Span( token.Start + curLoc, token.Length ) );
                    var tokenTag = new PowerShellTokenTag { TokenType = token.Type };
                    yield return new TagSpan<PowerShellTokenTag>( tokenSpan, tokenTag );
                }
            }
        }
    }
}