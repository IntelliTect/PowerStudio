﻿#region License

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
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Tagging.Taggers;
using PowerStudio.VsExtension.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Tagging.Providers
{
    [Export( typeof (IViewTaggerProvider) )]
    [TagType( typeof (HighlightWordTag) )]
    [ContentType( LanguageConfiguration.Name )]
    public class HighlightWordTaggerProvider : ViewTaggerProviderBase
    {
        [Import]
        internal ITextSearchService TextSearchService { get; set; }

        [Import]
        internal ITextStructureNavigatorSelectorService TextStructureNavigatorSelector { get; set; }

        protected override ITagger<T> GetTagger<T>( ITextView textView, ITextBuffer buffer )
        {
            ITextStructureNavigator textStructureNavigator =
                    TextStructureNavigatorSelector.GetTextStructureNavigator( buffer );
            var tagger =
                    new HighlightWordTagger( textView, buffer, TextSearchService, textStructureNavigator ) as ITagger<T>;
            return tagger;
        }
    }
}