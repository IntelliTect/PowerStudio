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
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
{
    public class OutliningTagger : TaggerBase<OutliningTag>
    {
        public OutliningTagger( ITextBuffer buffer )
                : base( buffer )
        {
            Parse();
        }

        protected override bool IsTokenInSpan( OutliningTag tag, ITextSnapshot snapshot, SnapshotSpan span )
        {
            int startLineNumber = span.Start.GetContainingLine().LineNumber;
            int endLineNumber = span.End.GetContainingLine().LineNumber;
            return tag.StartLine <= endLineNumber && tag.EndLine >= startLineNumber;
        }

        protected override List<OutliningTag> GetTags( ITextSnapshot snapshot )
        {
            var regions = new List<OutliningTag>();
            var stack = new Stack<PSToken>();
            IEnumerable<PSToken> tokens = GetTokens( snapshot, true );
            foreach ( PSToken token in tokens )
            {
                switch ( token.Type )
                {
                    case PSTokenType.GroupStart:
                    {
                        stack.Push( token );
                        break;
                    }
                    case PSTokenType.GroupEnd:
                    {
                        if ( stack.Count == 0 )
                        {
                            continue;
                        }
                        PSToken startToken = stack.Pop();
                        if ( ( token.EndLine - startToken.StartLine ) <
                             LanguageConfiguration.OutlineThreshold )
                        {
                            continue;
                        }
                        regions.Add( new OutliningTag( snapshot,
                                                       CreateSnapshotSpan( snapshot, startToken, token ),
                                                       false )
                                     {
                                             StartLine = startToken.StartLine,
                                             EndLine = token.StartLine,
                                     } );
                    }
                        break;
                    default:
                        break;
                }
            }
            return regions;
        }
    }
}