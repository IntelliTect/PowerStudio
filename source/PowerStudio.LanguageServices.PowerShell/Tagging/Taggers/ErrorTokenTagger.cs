#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
{
    public class ErrorTokenTagger : PsTokenTagger<ErrorTokenTag<PSToken>>
    {
        public ErrorTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
            Parse();
        }

        protected override List<ErrorTokenTag<PSToken>> GetTags( ITextSnapshot snapshot )
        {
            List<ErrorTokenTag<PSToken>> tags = ( from error in GetErrorTokens( snapshot )
                                                  select new ErrorTokenTag<PSToken>( error.Message )
                                                         {
                                                                 Token = error.Token,
                                                                 Span = CreateSnapshotSpan( snapshot, error.Token )
                                                         } )
                    .ToList();
            return tags;
        }
    }
}