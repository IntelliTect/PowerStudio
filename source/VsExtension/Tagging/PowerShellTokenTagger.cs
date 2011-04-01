﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellTokenTagger : ITagger<PowerShellTokenTag>
    {
        #region Implementation of ITagger<out PowerShellTokenTag>

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
            foreach (SnapshotSpan currentSpan in spans)
            {
                string text = currentSpan.GetText();
                Collection<PSParseError> errors;
                Collection<PSToken> tokens = PSParser.Tokenize( text, out errors );
                foreach ( var token in tokens )
                {
                    var tokenSpan = new SnapshotSpan(currentSpan.Snapshot, new Span(token.StartColumn, token.Length));
                    yield return new TagSpan<PowerShellTokenTag>( tokenSpan, new PowerShellTokenTag() {TokenType = token.Type} );
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        #endregion
    }
}
