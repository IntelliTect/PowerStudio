#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using PowerStudio.LanguageServices.Ini.Tagging;
using Xunit;

#endregion

namespace PowerStudio.LanguageServices.Ini.Tests
{
    public class IniParserTests
    {
        private readonly IniParser Parser = new IniParser();

        [Fact]
        public void single_line_comment_returns_a_single_comment_token()
        {
            string text = ";TheDudeAbides";
            IEnumerable<IniToken> results = Parser.Parse( text );
            Assert.Equal( 1, results.Count() );
            IniToken actual = results.First();
            Assert.Equal( IniTokenType.Comment, actual.Type );
            Assert.Equal( text, actual.Text );
            Assert.Equal( 0, actual.Start );
        }

        [Fact]
        public void single_line_comment_with_a_newline_returns_a_comment_token_and_whitespace_token()
        {
            string text = ";TheDudeAbides" + Environment.NewLine;
            IEnumerable<IniToken> results = Parser.Parse( text );
            Assert.Equal( 2, results.Count() );
            IniToken comment = results.First();
            Assert.Equal( IniTokenType.Comment, comment.Type );
            IniToken newline = results.Last();
            Assert.Equal( IniTokenType.Whitespace, newline.Type );
        }
    }
}