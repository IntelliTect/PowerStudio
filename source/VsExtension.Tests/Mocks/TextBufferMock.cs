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
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;

#endregion

namespace PowerStudio.VsExtension.Tests.Mocks
{
    public class TextBufferMock : ITextBuffer
    {
        public TextBufferMock( string content )
        {
            Content = content;
        }

        #region Implementation of IPropertyOwner

        /// <summary>
        ///   The collection of properties controlled by the property owner.
        /// </summary>
        public PropertyCollection Properties { get; private set; }

        #endregion

        #region Implementation of ITextBuffer

        /// <summary>
        ///   Creates an <see cref = "T:Microsoft.VisualStudio.Text.ITextEdit" /> object that handles compound edit operations on this buffer.
        /// </summary>
        /// <param name = "options">Options to apply to the compound edit operation.</param>
        /// <param name = "reiteratedVersionNumber">If not null, indicates that the version to be created by this edit operation is
        ///   the product of an undo or redo operation.</param>
        /// <param name = "editTag">An arbitrary object that will be associated with this edit transaction.</param>
        /// <returns>
        ///   A new <see cref = "T:Microsoft.VisualStudio.Text.ITextEdit" /> object.
        /// </returns>
        /// <exception cref = "T:System.InvalidOperationException">Another <see cref = "T:Microsoft.VisualStudio.Text.ITextBufferEdit" /> object is active for this text buffer, or 
        ///   <see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.CheckEditAccess" /> would return false.</exception>
        public ITextEdit CreateEdit( EditOptions options, int? reiteratedVersionNumber, object editTag )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates an <see cref = "T:Microsoft.VisualStudio.Text.ITextEdit" /> object that handles compound edit operations on this buffer.
        /// </summary>
        /// <returns>
        ///   A new <see cref = "T:Microsoft.VisualStudio.Text.ITextEdit" /> object.
        /// </returns>
        /// <exception cref = "T:System.InvalidOperationException">Another <see cref = "T:Microsoft.VisualStudio.Text.ITextBufferEdit" /> object is active for this text buffer.</exception>
        /// <remarks>
        ///   This method is equivalent to CreateEdit(EditOptions.None, null, null).
        /// </remarks>
        public ITextEdit CreateEdit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Creates an <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegionEdit" /> object that handles adding or removing read-only regions from this buffer.
        /// </summary>
        /// <returns>
        ///   A new <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegionEdit" /> object.
        /// </returns>
        /// <exception cref = "T:System.InvalidOperationException">Another <see cref = "T:Microsoft.VisualStudio.Text.ITextBufferEdit" /> object is active for this text buffer, or 
        ///   <see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.CheckEditAccess" /> would return false.</exception>
        public IReadOnlyRegionEdit CreateReadOnlyRegionEdit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Claims ownership of this buffer for the current thread. All subsequent modifications of this <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />
        ///   must be made from the current thread, or else an <see cref = "T:System.InvalidOperationException" /> will be raised.
        /// </summary>
        /// <exception cref = "T:System.InvalidOperationException">This method has been called previously from a different thread, or a
        ///   <see cref = "T:Microsoft.VisualStudio.Text.ITextBufferEdit" /> object is active for this text buffer.</exception>
        public void TakeThreadOwnership()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Determines whether edit operations on this text buffer are permitted on the calling thread. If <see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.TakeThreadOwnership" /> has
        ///   previously been called, edit operations are permitted only from the same thread that made that call.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the calling thread is allowed to perform edit operations, otherwise <c>false</c>.
        /// </returns>
        public bool CheckEditAccess()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Changes the <see cref = "T:Microsoft.VisualStudio.Utilities.IContentType" /> for this <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />.
        /// </summary>
        /// <param name = "newContentType">The new <see cref = "T:Microsoft.VisualStudio.Utilities.IContentType" />.</param>
        /// <param name = "editTag">An arbitrary object that will be associated with this edit transaction.</param>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "newContentType" /> is null.</exception>
        /// <exception cref = "T:System.InvalidOperationException">Another <see cref = "T:Microsoft.VisualStudio.Text.ITextBufferEdit" /> object is active for this <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />, or 
        ///   <see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.CheckEditAccess" /> would return false.</exception>
        public void ChangeContentType( IContentType newContentType, object editTag )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Inserts the given <paramref name = "text" />at the specified <paramref name = "position" />in the <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />.
        /// </summary>
        /// <param name = "position">The buffer position at which the first character of the text will appear.</param>
        /// <param name = "text">The text to be inserted.</param>
        /// <remarks>
        ///   This is a shortcut for creating a new <see cref = "T:Microsoft.VisualStudio.Text.ITextEdit" /> object, using it to insert the text, and then applying it. If the insertion
        ///   fails on account of a read-only region, the snapshot returned will be the same as the current snapshot of the buffer before
        ///   the attempted insertion.
        /// </remarks>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is less than zero or greater than the length of the buffer.</exception>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "text" /> is null.</exception>
        /// <exception cref = "T:System.InvalidOperationException">A text edit is currently active, or 
        ///   <see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.CheckEditAccess" /> would return false.</exception>
        public ITextSnapshot Insert( int position, string text )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Deletes a sequence of characters from the buffer.
        /// </summary>
        /// <param name = "deleteSpan">The span of characters to delete.</param>
        /// <remarks>
        ///   This is a shortcut for creating a new <see cref = "T:Microsoft.VisualStudio.Text.ITextEdit" /> object, using it to delete the text, and then applying it. If the deletion
        ///   fails on account of a read-only region, the snapshot returned will be the same as the current snapshot of the buffer before
        ///   the attempted deletion.
        /// </remarks>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "deleteSpan" />.End is greater than the length of the buffer.</exception>
        /// <exception cref = "T:System.InvalidOperationException">A TextEdit is currently active, or 
        ///   <see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.CheckEditAccess" /> would return false.</exception>
        public ITextSnapshot Delete( Span deleteSpan )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Replaces a sequence of characters with different text. This is equivalent to first deleting the text to be replaced and then
        ///   inserting the new text.
        /// </summary>
        /// <param name = "replaceSpan">The span of characters to replace.</param>
        /// <param name = "replaceWith">The new text to replace the old.</param>
        /// <remarks>
        ///   This is a shortcut for creating a new <see cref = "T:Microsoft.VisualStudio.Text.ITextEdit" /> object, using it to replace the text, and then applying it. If the replacement
        ///   fails on account of a read-only region, the snapshot returned will be the same as the current snapshot of the buffer before
        ///   the attempted replacement.
        /// </remarks>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "replaceSpan" />.End is greater than the length of the buffer.</exception>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "replaceWith" />is null.</exception>
        /// <exception cref = "T:System.InvalidOperationException">A text edit is currently active, or 
        ///   <see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.CheckEditAccess" /> would return false.</exception>
        public ITextSnapshot Replace( Span replaceSpan, string replaceWith )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Determines whether a text insertion would be prohibited at <paramref name = "position" /> due to an <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegion" />.
        /// </summary>
        /// <param name = "position">The position of the proposed text insertion.</param>
        /// <returns>
        ///   <c>true</c> if an <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegion" /> would prohibit insertions at this position, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is negative or greater than <see cref = "P:Microsoft.VisualStudio.Text.ITextBuffer.CurrentSnapshot" />.Length.</exception>
        /// <exception cref = "T:System.InvalidOperationException"><see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.TakeThreadOwnership" /> has previously been called, and this call is being made
        ///   from a different thread.</exception>
        public bool IsReadOnly( int position )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Determines whether a text insertion would be prohibited at <paramref name = "position" /> due to an <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegion" />.
        /// </summary>
        /// <param name = "position">The position of the proposed text insertion.</param>
        /// <param name = "isEdit"><c>true</c> if this check is part of an edit. <c>false</c> for a query without side effects.</param>
        /// <returns>
        ///   <c>true</c> if an <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegion" /> would prohibit insertions at this position, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "position" /> is negative or greater than <see cref = "P:Microsoft.VisualStudio.Text.ITextBuffer.CurrentSnapshot" />.Length.</exception>
        /// <exception cref = "T:System.InvalidOperationException"><see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.TakeThreadOwnership" /> has previously been called, and this call is being made
        ///   from a different thread.</exception>
        public bool IsReadOnly( int position, bool isEdit )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Determines whether a text modification or deletion would be prohibited at <paramref name = "span" /> due to an <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegion" />
        /// </summary>
        /// <param name = "span">The span to check.</param>
        /// <returns>
        ///   <c>true</c> if the entire span could be deleted or replaced, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "span" /> is null.</exception>
        /// <exception cref = "T:System.ArgumentOutOfRangeException">The <see cref = "P:Microsoft.VisualStudio.Text.Span.End" /> property of <paramref name = "span" /> is greater than <see cref = "P:Microsoft.VisualStudio.Text.ITextBuffer.CurrentSnapshot" />.Length.</exception>
        /// <exception cref = "T:System.InvalidOperationException"><see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.TakeThreadOwnership" /> has previously been called, and this call is being made
        ///   from a different thread.</exception>
        public bool IsReadOnly( Span span )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Determines whether a text modification or deletion would be prohibited at <paramref name = "span" /> due to an <see cref = "T:Microsoft.VisualStudio.Text.IReadOnlyRegion" />
        /// </summary>
        /// <param name = "span">The span to check.</param>
        /// <param name = "isEdit"><c>true</c> if this check is part of an edit. <c>false</c> for a querry without side effects.</param>
        /// <returns>
        ///   <c>true</c> if the entire span could be deleted or replaced, <c>false</c> otherwise.
        /// </returns>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "span" /> is null.</exception>
        /// <exception cref = "T:System.ArgumentOutOfRangeException">The <see cref = "P:Microsoft.VisualStudio.Text.Span.End" /> property of <paramref name = "span" /> is greater than <see cref = "P:Microsoft.VisualStudio.Text.ITextBuffer.CurrentSnapshot" />.Length.</exception>
        /// <exception cref = "T:System.InvalidOperationException"><see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.TakeThreadOwnership" /> has previously been called, and this call is being made
        ///   from a different thread.</exception>
        public bool IsReadOnly( Span span, bool isEdit )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Gets a list of read-only regions that overlap the given span.
        /// </summary>
        /// <param name = "span">The span to check for read-only regions.
        /// </param>
        /// <returns>
        ///   A <see cref = "T:Microsoft.VisualStudio.Text.NormalizedSpanCollection" /> of read-only regions that intersect the given span.
        /// </returns>
        /// <remarks>
        ///   This method returns an empty list if there are no read-only 
        ///   regions intersecting the span, or if the span is zero-length.
        /// </remarks>
        /// <exception cref = "T:System.ArgumentNullException"><paramref name = "span" /> is null.</exception>
        /// <exception cref = "T:System.ArgumentOutOfRangeException"><paramref name = "span" /> is past the end of the buffer.</exception>
        /// <exception cref = "T:System.InvalidOperationException"><see cref = "M:Microsoft.VisualStudio.Text.ITextBuffer.TakeThreadOwnership" /> has previously been called, and this call is being made
        ///   from a different thread.</exception>
        public NormalizedSpanCollection GetReadOnlyExtents( Span span )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   Gets the content type of the text in the buffer.
        /// </summary>
        public IContentType ContentType { get; private set; }

        /// <summary>
        ///   Gets the current content of the buffer.
        /// </summary>
        /// <returns />
        public ITextSnapshot CurrentSnapshot { get; private set; }

        /// <summary>
        ///   Determines whether an edit operation is currently in progress on the <see cref = "T:Microsoft.VisualStudio.Text.ITextBuffer" />.
        /// </summary>
        public bool EditInProgress { get; private set; }

        public event EventHandler<SnapshotSpanEventArgs> ReadOnlyRegionsChanged;
        public event EventHandler<TextContentChangedEventArgs> Changed;
        public event EventHandler<TextContentChangedEventArgs> ChangedLowPriority;
        public event EventHandler<TextContentChangedEventArgs> ChangedHighPriority;
        public event EventHandler<TextContentChangingEventArgs> Changing;
        public event EventHandler PostChanged;
        public event EventHandler<ContentTypeChangedEventArgs> ContentTypeChanged;

        #endregion

        public string Content { get; set; }
    }
}