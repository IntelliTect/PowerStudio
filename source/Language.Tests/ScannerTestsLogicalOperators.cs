using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PowerStudio.Language.Tests
{
    [TestClass]
    public class ScannerTestsLogicalOperators : ScannerTestContext
    {
        [TestMethod]
        public void LoneAmpAmpIsIdentified()
        {
            Scanner scanner = new Scanner("&&");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.AMPAMP, token);
            Assert.AreEqual("&&", scanner.yytext);
        }

        [TestMethod]
        public void LoneBarBarIsIdentified()
        {
            Scanner scanner = new Scanner("||");
            int result = scanner.yylex();
            Tokens token = (Tokens)result;
            Assert.AreEqual(Tokens.BARBAR, token);
            Assert.AreEqual("||", scanner.yytext);
        }
    }
}