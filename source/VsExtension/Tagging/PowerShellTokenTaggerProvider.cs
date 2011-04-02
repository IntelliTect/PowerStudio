#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using PowerStudio.VsExtension.Parsing;

namespace PowerStudio.VsExtension.Tagging
{
    [Export( typeof (ITaggerProvider) )]
    [ContentType( Configuration.Name )]
    [TagType( typeof (PowerShellTokenTag) )]
    public class PowerShellTokenTaggerProvider : ITaggerProvider
    {
        #region Implementation of ITaggerProvider

        /// <summary>
        /// Creates a tag provider for the specified buffer.
        /// </summary>
        /// <param name="buffer">The <see cref="T:Microsoft.VisualStudio.Text.ITextBuffer"/>.</param><typeparam name="T">The type of the tag.</typeparam>
        public ITagger<T> CreateTagger<T>( ITextBuffer buffer ) where T : ITag
        {
            return (ITagger<T>) new PowerShellTokenTagger();
        }

        #endregion
    }
}