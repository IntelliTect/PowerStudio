#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public enum TokenClass
    {
        CharacterLiteral,
        Comment,
        ExcludedCode,
        FormalLanguage,
        Identifier,
        Keyword,
        Literal,
        NaturalLanguage,
        NumberLiteral,
        Operator,
        Other,
        PreprocessorKeyword,
        StringLiteral,
        SymbolDefinition,
        SymbolReference,
        WhiteSpace
    }
}