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
using Microsoft.VisualStudio.Text;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class HighlightWordTagger : TaggerBase<HighlightWordTag>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "HighlightWordTagger" /> class.
        /// </summary>
        /// <param name = "buffer">The buffer.</param>
        public HighlightWordTagger( ITextBuffer buffer )
                : base( buffer )
        {
        }

        #region Overrides of TaggerBase<HighlightWordTag>

        /// <summary>
        ///   Gets all the tags in the text snapshot.
        /// </summary>
        /// <param name = "snapshot">Text snapshot for the new buffer.</param>
        /// <returns>
        ///   A <see cref = "T:PowerStudio.VsExtension.Tagging.ITokenTag`1" /> for each tag.
        /// </returns>
        /// <remarks>
        ///   <para>
        ///     Taggers are not required to return their tags in any specific order.
        ///   </para>
        ///   <para>
        ///     The recommended way to implement this method by returning all tags in the document.
        ///     It is not recommended that you implement this method by using generators ("yield return"),
        ///     which allows lazy evaluation of the entire tagging stack; the lazy evaluation is provided
        ///     by the caller which will also evaluate whether the given tag is in a given span.
        ///   </para>
        /// </remarks>
        protected override List<HighlightWordTag> GetTags( ITextSnapshot snapshot )
        {
            return Enumerable.Empty<HighlightWordTag>().ToList();
        }

        #endregion
    }
}