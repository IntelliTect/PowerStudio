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
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Parsing;

#endregion

namespace PowerStudio.VsExtension.Intellisense
{
    [Export( typeof (IQuickInfoSourceProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [Name( LanguageConfiguration.Name + "QuickInfo" )]
    public class PowerShellQuickInfoSourceProvider : IQuickInfoSourceProvider
    {
        [Import]
        private IBufferTagAggregatorFactoryService TagAggregatorFactory { get; set; }

        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        #region IQuickInfoSourceProvider Members

        /// <summary>
        ///   Creates a Quick Info provider for the specified context.
        /// </summary>
        /// <param name = "textBuffer">The text buffer for which to create a provider.</param>
        /// <returns>
        ///   A valid <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.IQuickInfoSource" /> instance, or null if none could be created.
        /// </returns>
        public IQuickInfoSource TryCreateQuickInfoSource( ITextBuffer textBuffer )
        {
            return new PowerShellQuickInfoSource( textBuffer,
                                                  TagAggregatorFactory.CreateTagAggregator<ErrorTag>( textBuffer ),
                                                  this );
        }

        #endregion
    }
}