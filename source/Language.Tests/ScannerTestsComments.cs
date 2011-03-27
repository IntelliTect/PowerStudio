using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PowerStudio.Language.Tests
{
    [TestClass]
    public class ScannerTestsComments : ScannerTestContext
    {
        [TestMethod]
        public void SingleLineCommentsAtTheStartOfALineAreIdentified()
        {
            Scanner scanner = new Scanner("#comment");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.COMMENT, token);
            Assert.AreEqual("#comment", scanner.yytext);
        }

        [TestMethod]
        public void SingleLineCommentsAtTheEndOfALineAreItentified()
        {
            Scanner scanner = new Scanner("$value#comment");
            int result = scanner.yylex();
            result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.COMMENT, token);
            Assert.AreEqual("#comment", scanner.yytext);
        }

        [TestMethod]
        public void BlockCommentsOverMultipleLinesAreIdentified()
        {
            string[] lines = new[]
                             {
                                     "/*" + Environment.NewLine,
                                     "comment" + Environment.NewLine,
                                     "*/",
                             };
            
            Scanner scanner = new Scanner(lines);
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.COMMENT, token);
            string text = scanner.yytext;

            result = scanner.yylex();
            token = (Tokens)result;
            Assert.AreEqual(Tokens.COMMENT, token);
            text += scanner.yytext;

            result = scanner.yylex();
            token = (Tokens)result;
            Assert.AreEqual(Tokens.COMMENT, token);
            text += scanner.yytext;

            Assert.AreEqual(string.Join("", lines), text);
        }

        [TestMethod]
        public void BlockCommentsAtTheStartOfALineAreIdentified()
        {
            Scanner scanner = new Scanner("/*comment*/");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.COMMENT, token);
            Assert.AreEqual("/*comment*/", scanner.yytext);
        }

        [TestMethod]
        public void BlockCommentsAtTheEndOfALineAreItentified()
        {
            Scanner scanner = new Scanner("$value/*comment*/");
            int result = scanner.yylex();
            result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.COMMENT, token);
            Assert.AreEqual("/*comment*/", scanner.yytext);
        }
    }
}