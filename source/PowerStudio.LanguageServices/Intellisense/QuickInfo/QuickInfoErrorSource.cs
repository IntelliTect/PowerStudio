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
using Microsoft.VisualStudio.Text.Tagging;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Intellisense.QuickInfo
{
    public class QuickInfoErrorSource<TToken> : QuickInfoSource<ErrorTokenTag<TToken>, TToken>
    {
        public QuickInfoErrorSource( ITextBuffer buffer,
                                     ITagAggregator<ErrorTokenTag<TToken>> aggregator,
                                     QuickInfoSourceProvider<ErrorTokenTag<TToken>, TToken> quickInfoErrorSourceProvider )
                : base( buffer, aggregator, quickInfoErrorSourceProvider )
        {
        }

        protected override object GetToolTip( ErrorTokenTag<TToken> tokenTag )
        {
            return tokenTag.ToolTipContent;
        }
    }
}