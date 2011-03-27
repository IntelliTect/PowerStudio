#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System.Collections.Generic;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;

namespace PowerStudio.VsExtension.Parsing
{
    public class PsSource : Source
    {
        public PsSource( PowerShellLanguageService service, IVsTextLines textLines, Colorizer colorizer )
                : base( service, textLines, colorizer )
        {
        }

        public object ParseResult { get; set; }

        public IList<TextSpan[]> Braces { get; set; }

        public override CommentInfo GetCommentFormat()
        {
            return Configuration.CommentInfo;
        }
    }
}