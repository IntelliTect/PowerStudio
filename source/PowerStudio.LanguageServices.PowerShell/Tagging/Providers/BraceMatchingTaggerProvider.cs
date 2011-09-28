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
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.LanguageServices.PowerShell.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Providers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Providers
{
    [Export( typeof (IViewTaggerProvider) )]
    [TagType( typeof (BraceMatchingTag<PSToken>) )]
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