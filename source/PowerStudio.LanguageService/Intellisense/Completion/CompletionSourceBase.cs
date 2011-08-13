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
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

#endregion

namespace PowerStudio.LanguageService.Intellisense.Completion
{
    public abstract class CompletionSourceBase : ICompletionSource
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "CompletionSourceBase" /> class.
        /// </summary>
        /// <param name = "sourceProvider">The source provider.</param>
        /// <param name = "textBuffer">The text buffer.</param>
        protected CompletionSourceBase( CompletionSourceProvider sourceProvider, ITextBuffer textBuffer )
        {
            if ( sourceProvider == null )
            {
                throw new ArgumentNullException( "sourceProvider" );
            }

            if ( textBuffer == null )
            {
                throw new ArgumentNullException( "textBuffer" );
            }

            SourceProvider = sourceProvider;
            Buffer = textBuffer;
        }

        protected ITextBuffer Buffer { get; private set; }
        protected CompletionSourceProvider SourceProvider { get; private set; }

        #region ICompletionSource Members

        /// <summary>
        ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public abstract void Dispose();

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
        public abstract void AugmentCompletionSession( ICompletionSession session, IList<CompletionSet> completionSets );

        #endregion

        protected ITrackingSpan FindTokenSpanAtPosition( ITrackingPoint point, ICompletionSession session )
        {
            SnapshotPoint currentPoint = ( session.TextView.Caret.Position.BufferPosition ) - 1;
            ITextStructureNavigator navigator =
                    SourceProvider.NavigatorService.GetTextStructureNavigator( Buffer );
            TextExtent extent = navigator.GetExtentOfWord( currentPoint );
            return currentPoint.Snapshot.CreateTrackingSpan( extent.Span, SpanTrackingMode.EdgeInclusive );
        }

        protected virtual void UpdateDefaultCompletionSet( ICompletionSession session,
                                                           IList<CompletionSet> completionSets,
                                                           IEnumerable<
                                                                   Microsoft.VisualStudio.Language.Intellisense.
                                                                   Completion> completions )
        {
            const string DefaultCompletionSetName = "Default";
            if ( completionSets.Count == 0 ||
                 !completionSets.Any( item => item.DisplayName == DefaultCompletionSetName ) )
            {
                var completionSet = new CompletionSet(
                        DefaultCompletionSetName,
                        //the non-localized title of the tab
                        DefaultCompletionSetName,
                        //the display title of the tab
                        FindTokenSpanAtPosition( session.GetTriggerPoint( Buffer ), session ),
                        completions,
                        null );
                completionSets.Add( completionSet );
            }
            else
            {
                CompletionSet completionSet =
                        completionSets.Where( item => item.DisplayName == DefaultCompletionSetName ).First();
                completionSets.Remove( completionSet );
                List<Microsoft.VisualStudio.Language.Intellisense.Completion> allCompletions =
                        completions.Union( completionSet.Completions ).ToList();
                UpdateDefaultCompletionSet( session, completionSets, allCompletions );
            }
        }

        protected static Microsoft.VisualStudio.Language.Intellisense.Completion CreateCompletion( string content )
        {
            return new Microsoft.VisualStudio.Language.Intellisense.Completion(
                    content, content, content, null, null);
        }

        #region Nested type: CompletionComparer

        protected class CompletionComparer : IEqualityComparer<Microsoft.VisualStudio.Language.Intellisense.Completion>,
                                             IComparer<Microsoft.VisualStudio.Language.Intellisense.Completion>
        {
            private static readonly IComparer<string> DefaultStringComparer =
                    StringComparer.Create( CultureInfo.CurrentCulture, false );

            #region Implementation of IEqualityComparer<in Completion>

            /// <summary>
            ///   Determines whether the specified objects are equal.
            /// </summary>
            /// <returns>
            ///   true if the specified objects are equal; otherwise, false.
            /// </returns>
            /// <param name = "x">The first object of type <paramref name = "T" /> to compare.</param>
            /// <param name = "y">The second object of type <paramref name = "T" /> to compare.</param>
            public bool Equals( Microsoft.VisualStudio.Language.Intellisense.Completion x,
                                Microsoft.VisualStudio.Language.Intellisense.Completion y )
            {
                return string.Equals( x.DisplayText, y.DisplayText, StringComparison.OrdinalIgnoreCase );
            }

            /// <summary>
            ///   Returns a hash code for the specified object.
            /// </summary>
            /// <returns>
            ///   A hash code for the specified object.
            /// </returns>
            /// <param name = "obj">The <see cref = "T:System.Object" /> for which a hash code is to be returned.</param>
            /// <exception cref = "T:System.ArgumentNullException">The type of <paramref name = "obj" /> is a reference type and <paramref name = "obj" /> is null.</exception>
            public int GetHashCode( Microsoft.VisualStudio.Language.Intellisense.Completion obj )
            {
                return obj.DisplayText.GetHashCode();
            }

            #endregion

            #region Implementation of IComparer<in Completion>

            /// <summary>
            ///   Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
            /// </summary>
            /// <returns>
            ///   A signed integer that indicates the relative values of <paramref name = "x" /> and <paramref name = "y" />, as shown in the following table.Value Meaning Less than zero<paramref name = "x" /> is less than <paramref name = "y" />.Zero<paramref name = "x" /> equals <paramref name = "y" />.Greater than zero<paramref name = "x" /> is greater than <paramref name = "y" />.
            /// </returns>
            /// <param name = "x">The first object to compare.</param>
            /// <param name = "y">The second object to compare.</param>
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