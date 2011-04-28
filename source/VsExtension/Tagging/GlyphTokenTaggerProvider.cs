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

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    [Export( typeof (ITaggerProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [TagType( typeof (GlyphTag) )]
    [Order( Before = Priority.Default )]
    public class GlyphTokenTaggerProvider : TaggerProviderBase
    {
        protected override Func<ITagger<T>> GetFactory<T>( ITextBuffer buffer )
        {
            return () => new GlyphTokenTagger( buffer ) as ITagger<T>;
        }
    }
}