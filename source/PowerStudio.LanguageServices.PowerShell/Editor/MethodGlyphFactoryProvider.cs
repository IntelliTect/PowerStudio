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
using System.Management.Automation;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.LanguageServices.Editor;
using PowerStudio.LanguageServices.PowerShell.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Editor
{
    [Export( typeof (IGlyphFactoryProvider) )]
    [Name( "MethodGlyph" )]
    [Order( After = "VsTextMarker" )]
    [ContentType( LanguageConfiguration.Name )]
    [TagType( typeof (MethodTag) )]
    internal sealed class MethodGlyphFactoryProvider : GlyphFactoryProviderBase<MethodTag, PSToken>
    {
        protected override Func<IGlyphFactory<MethodTag, PSToken>> GetFactory( IWpfTextView view,
                                                                               IWpfTextViewMargin margin )
        {
            return () => new MethodGlyphFactory() as IGlyphFactory<MethodTag, PSToken>;
        }
    }
}