#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Text;
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Tagging.Taggers
{
    public abstract class GlyphTokenTagger<TTokenTag> : TaggerBase<TTokenTag>
            where TTokenTag : GlyphTag
    {
        protected GlyphTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
        }
    }
}