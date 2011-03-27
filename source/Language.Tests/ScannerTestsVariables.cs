using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PowerStudio.Language.Tests
{
    [TestClass]
    public class ScannerTestsVariables : ScannerTestContext
    {
        [TestMethod]
        public void SimpleVariableNamesAreIdentified()
        {
            Scanner scanner = new Scanner("$value");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.VARIABLE, token);
            Assert.AreEqual("$value", scanner.yytext);
        }

        [TestMethod]
        public void SimpleVariableNamesWithinCurlyBracesAreIdentified()
        {
            Scanner scanner = new Scanner("${value}");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.VARIABLE, token);
            Assert.AreEqual("${value}", scanner.yytext);
        }

        [TestMethod]
        public void UnicodeVariableNamesAreIdentified()
        {
            Scanner scanner = new Scanner("$ᚠᛇᚻ");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.VARIABLE, token);
            Assert.AreEqual("$ᚠᛇᚻ", scanner.yytext);
        }

        [TestMethod]
        public void UnicodeVariableNamesWithinCurlyBracesAreIdentified()
        {
            Scanner scanner = new Scanner("${ᚠᛇᚻ}");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.VARIABLE, token);
            Assert.AreEqual("${ᚠᛇᚻ}", scanner.yytext);
        }
    }
}