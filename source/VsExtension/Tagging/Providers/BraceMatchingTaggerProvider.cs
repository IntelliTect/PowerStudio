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
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Tagging.Taggers;
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Tagging.Providers
{
    [Export( typeof (IViewTaggerProvider) )]
    [TagType( typeof (BraceMatchingTag) )]
    [ContentType( LanguageConfiguration.Name )]
    public class BraceMatchingTaggerProvider : ViewTaggerProviderBase
    {
        protected override ITagger<T> GetTagger<T>( ITextView textView, ITextBuffer buffer )
        {
            var tagger = new BraceMatchingTagger( textView, buffer ) as ITagger<T>;
            return tagger;
        }
    }
}