using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PowerStudio.Language.Tests
{

[TestClass]
    public class ScannerTestsComparisonKeywords : ScannerTestContext
    {
    private static readonly string[] Keywords = new[]
                                               {
                                                   "Begin",
                                                   "Break",
                                                   "Catch",
                                                   "Continue",
                                                   "Data",
                                                   "Do",
                                                   "Else",
                                                   "Elseif",
                                                   "End",
                                                   "Exit",
                                                   "Filter",
                                                   "Finally",
                                                   "For",
                                                   "Function",
                                                   "If",
                                                   "In",
                                                   "Param",
                                                   "Process",
                                                   "Return",
                                                   "Switch",
                                                   "Throw",
                                                   "Try",
                                                   "Trap",
                                                   "While",
                                                   "Until"
                                               };

    /// <summary>
    /// This takes the place of over 1000 individual unit tests.
    /// </summary>
    [TestMethod]
    public void TokensInAllCasePermutationsAreIdentified()
    {
        foreach (string keyword in Keywords)
        {
            foreach (string variant in Permute(keyword))
            {
                var scanner = new Scanner(variant);
                int result = scanner.yylex();
                var token = (Tokens)result;
                Tokens expected;
                Enum.TryParse(keyword, true, out expected);
                Assert.AreEqual(expected, token);

                Assert.AreEqual(variant, scanner.yytext);
            }
        }
    }

    private static IEnumerable<string> Permute(string current)
    {
        if (current.Length == 1)
        {
            var source = new[] { Char.ToUpper(current[0]).ToString(), Char.ToLower(current[0]).ToString() };
            return source;
        }

        IEnumerable<string> permutations = Permute(current.Substring(1));
        IEnumerable<string> lowerP = permutations.Select(p => Char.ToLower(current[0]) + p);
        IEnumerable<string> upperP = permutations.Select(p => Char.ToUpper(current[0]) + p);
        return upperP.Union(lowerP);
    }
}
    [TestClass]
    public class ScannerTestsComparisonOperators : ScannerTestContext
    {
        private static readonly string[] ComparisonOperators = new[]
                                                               {
                                                                       "COEQ",
                                                                       "CONE",
                                                                       "COGE",
                                                                       "COGT",
                                                                       "COLT",
                                                                       "COLE",
                                                                       "COIEQ",
                                                                       "COINE",
                                                                       "COIGE",
                                                                       "COIGT",
                                                                       "COILT",
                                                                       "COILE",
                                                                       "COCEQ",
                                                                       "COCNE",
                                                                       "COCGE",
                                                                       "COCGT",
                                                                       "COCLT",
                                                                       "COCLE",
                                                                       "COLIKE",
                                                                       "CONOTLIKE",
                                                                       "COMATCH",
                                                                       "CONOTMATCH",
                                                                       "COILIKE",
                                                                       "COINOTLIKE",
                                                                       "COIMATCH",
                                                                       "COINOTMATCH",
                                                                       "COCLIKE",
                                                                       "COCNOTLIKE",
                                                                       "COCMATCH",
                                                                       "COCNOTMATCH",
                                                                       "COCONTAINS",
                                                                       "CONOTCONTAINS",
                                                                       "COICONTAINS",
                                                                       "COINOTCONTAINS",
                                                                       "COCCONTAINS",
                                                                       "COCNOTCONTAINS",
                                                                       "COISNOT",
                                                                       "COIS",
                                                                       "COAS",
                                                                       "COREPLACE",
                                                                       "COIREPLACE",
                                                                       "COCREPLACE"
                                                               };

        /// <summary>
        /// This takes the place of over 1000 individual unit tests.
        /// </summary>
        [TestMethod]
        public void TokensInAllCasePermutationsAreIdentified()
        {
            foreach ( string copToken in ComparisonOperators )
            {
                string copText = copToken.Substring( 2 );
                foreach ( string copVariant in Permute( copText ) )
                {
                    string text = "-" + copVariant;
                    var scanner = new Scanner( text );
                    int result = scanner.yylex();
                    var token = (Tokens) result;
                    Tokens expected;
                    Enum.TryParse( copToken, out expected );
                    Assert.AreEqual( expected, token );

                    Assert.AreEqual( text, scanner.yytext );
                }
            }
        }

        private static IEnumerable<string> Permute( string current )
        {
            if ( current.Length == 1 )
            {
                var source = new[] { Char.ToUpper( current[0] ).ToString(), Char.ToLower( current[0] ).ToString() };
                return source;
            }

            IEnumerable<string> permutations = Permute( current.Substring( 1 ) );
            IEnumerable<string> lowerP = permutations.Select( p => Char.ToLower( current[0] ) + p );
            IEnumerable<string> upperP = permutations.Select( p => Char.ToUpper( current[0] ) + p );
            return upperP.Union( lowerP );
        }
    }
}