﻿#region License

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
using PowerStudio.VsExtension.Tagging.Taggers;
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Tagging.Providers
{
    [Export( typeof (ITaggerProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [TagType( typeof (TokenClassificationTag) )]
    public class ClassificationTaggerProvider : TaggerProviderBase
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry;

        protected override Func<ITagger<T>> GetFactory<T>( ITextBuffer buffer )
        {
            var tokenClassification = new TokenClassifier( ClassificationTypeRegistry, buffer );
            return () => new ClassificationTagger( buffer, tokenClassification ) as ITagger<T>;
        }
    }
}