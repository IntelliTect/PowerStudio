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
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.LanguageService.Tagging.Taggers;
using PowerStudio.LanguageService.Tagging.Tags;
using PowerStudio.VsExtension;

#endregion

namespace PowerStudio.LanguageService.Tagging.Providers
{
    [Export( typeof (ITaggerProvider) )]
    [TagType( typeof (OutliningTag) )]
    [ContentType( LanguageConfiguration.Name )]
    public class OutliningTaggerProvider : TaggerProviderBase
    {
        protected override Func<ITagger<T>> GetFactory<T>( ITextBuffer buffer )
        {
            return () => new OutliningTagger( buffer ) as ITagger<T>;
        }
    }
}