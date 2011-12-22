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
using System.Collections.Generic;
using System.Linq;
using PowerStudio.LanguageServices.Ini.Tagging;

#endregion

namespace PowerStudio.LanguageServices.Ini
{
    public class IniParser
    {
        private const char Underscore = '_';

        public IEnumerable<IniToken> Parse( string text )
        {
            string[] lines = text.Split( new[] { Environment.NewLine }, StringSplitOptions.None );

            for ( int i = 0; i < lines.Length - 1; i++ )
            {
                lines[i] = lines[i] + Environment.NewLine;
            }
            int offset = 0;
            foreach ( string line in lines )
            {
                string local = line;
                IEnumerable<IniToken> tokens = ParseLine( local, offset );
                foreach ( IniToken token in tokens )
                {
                    yield return token;
                }
                offset += local.Length;
            }
        }

        public IEnumerable<IniToken> ParseLine( string text, int offset = 0 )
        {
            bool shouldReadAttributeValue = false;
            bool shouldReadSectionValue = false;
            string value;
            for ( int index = 0; index < text.Length; index += value.Length )
            {
                char current = text[index];
                IniTokenType tokenType = IniTokenType.None;

                if ( DoesStringStartWithNewLine( text, index ) )
                {
                    value = Environment.NewLine;
                    tokenType = IniTokenType.Whitespace;
                }
                else if ( shouldReadAttributeValue )
                {
                    shouldReadAttributeValue = false;
                    value = TakeWhile( text, index, Delimiter.IsNotValue );
                    tokenType = GetAttributeTokenType( value );
                }
                else if ( shouldReadSectionValue )
                {
                    shouldReadSectionValue = false;
                    value = TakeWhile( text, index, Delimiter.IsNotSection );
                    tokenType = IniTokenType.Section;
                }
                else
                {
                    switch ( current )
                    {
                        case Delimiter.CommentStart:
                            int newlineLocation = text.IndexOf( Environment.NewLine, index );
                            value = newlineLocation == -1
                                            ? text.Substring( index )
                                            : text.Substring( index, newlineLocation - index );
                            tokenType = IniTokenType.Comment;
                            break;
                        case Delimiter.Value:
                            shouldReadAttributeValue = true;
                            value = Delimiter.Value.ToString();
                            tokenType = IniTokenType.Delimiter;
                            break;
                        case Delimiter.SectionStart:
                            shouldReadSectionValue = true;
                            value = current.ToString();
                            tokenType = IniTokenType.Delimiter;
                            break;
                        case Delimiter.SectionEnd:
                            value = current.ToString();
                            tokenType = IniTokenType.Delimiter;
                            break;
                        default:
                            if ( char.IsWhiteSpace( current ) )
                            {
                                value = TakeWhile( text, index, char.IsWhiteSpace );
                                tokenType = IniTokenType.Whitespace;
                            }
                            else if ( char.IsLetterOrDigit( current ) )
                            {
                                value = TakeWhile( text, index, ( ch => char.IsLetterOrDigit( ch ) || ch == Underscore ) );
                                tokenType = IniTokenType.Attribute;
                            }
                            else
                            {
                                value = current.ToString();
                            }
                            break;
                    }
                }

                if ( value.Length > 0 )
                {
                    yield return new IniToken { Start = index + offset, Text = value, Type = tokenType };
                }
            }
        }

        public bool DoesStringStartWithNewLine( string text, int i )
        {
            return i < text.Length - 1 &&
                   string.Equals( text.Substring( i, Environment.NewLine.Length ), Environment.NewLine );
        }

        public IniTokenType GetAttributeTokenType( string value )
        {
            IniTokenType tokenType = IniTokenType.AttributeValue;

            if ( NumberIdenifier.IsNumber(value ) )
            {
                tokenType = IniTokenType.AttributeValueNumber;
            }
            else if ( value.Length >= 2 && value[0] == '"' &&
                      value[value.Length - 1] == '"' )
            {
                tokenType = IniTokenType.AttributeValueString;
            }
            return tokenType;
        }

        internal string TakeWhile( string text, int startIndex, Func<char, bool> predicate )
        {
            return new string( text.Skip( startIndex ).TakeWhile( predicate ).ToArray() );
        }

        #region Nested type: Delimiter

        private static class Delimiter
        {
            public const char CommentStart = ';';
            public const char SectionStart = '[';
            public const char SectionEnd = ']';
            public const char Value = '=';
            private static readonly char CarriageReturn = Environment.NewLine[0];

            public static bool IsNotValue( char ch )
            {
                return ch != CommentStart && ch != Value && ch != CarriageReturn;
            }

            public static bool IsNotSection( char ch )
            {
                return IsNotValue( ch ) && ch != SectionEnd && ch != SectionStart;
            }
        }

        #endregion
    }
}