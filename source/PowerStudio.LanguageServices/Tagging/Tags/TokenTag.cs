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

#endregion

namespace PowerStudio.LanguageServices.Tagging.Tags
{
    public class TokenTag<TTokenTag> : ITokenTag<TTokenTag>
    {
        #region ITokenTag Members

        public TTokenTag Token { get; set; }
        public SnapshotSpan Span { get; set; }

        #endregion
    }
}