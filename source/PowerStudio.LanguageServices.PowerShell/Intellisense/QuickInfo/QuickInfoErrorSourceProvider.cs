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
using System.Management.Automation;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.LanguageServices.Intellisense.QuickInfo;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Intellisense.QuickInfo
{
    [Export( typeof (IQuickInfoSourceProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [Name( LanguageConfiguration.Name + "Error QuickInfo" )]
    public class QuickInfoErrorSourceProvider : QuickInfoSourceProvider<ErrorTokenTag<PSToken>,PSToken>
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
            return new QuickInfoErrorSource<PSToken>( textBuffer,
                                                      TagAggregatorFactory.CreateTagAggregator<ErrorTokenTag<PSToken>>(
                                                              textBuffer ),
                                                      this );
        }
    }
}