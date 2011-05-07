#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.ComponentModel.Composition;
using System.Windows.Media.TextFormatting;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Utilities;

#endregion

namespace PowerStudio.VsExtension.Formatting
{
    [Export( typeof (ITextParagraphPropertiesFactoryService) )]
    [ContentType( LanguageConfiguration.Name )]
    public class ParagraphPropertiesFactoryService : ITextParagraphPropertiesFactoryService
    {
        /// <summary>
        ///   Creates a <see cref = "T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> for the provided configuration.
        /// </summary>
        /// <param name = "formattedLineSource">The <see cref = "T:Microsoft.VisualStudio.Text.Formatting.IFormattedLineSource" /> that's performing the formatting of the line. You can access useful properties about the ongoing formatting operation from this object.</param>
        /// <param name = "textProperties">The <see cref = "T:Microsoft.VisualStudio.Text.Formatting.TextFormattingRunProperties" /> of the line for which <see cref = "T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> are to be provided. This paramter can be used to obtain formatting information about the textual contents of the line.</param>
        /// <param name = "line">The <see cref = "T:Microsoft.VisualStudio.Text.IMappingSpan" /> corresponding to the line that's being formatted/rendered.</param>
        /// <param name = "lineStart">The <see cref = "T:Microsoft.VisualStudio.Text.IMappingPoint" /> corresponding to the beginning of the line segment that's being formatted. This paramter is relevant for word-wrap scenarios where a single <see cref = "T:Microsoft.VisualStudio.Text.ITextSnapshotLine" /> results in multiple formatted/rendered lines on the view.</param>
        /// <param name = "lineSegment">The segment number of the line segment that's been currently formatted. This is a zero-based index and is applicable to word-wrapped lines. If a line is word-wrapped into 4 segments, you will receive 4 calls for the line with lineSegments of 0, 1, 2, and 3.</param>
        /// <returns>
        ///   A <see cref = "T:System.Windows.Media.TextFormatting.TextParagraphProperties" /> to be used when the line is being formatted.
        /// </returns>
        /// <remarks>
        ///   Please note that you can return a <see cref = "T:Microsoft.VisualStudio.Text.Formatting.TextFormattingParagraphProperties" /> which has a convenient set of basic properties defined.
        /// </remarks>
        public TextParagraphProperties Create( IFormattedLineSource formattedLineSource,
                                               TextFormattingRunProperties textProperties,
                                               IMappingSpan line,
                                               IMappingPoint lineStart,
                                               int lineSegment )
        {
            return new TextFormattingParagraphProperties(textProperties, formattedLineSource.TabSize);
        }
    }
}