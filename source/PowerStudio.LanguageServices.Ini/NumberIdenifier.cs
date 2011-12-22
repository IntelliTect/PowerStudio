#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Text.RegularExpressions;

#endregion

namespace PowerStudio.LanguageServices.Ini
{
    public static class NumberIdenifier
    {
        public static readonly Regex FloatingPointNumberRegexWithExponent =
                new Regex( @"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?", RegexOptions.Compiled );

        public static bool IsNumber( string value )
        {
            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }
            bool isMatch = FloatingPointNumberRegexWithExponent.IsMatch( value );
            return isMatch;
        }
    }
}