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
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.LanguageServices.PowerShell.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Providers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Providers
{
    [Export( typeof (ITaggerProvider) )]
    [ContentType( LanguageConfiguration.Name )]
    [TagType(typeof(ErrorTokenTag<PSToken>))]
    [Order( Before = Priority.Default )]
    public class ErrorTokenTaggerProvider : TaggerProviderBase
    {
        protected override Func<ITagger<T>> GetFactory<T>( ITextBuffer buffer )
        {
            return () => new ErrorTokenTagger( buffer ) as ITagger<T>;
        }
    }
}