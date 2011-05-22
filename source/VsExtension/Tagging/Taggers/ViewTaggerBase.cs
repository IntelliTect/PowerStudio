#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

#endregion

namespace PowerStudio.VsExtension.Tagging.Taggers
{
    public abstract class ViewTaggerBase<T> : TaggerBase<T>
            where T : ITokenTag
    {
        protected ViewTaggerBase( ITextView view, ITextBuffer buffer )
                : base( buffer )
        {
            View = view;
            View.Caret.PositionChanged += CaretPositionChanged;
            View.LayoutChanged += ViewLayoutChanged;
        }

        protected virtual ITextView View { get; set; }
        protected virtual SnapshotPoint? CurrentChar { get; set; }

        protected virtual void ViewLayoutChanged( object sender, TextViewLayoutChangedEventArgs e )
        {
            // Make sure that there has really been a change and that a new snapshot was generated
            if ( e.NewSnapshot !=
                 e.OldSnapshot )
            {
                UpdateAtCaretPosition( View.Caret.Position );
            }
        }

        protected virtual void CaretPositionChanged( object sender, CaretPositionChangedEventArgs e )
        {
            UpdateAtCaretPosition( e.NewPosition );
        }

        protected virtual void UpdateAtCaretPosition( CaretPosition caretPosition )
        {
            CurrentChar = caretPosition.Point.GetPoint( Buffer, caretPosition.Affinity );
        }
    }
}