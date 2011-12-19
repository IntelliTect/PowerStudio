#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.LanguageServices.Ini.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Providers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Ini.Tagging.Providers
{
    [Export( typeof (ITaggerProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [TagType( typeof (TokenClassificationTag<IniToken>) )]
    public class ClassificationTaggerProvider : TaggerProviderBase
    {
#pragma warning disable 0649
        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry;
#pragma warning restore 0649

        protected override Func<ITagger<T>> GetFactory<T>( ITextBuffer buffer )
        {
            var tokenClassification = new TokenClassifier( ClassificationTypeRegistry, buffer );
            return () => new ClassificationTagger( buffer, tokenClassification ) as ITagger<T>;
        }
    }
}