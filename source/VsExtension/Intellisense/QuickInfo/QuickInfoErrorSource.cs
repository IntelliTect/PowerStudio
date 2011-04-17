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
using PowerStudio.VsExtension.Tagging;

#endregion

namespace PowerStudio.VsExtension.Intellisense.QuickInfo
{
    public class QuickInfoErrorSource : QuickInfoSource<ErrorTokenTag>
    {
        public QuickInfoErrorSource( ITextBuffer buffer,
                                     ITagAggregator<ErrorTokenTag> aggregator,
                                     QuickInfoSourceProvider<ErrorTokenTag> quickInfoErrorSourceProvider )
                : base( buffer, aggregator, quickInfoErrorSourceProvider )
        {
        }

        protected override object GetToolTip( ErrorTokenTag tokenTag )
        {
            return tokenTag.ToolTipContent;
        }
    }
}