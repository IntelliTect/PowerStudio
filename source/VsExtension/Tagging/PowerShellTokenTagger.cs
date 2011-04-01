using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using PowerStudio.Language;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellTokenTagger : ITagger<PowerShellTokenTag>
    {
        public PowerShellTokenTagger()
        {
            lexer = new Scanner();
        }

        protected Scanner lexer { get; set; }

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
                int start, end;
                lexer.SetSource( text, 0 );
                int state = 0;
                while (true)
                {
                    int result = lexer.GetNext( ref state, out start, out end );
                    Tokens resultToken = (Tokens) result;
                }
            }
            yield return new TagSpan<PowerShellTokenTag>( new SnapshotSpan(), new PowerShellTokenTag() );
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        #endregion
    }
}
