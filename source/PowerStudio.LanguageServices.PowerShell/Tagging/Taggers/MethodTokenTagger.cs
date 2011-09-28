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
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using PowerStudio.LanguageServices.PowerShell.Tagging.Tags;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
{
    public class MethodTokenTagger : GlyphTokenTagger<MethodTag,PSToken>
    {
        public MethodTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
            Parse();
        }

        protected override List<MethodTag> GetTags(ITextSnapshot snapshot)
        {
            var methods = new List<MethodTag>();
            IEnumerable<PSToken> tokens = base.GetTokens( snapshot, false );
            bool nextIsMethodName = false;
            foreach ( PSToken psToken in tokens )
            {
                if ( nextIsMethodName )
                {
                    nextIsMethodName = false;
                    methods.Add( new MethodTag
                                 { Token = psToken, Span = CreateSnapshotSpan( snapshot, psToken ) } );
                    continue;
                }

                if ( psToken.Type == PSTokenType.Keyword &&
                     string.Equals( psToken.Content, "function" ) )
                {
                    nextIsMethodName = true;
                }
            }
            return methods;
        }
    }
}