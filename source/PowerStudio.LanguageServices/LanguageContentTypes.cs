#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.ComponentModel;

#endregion

namespace PowerStudio.LanguageServices
{
    public enum LanguageContentTypes
    {
        Basic,
        [Description( "C/C++" )]
        CPlusPlus,
        ConsoleOutput,
        CSharp,
        CSS,
        ENC,
        FindResults,
        [Description( "F#" )]
        FSharp,
        HTML,
        JScript,
        XAML,
        XML,
    }
}