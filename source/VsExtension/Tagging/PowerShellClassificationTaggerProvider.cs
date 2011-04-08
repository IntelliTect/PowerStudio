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
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Parsing;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    [Export( typeof (ITaggerProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [TagType( typeof (ClassificationTag) )]
    public class PowerShellClassificationTaggerProvider : ITaggerProvider
    {
        [Import]
        internal IBufferTagAggregatorFactoryService AggregatorFactory;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry;

        #region Implementation of ITaggerProvider

        /// <summary>
        ///   Creates the tagger.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "buffer">The buffer.</param>
        /// <returns></returns>
        public ITagger<T> CreateTagger<T>( ITextBuffer buffer ) where T : ITag
        {
            ITagAggregator<PowerShellTokenTag> tagAggregator =
                    AggregatorFactory.CreateTagAggregator<PowerShellTokenTag>( buffer );
            var tokenClassification = new TokenClassification( ClassificationTypeRegistry );
            return (ITagger<T>) new PowerShellClassificationTagger( buffer, tagAggregator, tokenClassification );
        }

        #endregion
    }
}