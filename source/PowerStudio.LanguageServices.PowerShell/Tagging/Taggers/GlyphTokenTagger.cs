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
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
{
    public abstract class GlyphTokenTagger<TTokenTag,TToken> : TaggerBase<TTokenTag,TToken>
            where TTokenTag : GlyphTag<TToken>
    {
        protected GlyphTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
        }
    }
}