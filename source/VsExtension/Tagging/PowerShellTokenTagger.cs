#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellTokenTagger : ITagger<PowerShellTokenTag>, ITagger<ErrorTag>
    {
        private readonly ITextBuffer _Buffer;

        public PowerShellTokenTagger( ITextBuffer buffer )
        {
            _Buffer = buffer;
        }

        #region Implementation of ITagger<out PowerShellTokenTag>

        #region ITagger<ErrorTag> Members

        IEnumerable<ITagSpan<ErrorTag>> ITagger<ErrorTag>.GetTags( NormalizedSnapshotSpanCollection spans )
        {
            if ( spans.Count == 0 ||
                 _Buffer.CurrentSnapshot.Length == 0 )
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

                foreach ( PSParseError error in errors )
                {
                    if ( error.Token.Type ==
                         PSTokenType.Position )
                    {
                        continue;
                    }
                    var tokenSpan = new SnapshotSpan( currentSpan.Snapshot,
                                                      new Span( error.Token.Start + curLoc, error.Token.Length ) );
                    var errorTag = new ErrorTag( PredefinedErrorTypeNames.SyntaxError, error.Message );
                    yield return new TagSpan<ErrorTag>( tokenSpan, errorTag );
                }
            }
        }

        #endregion

        #region ITagger<PowerShellTokenTag> Members

        /// <summary>
        /// Gets all the tags that overlap the <paramref name="spans"/>.
        /// </summary>
        /// <param name="spans">The spans to visit.</param>
        /// <returns>
        /// A <see cref="T:Microsoft.VisualStudio.Text.Tagging.ITagSpan`1"/> for each tag.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Taggers are not required to return their tags in any specific order.
        /// </para>
        /// <para>
        /// The recommended way to implement this method is by using generators ("yield return"),
        ///             which allows lazy evaluation of the entire tagging stack.
        /// </para>
        /// </remarks>
        public virtual IEnumerable<ITagSpan<PowerShellTokenTag>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            if ( spans.Count == 0 ||
                 _Buffer.CurrentSnapshot.Length == 0 )
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
                foreach ( PSToken token in tokens )
                {
                    var tokenSpan = new SnapshotSpan( currentSpan.Snapshot,
                                                      new Span( token.Start + curLoc, token.Length ) );
                    var tokenTag = new PowerShellTokenTag { TokenType = token.Type };
                    yield return new TagSpan<PowerShellTokenTag>( tokenSpan, tokenTag );
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        #endregion

        #endregion
    }
}