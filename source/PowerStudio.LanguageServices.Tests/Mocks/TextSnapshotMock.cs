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
using System.IO;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

#endregion

namespace PowerStudio.LanguageServices.Tests.Mocks
{
    public class TextSnapshotMock : ITextSnapshot
    {
        private readonly TextBufferMock _TextBuffer;

        public TextSnapshotMock( TextBufferMock textBuffer )
        {
            _TextBuffer = textBuffer;
        }

        #region Implementation of ITextSnapshot

        /// <summary>
        ///   Gets text from the snapshot starting at the beginning of the span and having length equal to the length of the span.
        /// </summary>
        /// <param name = "span">The span to return.</param>
        /// <exception cref = "T:System.ArgumentOutOfRangeException">The end of the span is greater than <see cref = "P:Microsoft.VisualStudio.Text.ITextSnapshot.Length" />.</exception>
        /// <returns>
        ///   A non-null string.
        /// </returns>
        public string GetText( Span span )
        {
            return GetText().Substring( span.Start, span.Length );
        }

        /// <summary>
        ///   Gets text from the snapshot starting at <paramref name = "startIndex" /> and having length equal to <paramref name = "length" />.
        /// </summary>
        /// <param name = "startIndex">The starting index.</param>
        /// <param name = "length">The length of text to get.</param>
        /// <returns>
        ///   The string of length <paramref name = "length" /> starting at <paramref name = "startIndex" /> in the underlying <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "startIndex" /> is less than zero or greater than the length of the snapshot,
        ///   or <paramref name = "length" /> is less than zero, or <paramref name = "startIndex" /> plus <paramref name = "length" /> is greater than the length of the snapshot.</exception>
        public string GetText( int startIndex, int length )
        {
            return GetText().Substring( startIndex, length );
        }

        /// <summary>
        ///   Gets all the text in the snapshot.
        /// </summary>
        /// <returns>
        ///   A non-null string.
        /// </returns>
        public string GetText()
        {
            return _TextBuffer.Content;
        }

