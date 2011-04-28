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
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

#endregion

namespace PowerStudio.VsExtension.Intellisense.Completion
{
    internal class CompletionSource : ICompletionSource
    {
        private static readonly IEnumerable<string> BuiltInCompletions;
        private readonly ITextBuffer _Buffer;
        private readonly CompletionSourceProvider _SourceProvider;
        private List<Microsoft.VisualStudio.Language.Intellisense.Completion> _Completions;
        private bool _IsDisposed;

        static CompletionSource()
        {
            IEnumerable<string> keywords = SplitText( Resources.Keywords );
            IEnumerable<string> variables = SplitText( Resources.BuiltInVariables );
            IEnumerable<string> preferenceVariables = SplitText( Resources.PreferenceVariables );
            IEnumerable<string> cmdlets = SplitText( Resources.CmdLets );
            IEnumerable<string> aliases = SplitText( Resources.Aliases );
            BuiltInCompletions =
                    keywords.Union( variables ).Union( preferenceVariables ).Union( cmdlets ).Union( aliases ).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompletionSource"/> class.
        /// </summary>
        /// <param name="sourceProvider">The source provider.</param>
        /// <param name="textBuffer">The text buffer.</param>
        public CompletionSource( CompletionSourceProvider sourceProvider, ITextBuffer textBuffer )
        {
            _SourceProvider = sourceProvider;
            _Buffer = textBuffer;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if ( _IsDisposed )
            {
                return;
            }
            GC.SuppressFinalize( this );
            _IsDisposed = true;
        }

        /// <summary>
        /// Determines which <see cref="T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet"/>s should be part of the specified <see cref="T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSession"/>.
        /// </summary>
        /// <param name="session">The session for which completions are to be computed.</param><param name="completionSets">The set of the completionSets to be added to the session.</param>
        /// <remarks>
        /// Each applicable <see cref="M:Microsoft.VisualStudio.Language.Intellisense.ICompletionSource.AugmentCompletionSession(Microsoft.VisualStudio.Language.Intellisense.ICompletionSession,System.Collections.Generic.IList{Microsoft.VisualStudio.Language.Intellisense.CompletionSet})"/> instance will be called in-order to
        ///             (re)calculate a <see cref="T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSession"/>.  <see cref="T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet"/>s can be added to the session by adding
        ///             them to the completionSets collection passed-in as a parameter.  In addition, by removing items from the collection, a
        ///             source may filter <see cref="T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet"/>s provided by <see cref="T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSource"/>s earlier in the calculation
        ///             chain.
        /// </remarks>
        public void AugmentCompletionSession( ICompletionSession session, IList<CompletionSet> completionSets )
        {
            completionSets.Clear();

            _Completions = new List<Microsoft.VisualStudio.Language.Intellisense.Completion>();
            foreach ( string completion in BuiltInCompletions )
            {
                _Completions.Add( new Microsoft.VisualStudio.Language.Intellisense.Completion( completion,
                                                                                               completion,
                                                                                               null,
                                                                                               null,
                                                                                               null ) );
            }

            // TODO: This declaration set is a quick impl. It does not take scope or order in file into consideration
            string text = _Buffer.CurrentSnapshot.GetText();
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
                    _Completions.Add( completion );
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
                    _Completions.Add( completion );
                }
            }
            _Completions.Sort( new CompletionComparer() );
            completionSets.Add( new CompletionSet(
                                        "Tokens",
                                        //the non-localized title of the tab
                                        "Tokens",
                                        //the display title of the tab
                                        FindTokenSpanAtPosition( session.GetTriggerPoint( _Buffer ), session ),
                                        _Completions.Distinct( new CompletionComparer() ),
                                        null ) );
        }

        #endregion

        private static IEnumerable<string> SplitText( string text )
        {
            return text.Split( new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries )
                    .ToList();
        }

        private ITrackingSpan FindTokenSpanAtPosition( ITrackingPoint point, ICompletionSession session )
        {
            SnapshotPoint currentPoint = ( session.TextView.Caret.Position.BufferPosition ) - 1;
            ITextStructureNavigator navigator =
                    _SourceProvider.NavigatorService.GetTextStructureNavigator( _Buffer );
            TextExtent extent = navigator.GetExtentOfWord( currentPoint );
            return currentPoint.Snapshot.CreateTrackingSpan( extent.Span, SpanTrackingMode.EdgeInclusive );
        }

        #region Nested type: CompletionComparer

        private class CompletionComparer : IEqualityComparer<Microsoft.VisualStudio.Language.Intellisense.Completion>,
                                           IComparer<Microsoft.VisualStudio.Language.Intellisense.Completion>
        {
            private static readonly IComparer<string> DefaultStringComparer =
                    StringComparer.Create( CultureInfo.CurrentCulture, false );

            #region Implementation of IEqualityComparer<in Completion>

            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <returns>
            /// true if the specified objects are equal; otherwise, false.
            /// </returns>
            /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
            public bool Equals( Microsoft.VisualStudio.Language.Intellisense.Completion x,
                                Microsoft.VisualStudio.Language.Intellisense.Completion y )
            {
                return string.Equals( x.DisplayText, y.DisplayText, StringComparison.OrdinalIgnoreCase );
            }

            /// <summary>
            /// Returns a hash code for the specified object.
            /// </summary>
            /// <returns>
            /// A hash code for the specified object.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
            public int GetHashCode( Microsoft.VisualStudio.Language.Intellisense.Completion obj )
            {
                return obj.DisplayText.GetHashCode();
            }

            #endregion

            #region Implementation of IComparer<in Completion>

            /// <summary>
            /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/>, as shown in the following table.Value Meaning Less than zero<paramref name="x"/> is less than <paramref name="y"/>.Zero<paramref name="x"/> equals <paramref name="y"/>.Greater than zero<paramref name="x"/> is greater than <paramref name="y"/>.
            /// </returns>
            /// <param name="x">The first object to compare.</param><param name="y">The second object to compare.</param>
            public int Compare( Microsoft.VisualStudio.Language.Intellisense.Completion x,
                                Microsoft.VisualStudio.Language.Intellisense.Completion y )
            {
                if ( x == null &&
                     y == null )
                {
                    return 0;
                }
                if ( x != null &&
                     y == null )
                {
                    return 1;
                }
                if ( x == null )
                {
                    return -1;
                }
                return DefaultStringComparer.Compare( x.DisplayText, y.DisplayText );
            }

            #endregion
        }

        #endregion
    }
}