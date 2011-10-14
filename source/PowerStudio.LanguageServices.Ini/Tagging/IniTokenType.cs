#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

namespace PowerStudio.LanguageServices.Ini.Tagging
{
    public enum IniTokenType
    {
        None,
        Whitespace,
        Comment,
        Delimiter,
        Section,
        Attribute,
        AttributeValue,
        AttributeValueString,
        AttributeValueNumber,
    }
}