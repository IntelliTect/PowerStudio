#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Management.Automation;
using Microsoft.VisualStudio.Text;

#endregion

namespace PowerStudio.VsExtension.Tagging.Tags
{
    public class TokenTag : ITokenTag
    {
        #region ITokenTag Members

        public PSToken Token { get; set; }
        public SnapshotSpan Span { get; set; }

        #endregion
    }
}