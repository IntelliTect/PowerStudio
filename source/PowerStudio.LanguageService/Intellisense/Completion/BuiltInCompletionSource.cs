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
using System.Linq;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

#endregion

namespace PowerStudio.LanguageService.Intellisense.Completion
{
    internal class BuiltInCompletionSource : CompletionSourceBase
    {
        private static readonly IEnumerable<string> BuiltInCompletions;
        private static readonly List<Microsoft.VisualStudio.Language.Intellisense.Completion> Completions;
        private bool _IsDisposed;

        static BuiltInCompletionSource()
        {
            IEnumerable<string> keywords = SplitMultiLineTextIntoACollectionOfLines( Resources.Keywords );
            IEnumerable<string> variables =
                    SplitMultiLineTextIntoACollectionOfLines( Resources.BuiltInVariables );
            IEnumerable<string> preferenceVariables =
                    SplitMultiLineTextIntoACollectionOfLines( Resources.PreferenceVariables );
            IEnumerable<string> cmdlets = SplitMultiLineTextIntoACollectionOfLines( Resources.CmdLets );
            IEnumerable<string> aliases = SplitMultiLineTextIntoACollectionOfLines( Resources.Aliases );
            BuiltInCompletions =
                    ( keywords.Union( variables ).Union( preferenceVariables ).Union( cmdlets ).Union( aliases ) ).
                            ToList();

            Completions = BuiltInCompletions
                    .Select( CreateCompletion )
                    .ToList();

            Completions.Sort( new CompletionComparer() );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BuiltInCompletionSource"/> class.
        /// </summary>
        /// <param name="sourceProvider">The source provider.</param>
        /// <param name="textBuffer">The text buffer.</param>
        public BuiltInCompletionSource( CompletionSourceProvider sourceProvider, ITextBuffer textBuffer )
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
            UpdateDefaultCompletionSet( session, completionSets, Completions );
        }

        private static IEnumerable<string> SplitMultiLineTextIntoACollectionOfLines( string text )
        {
            List<string> items = text.Split( new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries )
                    .ToList();
            return items;
        }
    }
}