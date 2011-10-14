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
using Microsoft.VisualStudio.Text;
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Ini.Tagging.Taggers
{
    public abstract class IniTokenTagger<TTokenTag> : TaggerBase<TTokenTag, IniToken>
            where TTokenTag : TokenTag<IniToken>
    {
        protected IniParser Parser = new IniParser();

        protected IniTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
        }

        /// <summary>
        ///   Gets the tokens by parsing the text snapshot, optionally including any errors.
        /// </summary>
        /// <param name = "textSnapshot">The text snapshot.</param>
        /// <param name = "includeErrors">if set to <c>true</c> [include errors].</param>
        /// <returns></returns>
        protected override IEnumerable<IniToken> GetTokens( ITextSnapshot textSnapshot, bool includeErrors )
        {
            string text = textSnapshot.GetText();
            IEnumerable<IniToken> tokens = Parser.Parse( text );
            return tokens;
        }

        /// <summary>
        ///   Creates a <see cref = "SnapshotSpan" /> for the given text snapshot and token.
        /// </summary>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "token">The token.</param>
        /// <returns></returns>
        protected override SnapshotSpan CreateSnapshotSpan( ITextSnapshot snapshot, IniToken token )
        {
            return new SnapshotSpan( snapshot, new Span( token.Start, token.Length ) );
        }

        /// <summary>
        ///   Creates a <see cref = "SnapshotSpan" /> for the given text snapshot covering all text between
        ///   the start and end tokens - (startToken, endToken)
        /// </summary>
        /// <param name = "snapshot">The current snapshot.</param>
        /// <param name = "startToken">The start token of the span.</param>
        /// <param name = "endToken">The end token of the span.</param>
        /// <returns></returns>
        protected override SnapshotSpan CreateSnapshotSpan( ITextSnapshot snapshot,
                                                            IniToken startToken,
                                                            IniToken endToken )
        {
            SnapshotSpan startSnapshot = CreateSnapshotSpan( snapshot, startToken );
            SnapshotSpan endSnapshot = CreateSnapshotSpan( snapshot, endToken );
            return new SnapshotSpan( startSnapshot.Start, endSnapshot.End );
        }
    }
}