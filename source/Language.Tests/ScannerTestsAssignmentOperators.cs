using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PowerStudio.Language.Tests
{
    [TestClass]
    public class ScannerTestsAssignmentOperators : ScannerTestContext
    {
        [TestMethod]
        public void LoneAssignmentOperatorIsIdentified()
        {
            var scanner = new Scanner( "=" );
            int result = scanner.yylex();
            var token = (Tokens) result;
            Assert.AreEqual( Tokens.ASSIGNMENTOPERATOR, token );
            Assert.AreEqual( "=", scanner.yytext );
        }

        [TestMethod]
        public void DoubleAssignmentOperatorIsIdentifiedAsTwoSeparateAssignmentOperators()
        {
            var scanner = new Scanner("==");
            int result = scanner.yylex();
            var token = (Tokens)result;
            Assert.AreEqual(Tokens.ASSIGNMENTOPERATOR, token);
            Assert.AreEqual("=", scanner.yytext);

            result = scanner.yylex();
            token = (Tokens)result;
            Assert.AreEqual(Tokens.ASSIGNMENTOPERATOR, token);
            Assert.AreEqual("=", scanner.yytext);
        }

        [TestMethod]
        public void LoneDivAssignmentOperatorIsIdentified()
        {
            var scanner = new Scanner( "/=" );
            int result = scanner.yylex();
            var token = (Tokens) result;
            Assert.AreEqual( Tokens.ASSIGNMENTOPERATOR, token );
            Assert.AreEqual( "/=", scanner.yytext );
        }

        [TestMethod]
        public void LoneMultAssignmentOperatorIsIdentified()
        {
            var scanner = new Scanner( "*=" );
            int result = scanner.yylex();
            var token = (Tokens) result;
            Assert.AreEqual( Tokens.ASSIGNMENTOPERATOR, token );
            Assert.AreEqual( "*=", scanner.yytext );
        }

        [TestMethod]
        public void LonePlusAssignmentOperatorIsIdentified()
        {
            var scanner = new Scanner( "+=" );
            int result = scanner.yylex();
            var token = (Tokens) result;
            Assert.AreEqual( Tokens.ASSIGNMENTOPERATOR, token );
            Assert.AreEqual( "+=", scanner.yytext );
        }

        [TestMethod]
        public void LoneMinusAssignmentOperatorIsIdentified()
        {
            var scanner = new Scanner( "-=" );
            int result = scanner.yylex();
            var token = (Tokens) result;
            Assert.AreEqual( Tokens.ASSIGNMENTOPERATOR, token );
            Assert.AreEqual( "-=", scanner.yytext );
        }

        [TestMethod]
        public void LoneModAssignmentOperatorIsIdentified()
        {
            var scanner = new Scanner( "%=" );
            int result = scanner.yylex();
            var token = (Tokens) result;
            Assert.AreEqual( Tokens.ASSIGNMENTOPERATOR, token );
            Assert.AreEqual( "%=", scanner.yytext );
        }
    }
}