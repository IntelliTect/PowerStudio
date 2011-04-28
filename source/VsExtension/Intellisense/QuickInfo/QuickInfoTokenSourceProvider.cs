#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#if DEBUG

#region Using Directives

using System.ComponentModel.Composition;
using System.Text;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Tagging;

#endregion

namespace PowerStudio.VsExtension.Intellisense.QuickInfo
{
    [Export( typeof (IQuickInfoSourceProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [Name( LanguageConfiguration.Name + "TokenTag QuickInfo" )]
    public class QuickInfoTokenSourceProvider : QuickInfoSourceProvider<TokenClassificationTag>
    {
        /// <summary>
        ///   Creates a Quick Info provider for the specified context.
        /// </summary>
        /// <param name = "textBuffer">The text buffer for which to create a provider.</param>
        /// <returns>
        ///   A valid <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSource" /> instance, or null if none could be created.
        /// </returns>
        public override IQuickInfoSource TryCreateQuickInfoSource( ITextBuffer textBuffer )
        {
            var tagAggregator = TagAggregatorFactory.CreateTagAggregator<TokenClassificationTag>( textBuffer );
            return new QuickInfoTokenSource( textBuffer, tagAggregator, this );
        }

        #region Nested type: QuickInfoTokenSource

        private class QuickInfoTokenSource : QuickInfoSource<TokenClassificationTag>
        {
            public QuickInfoTokenSource( ITextBuffer buffer,
                                         ITagAggregator<TokenClassificationTag> aggregator,
                                         QuickInfoSourceProvider<TokenClassificationTag> quickInfoErrorSourceProvider )
                    : base( buffer, aggregator, quickInfoErrorSourceProvider )
            {
            }

            protected override object GetToolTip( TokenClassificationTag tokenTag )
            {
                var token = tokenTag.Token;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat( "Content: {0}", token.Content );
                sb.AppendLine();
                sb.AppendFormat("Length: {0}", token.Length);
                sb.AppendLine();
                sb.AppendFormat("Type: {0}", token.Type);
                sb.AppendLine();
                sb.AppendFormat("StartLine: {0}", token.StartLine);
                sb.AppendLine();
                sb.AppendFormat("EndLine: {0}", token.EndLine);
                sb.AppendLine();
                sb.AppendFormat("Start: {0}", token.Start);
                sb.AppendLine();
                sb.AppendFormat("StartLine: {0}", token.StartLine);
                sb.AppendLine();
                sb.AppendFormat("StartColumn: {0}", token.StartColumn);
                sb.AppendLine();
                sb.AppendFormat("EndLine: {0}", token.EndLine);
                sb.AppendLine();
                sb.AppendFormat("EndColumn: {0}", token.EndColumn);

                return sb.ToString();
            }
        }

        #endregion
    }
}

#endif