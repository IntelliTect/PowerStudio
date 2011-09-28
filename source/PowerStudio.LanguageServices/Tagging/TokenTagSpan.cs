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
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.LanguageServices.Tagging
{
    /// <summary>
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    public class TokenTagSpan<TTokenTag, TToken> : ITagSpan<TTokenTag>
            where TTokenTag : ITokenTag<TToken>
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "TokenTagSpan&lt;T&gt;" /> class.
        /// </summary>
        /// <param name = "tag">The tag.</param>
        public TokenTagSpan( TTokenTag tag )
        {
            if ( ReferenceEquals( tag, null ) )
            {
                throw new ArgumentNullException( "tag" );
            }
            Tag = tag;
        }

        #region Implementation of ITagSpan<out T>

        /// <summary>
        ///   Gets the tag located in this span.
        /// </summary>
        public TTokenTag Tag { get; private set; }

        /// <summary>
        ///   Gets the snapshot span for this tag.
        /// </summary>
        public SnapshotSpan Span
        {
            get { return Tag.Span; }
        }

        #endregion
    }
}