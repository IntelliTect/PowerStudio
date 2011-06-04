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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

#endregion

namespace PowerStudio.LanguageService.Intellisense.Completion
{
    internal class SmartCompletionSource : CompletionSourceBase
    {
        private bool _IsDisposed;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "SmartCompletionSource" /> class.
        /// </summary>
        /// <param name = "sourceProvider">The source provider.</param>
        /// <param name = "textBuffer">The text buffer.</param>
        public SmartCompletionSource( CompletionSourceProvider sourceProvider, ITextBuffer textBuffer )
                : base( sourceProvider, textBuffer )
        {
        }

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
            if ( _IsDisposed )
            {
                return;
            }
            GC.SuppressFinalize( this );
            _IsDisposed = true;
        }

        /// <summary>
        ///   Determines which <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet" />s should be part of the specified <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSession" />.
        /// </summary>
        /// <param name = "session">The session for which completions are to be computed.</param>
        /// <param name = "completionSets">The set of the completionSets to be added to the session.</param>
        /// <remarks>
        ///   Each applicable <see cref = "M:Microsoft.VisualStudio.Language.Intellisense.ICompletionSource.AugmentCompletionSession(Microsoft.VisualStudio.Language.Intellisense.ICompletionSession,System.Collections.Generic.IList{Microsoft.VisualStudio.Language.Intellisense.CompletionSet})" /> instance will be called in-order to
        ///   (re)calculate a <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSession" />.  <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet" />s can be added to the session by adding
        ///   them to the completionSets collection passed-in as a parameter.  In addition, by removing items from the collection, a
        ///   source may filter <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet" />s provided by <see cref = "T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSource" />s earlier in the calculation
        ///   chain.
        /// </remarks>
        public override void AugmentCompletionSession( ICompletionSession session, IList<CompletionSet> completionSets )
        {
            completionSets.Clear();

            var completions = new List<Microsoft.VisualStudio.Language.Intellisense.Completion>();

            // TODO: This declaration set is a quick impl. It does not take scope or order in file into consideration
            string text = Buffer.CurrentSnapshot.GetText();
            Collection<PSParseError> errors;
            Collection<PSToken> tokens = PSParser.Tokenize( text, out errors );
            bool nextIsMethodName = false;
            foreach ( PSToken psToken in tokens )
            {
                if ( nextIsMethodName )
                {
                    nextIsMethodName = false;
                    var completion = new Microsoft.VisualStudio.Language.Intellisense.Completion(
                            psToken.Content, psToken.Content, null, null, null );
                    completions.Add( completion );
                    continue;
                }

                if ( psToken.Type == PSTokenType.Keyword &&
                     string.Equals( psToken.Content, "function" ) )
                {
                    nextIsMethodName = true;
                }
                if ( psToken.Type ==
                     PSTokenType.Variable )
                {
                    var completion = new Microsoft.VisualStudio.Language.Intellisense.Completion(
                            "$" + psToken.Content, "$" + psToken.Content, null, null, null );
                    completions.Add( completion );
                }
            }
            completions.Sort( new CompletionComparer() );
            completionSets.Add( new CompletionSet(
                                        "Tokens",
                                        //the non-localized title of the tab
                                        "Tokens",
                                        //the display title of the tab
                                        FindTokenSpanAtPosition( session.GetTriggerPoint( Buffer ), session ),
                                        completions.Distinct( new CompletionComparer() ),
                                        null ) );
        }
    }
}