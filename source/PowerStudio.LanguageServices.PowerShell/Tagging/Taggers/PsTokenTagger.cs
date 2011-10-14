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
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using PowerStudio.LanguageServices.Tagging.Taggers;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging.Taggers
{
    public abstract class PsViewTokenTagger<TTokenTag> : ViewTaggerBase<TTokenTag, PSToken>
            where TTokenTag : TokenTag<PSToken>
    {
        protected PsViewTokenTagger(ITextView view, ITextBuffer buffer)
            : base(view, buffer)
        {
        }

        /// <summary>
        ///   Gets the tokens by parsing the text snapshot, optionally including any errors.
        /// </summary>
        /// <param name = "textSnapshot">The text snapshot.</param>
        /// <param name = "includeErrors">if set to <c>true</c> [include errors].</param>
        /// <returns></returns>
        protected override IEnumerable<PSToken> GetTokens(ITextSnapshot textSnapshot, bool includeErrors)
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            Collection<PSToken> tokens = PSParser.Tokenize(text, out errors);
            if (includeErrors)
            {
                return tokens.Union(errors.Select(error => error.Token)).ToList();
            }
            return tokens;
        }

        /// <summary>
        ///   Gets the error tokens by parsing the text snapshot.
        /// </summary>
        /// <param name = "textSnapshot">The text snapshot.</param>
        /// <returns></returns>
        protected virtual IEnumerable<PSParseError> GetErrorTokens(ITextSnapshot textSnapshot)
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            PSParser.Tokenize(text, out errors);
            return errors;
        }

        /// <summary>
        ///   Creates a <see cref = "SnapshotSpan" /> for the given text snapshot and token.
        /// </summary>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "token">The token.</param>
        /// <returns></returns>
        protected override SnapshotSpan CreateSnapshotSpan(ITextSnapshot snapshot, PSToken token)
        {
            return new SnapshotSpan(snapshot, new Span(token.Start, token.Length));
        }

        /// <summary>
        ///   Creates a <see cref = "SnapshotSpan" /> for the given text snapshot covering all text between
        ///   the start and end tokens - (startToken, endToken)
        /// </summary>
        /// <param name = "snapshot">The current snapshot.</param>
        /// <param name = "startToken">The start token of the span.</param>
        /// <param name = "endToken">The end token of the span.</param>
        /// <returns></returns>
        protected override SnapshotSpan CreateSnapshotSpan(ITextSnapshot snapshot, PSToken startToken, PSToken endToken)
        {
            SnapshotSpan startSnapshot = CreateSnapshotSpan(snapshot, startToken);
            SnapshotSpan endSnapshot = CreateSnapshotSpan(snapshot, endToken);
            return new SnapshotSpan(startSnapshot.Start, endSnapshot.End);
        }
    }

    public abstract class PsTokenTagger<TTokenTag> : TaggerBase<TTokenTag, PSToken>
            where TTokenTag : TokenTag<PSToken>
    {
        protected PsTokenTagger( ITextBuffer buffer )
                : base( buffer )
        {
        }

        /// <summary>
        ///   Gets the tokens by parsing the text snapshot, optionally including any errors.
        /// </summary>
        /// <param name = "textSnapshot">The text snapshot.</param>
        /// <param name = "includeErrors">if set to <c>true</c> [include errors].</param>
        /// <returns></returns>
        protected override IEnumerable<PSToken> GetTokens( ITextSnapshot textSnapshot, bool includeErrors )
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            Collection<PSToken> tokens = PSParser.Tokenize( text, out errors );
            if ( includeErrors )
            {
                return tokens.Union( errors.Select( error => error.Token ) ).ToList();
            }
            return tokens;
        }

        /// <summary>
        ///   Gets the error tokens by parsing the text snapshot.
        /// </summary>
        /// <param name = "textSnapshot">The text snapshot.</param>
        /// <returns></returns>
        protected virtual IEnumerable<PSParseError> GetErrorTokens( ITextSnapshot textSnapshot )
        {
            string text = textSnapshot.GetText();
            Collection<PSParseError> errors;
            PSParser.Tokenize( text, out errors );
            return errors;
        }

        /// <summary>
        ///   Creates a <see cref = "SnapshotSpan" /> for the given text snapshot and token.
        /// </summary>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "token">The token.</param>
        /// <returns></returns>
        protected override SnapshotSpan CreateSnapshotSpan( ITextSnapshot snapshot, PSToken token )
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
        protected override SnapshotSpan CreateSnapshotSpan( ITextSnapshot snapshot, PSToken startToken, PSToken endToken )
        {
            SnapshotSpan startSnapshot = CreateSnapshotSpan( snapshot, startToken );
            SnapshotSpan endSnapshot = CreateSnapshotSpan( snapshot, endToken );
            return new SnapshotSpan( startSnapshot.Start, endSnapshot.End );
        }
    }
}