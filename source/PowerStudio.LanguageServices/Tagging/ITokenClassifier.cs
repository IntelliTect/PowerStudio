﻿#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Text.Classification;

#endregion

namespace PowerStudio.LanguageServices.Tagging
{
    /// <summary>
    /// </summary>
    public interface ITokenClassifier<TToken>
    {
        /// <summary>
        ///   Gets the <see cref = "Microsoft.VisualStudio.Text.Classification.IClassificationType" /> with the specified token type.
        /// </summary>
        IClassificationType this[ TToken token ] { get; }
    }
}