        /// <summary>
        ///   Converts a range of text to a character array.
        /// </summary>
        /// <param name = "startIndex">The starting index of the range of text.
        /// </param>
        /// <param name = "length">The length of the text.
        /// </param>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "startIndex" /> is less than zero or greater than the length of the snapshot, or
        ///   <paramref name = "length" /> is less than zero, or <paramref name = "startIndex" /> plus <paramref name = "length" /> is greater than the length of the snapshot.</exception>
        /// <returns>
        ///   The array of characters starting at <paramref name = "startIndex" /> in the underlying <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" /> and extend to its end.
        /// </returns>
        public char[] ToCharArray( int startIndex, int length )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Copies a range of text to a character array.
        /// </summary>
        /// <param name = "sourceIndex">The starting index in the text snapshot.
        /// </param>
        /// <param name = "destination">The destination array.
        /// </param>
        /// <param name = "destinationIndex">The index in the destination array at which to start copying the text.
        /// </param>
        /// <param name = "count">The number of characters to copy.
        /// </param>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "sourceIndex" /> is less than zero or greater than the length of the snapshot, or
        ///   <paramref name = "count" /> is less than zero, or <paramref name = "sourceIndex" /> + <paramref name = "count" /> is greater than the length of the snapshot, or
        ///   <paramref name = "destinationIndex" /> is less than zero, or <paramref name = "destinationIndex" /> plus <paramref name = "count" /> is greater than the length of <paramref name = "destination" />.</exception>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "destination" /> is null.</exception>
        public void CopyTo( int sourceIndex, char[] destination, int destinationIndex, int count )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates a <see cref = "T:Microsoft.VisualStudio.Text.ITrackingPoint" /> against this snapshot.
        /// </summary>
        /// <param name = "position">The position of the point.</param>
        /// <param name = "trackingMode">The tracking mode of the point.</param>
        /// <returns>
        ///   A non-null TrackingPoint.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is less than zero or greater than the length of the snapshot.</exception>
        public ITrackingPoint CreateTrackingPoint( int position, PointTrackingMode trackingMode )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates a <see cref = "T:Microsoft.VisualStudio.Text.ITrackingPoint" /> against this snapshot.
        /// </summary>
        /// <param name = "position">The position of the point.</param>
        /// <param name = "trackingMode">The tracking mode of the point.</param>
        /// <param name = "trackingFidelity">The tracking fidelity of the point.</param>
        /// <returns>
        ///   A non-null TrackingPoint.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is less than zero or greater than the length of the snapshot.</exception>
        /// <remarks>
        ///   This text point reprises its previous position when visiting a version that was created by undo or redo.
        /// </remarks>
        public ITrackingPoint CreateTrackingPoint( int position,
                                                   PointTrackingMode trackingMode,
                                                   TrackingFidelityMode trackingFidelity )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates a <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" /> against this snapshot.
        /// </summary>
        /// <param name = "span">The span of text in this snapshot.</param>
        /// <param name = "trackingMode">How the tracking span will react to changes at its boundaries.</param>
        /// <returns>
        ///   A non-null <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" />.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException">The end of the span is greater than the length of the text snapshot.</exception>
        public ITrackingSpan CreateTrackingSpan( Span span, SpanTrackingMode trackingMode )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates a <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" /> against this snapshot.
        /// </summary>
        /// <param name = "span">The span of text in this snapshot.</param>
        /// <param name = "trackingMode">How the tracking span should react to changes at its boundaries.</param>
        /// <param name = "trackingFidelity">The tracking fidelity of the span.</param>
        /// <returns>
        ///   A non-null <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" />.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException">The end of the span is greater than the length of the text snapshot.</exception>
        public ITrackingSpan CreateTrackingSpan( Span span,
                                                 SpanTrackingMode trackingMode,
                                                 TrackingFidelityMode trackingFidelity )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates a <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" /> against this snapshot.
        /// </summary>
        /// <param name = "start">The starting position of the tracking span.</param>
        /// <param name = "length">The length of the tracking span.</param>
        /// <param name = "trackingMode">How the tracking span should react to changes at its boundaries.</param>
        /// <returns>
        ///   A non-null <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" />.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "start" /> is negative or greater than <see cref = "P:Microsoft.VisualStudio.Text.ITextSnapshot.Length" />, or
        ///   <paramref name = "length" /> is negative, or <paramref name = "start" /> plus <paramref name = "length" />
        ///   is less than <paramref name = "start" />.</exception>
        public ITrackingSpan CreateTrackingSpan( int start, int length, SpanTrackingMode trackingMode )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates a <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" /> against this snapshot.
        /// </summary>
        /// <param name = "start">The starting position of the tracking span.</param>
        /// <param name = "length">The length of the tracking span.</param>
        /// <param name = "trackingMode">How the tracking span should react to changes at its boundaries.</param>
        /// <param name = "trackingFidelity">The tracking fidelilty mode.</param>
        /// <returns>
        ///   A non-null <see cref = "T:Microsoft.VisualStudio.Text.ITrackingSpan" />..
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "start" /> is negative or greater than <see cref = "P:Microsoft.VisualStudio.Text.ITextSnapshot.Length" />, or
        ///   <paramref name = "length" /> is negative, or <paramref name = "start" /> plus <paramref name = "length" />
        ///   is less than <paramref name = "start" />.</exception>
        public ITrackingSpan CreateTrackingSpan( int start,
                                                 int length,
                                                 SpanTrackingMode trackingMode,
                                                 TrackingFidelityMode trackingFidelity )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Gets an <see cref = "T:Microsoft.VisualStudio.Text.ITextSnapshotLine" /> for the given line number.
        /// </summary>
        /// <param name = "lineNumber">The line number.</param>
        /// <returns>
        ///   A non-null <see cref = "T:Microsoft.VisualStudio.Text.ITextSnapshotLine" />.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "lineNumber" /> is less than zero or greater than or equal to LineCount/&gt;.</exception>
        public ITextSnapshotLine GetLineFromLineNumber( int lineNumber )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Gets an <see cref = "T:Microsoft.VisualStudio.Text.ITextSnapshotLine" /> for a line at the given position.
        /// </summary>
        /// <param name = "position">The position.</param>
        /// <returns>
        ///   A non-null <see cref = "T:Microsoft.VisualStudio.Text.ITextSnapshotLine" />.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is less than zero or greater than length of line.</exception>
        public ITextSnapshotLine GetLineFromPosition( int position )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Gets the number of the line that contains the character at the specified position.
        /// </summary>
        /// <returns>
        ///   The line number of the line in which <paramref name = "position" /> lies.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is less than zero or greater than Length/&gt;.</exception>
        public int GetLineNumberFromPosition( int position )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Writes a substring of the contents of the snapshot.
        /// </summary>
        /// <param name = "writer">The <see cref = "T:System.IO.TextWriter" /> to use.</param>
        /// <param name = "span">The span of text to write.</param>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "writer" /> is null.</exception>
        /// <exception cref = "T:System.ArgumentOutOfRangeException">The end of the span is greater than the length of the snapshot.
        /// </exception>
        public void Write( TextWriter writer, Span span )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Writes the contents of the snapshot.
        /// </summary>
        /// <param name = "writer">The <see cref = "T:System.IO.TextWriter" />to use.</param>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "writer" /> is null.</exception>
        public void Write( TextWriter writer )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   The <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" /> of which this is a snapshot.
        /// </summary>
        /// <remarks>
        ///   This property always returns the same <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" /> object, but the <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" /> is not itself immutable.
        /// </remarks>
        public ITextBuffer TextBuffer
        {
            get { return _TextBuffer; }
        }

        /// <summary>
        ///   The <see cref = "T:Microsoft.VisualStudio.Utilities.IContentType" /> of the <see cref = "P:Microsoft.VisualStudio.Text.ITextSnapshot.TextBuffer" /> when this snapshot was current.
        /// </summary>
        public IContentType ContentType { get; private set; }

        /// <summary>
        ///   The version of the <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" /> that this <see cref = "T:Microsoft.VisualStudio.Text.ITextSnapshot" /> represents.
        /// </summary>
        /// <remarks>
        ///   This property always returns the same <see cref = "T:Microsoft.VisualStudio.Text.ITextVersion" />. The <see cref = "P:Microsoft.VisualStudio.Text.ITextVersion.Changes" /> property is
        ///   initially null and becomes populated when it ceases to be the most recent version.
        /// </remarks>
        public ITextVersion Version { get; private set; }

        /// <summary>
        ///   Gets the number of UTF-16 characters contained in the snapshot.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        ///   Gets the positive number of lines in the snapshot. A snapshot whose <see cref = "P:Microsoft.VisualStudio.Text.ITextSnapshot.Length" /> is zero is considered to have one line.
        /// </summary>
        public int LineCount { get; private set; }

        /// <summary>
        ///   Gets a single character at the specified position.
        /// </summary>
        /// <param name = "position">The position of the character.</param>
        /// <returns>
        ///   The character at <paramref name = "position" />.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is less than zero or greater than or equal to the length of the snapshot.</exception>
        public char this[ int position ]
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   An enumerator for the set of lines in the snapshot.
        /// </summary>
        public IEnumerable<ITextSnapshotLine> Lines { get; private set; }

        #endregion
    }
}