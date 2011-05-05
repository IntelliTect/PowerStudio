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
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Tagging.Taggers
{
    public class HighlightWordTagger : TaggerBase<HighlightWordTag>
    {
        public HighlightWordTagger( ITextBuffer buffer )
                : base( buffer )
        {
        }

        #region Overrides of TaggerBase<HighlightWordTag>

        protected override List<HighlightWordTag> GetTags( ITextSnapshot snapshot )
        {
            return Enumerable.Empty<HighlightWordTag>().ToList();
        }

        #endregion
    }
}