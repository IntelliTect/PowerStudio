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

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellTagger
    {
        protected ITextBuffer Buffer { get; private set; }

        public PowerShellTagger( ITextBuffer buffer )
        {
            Buffer = buffer;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}