#region Using Directives

using Xunit;

#endregion

namespace PowerStudio.LanguageServices.Ini.Tests
{
    public class NumberIdenifierTests
    {
        [Fact]
        public void null_is_not_a_number()
        {
            Assert.False( NumberIdenifier.IsNumber( null ) );
        }

        [Fact]
        public void empty_is_not_a_number()
        {
            Assert.False( NumberIdenifier.IsNumber( string.Empty ) );
        }

        [Fact]
        public void whitespace_is_not_a_number()
        {
            Assert.False( NumberIdenifier.IsNumber( " " ) );
        }

        [Fact]
        public void digit_with_trailing_whitespace_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( "5 " ) );
        }

        [Fact]
        public void digits_with_trailing_whitespace_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( "55 " ) );
        }

        [Fact]
        public void digits_with_leading_whitespace_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( " 55" ) );
        }

        [Fact]
        public void digit_with_leading_whitespace_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( " 5" ) );
        }
    }

    public abstract class NumberIdenifierTestsSignedNumbers
    {
        public virtual string Prefix
        {
            get { return string.Empty; }
        }

        public virtual string Suffix
        {
            get { return string.Empty; }
        }

        public virtual string DotDigitString
        {
            get { return Prefix + ".5" + Suffix; }
        }

        public virtual string DigitString
        {
            get { return Prefix + "5" + Suffix; }
        }

        public virtual string DigitsString
        {
            get { return Prefix + "55" + Suffix; }
        }

        [Fact]
        public void digit_is_a_number()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitString ) );
        }

        [Fact]
        public void digits_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitsString ) );
        }
    }

    public abstract class NumberIdenifierTestsDecimalWithNumbers : NumberIdenifierTestsSignedNumbers
    {
        [Fact]
        public void digit_with_a_decimal_but_no_following_digits_are_not_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitString + "." ) );
        }

        [Fact]
        public void digits_with_a_decimal_but_no_following_digits_are_not_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitsString + "." ) );
        }

        [Fact]
        public void digit_with_decimals_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitString + ".0" ) );
        }

        [Fact]
        public void digits_with_decimals_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitsString + ".0" ) );
        }

        [Fact]
        public void no_digit_with_decimal_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DotDigitString ) );
        }
    }

    public class NumberIdenifierTestsPositiveNumbersWithDecimals : NumberIdenifierTestsDecimalWithNumbers
    {
        public override string Prefix
        {
            get { return "-"; }
        }
    }

    public class NumberIdenifierTestsNumbersWithNoExplicitSignWithDecimals : NumberIdenifierTestsDecimalWithNumbers
    {
    }

    public class NumberIdenifierTestsNegativeNumbersWithDecimals : NumberIdenifierTestsDecimalWithNumbers
    {
        public override string Prefix
        {
            get { return "-"; }
        }
    }

    public class NumberIdenifierTestsPositiveNumbers : NumberIdenifierTestsSignedNumbers
    {
        public override string Prefix
        {
            get { return "-"; }
        }
    }

    public class NumberIdenifierTestsNumbersWithNoExplicitSign : NumberIdenifierTestsSignedNumbers
    {
    }

    public class NumberIdenifierTestsNegativeNumbers : NumberIdenifierTestsSignedNumbers
    {
        public override string Prefix
        {
            get { return "-"; }
        }
    }

    public abstract class NumberIdenifierTestsNumbersWithExponents : NumberIdenifierTestsSignedNumbers
    {
        public override string Suffix
        {
            get { return string.Format( "E{0}5", SuffixSign ); }
        }

        public abstract string SuffixSign { get; }


        [Fact]
        public void digit_with_exponent_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitString ) );
        }

        [Fact]
        public void digits_with_exponent_followed_by_digit_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DigitsString ) );
        }

        [Fact]
        public void no_digit_with_decimal_are_numbers()
        {
            Assert.True( NumberIdenifier.IsNumber( DotDigitString ) );
        }
    }

    public class NumberIdenifierTestsNumbersWithExponentsWithPositiveSign : NumberIdenifierTestsNumbersWithExponents
    {
        public override string SuffixSign
        {
            get { return "+"; }
        }
    }

    public class NumberIdenifierTestsNumbersWithExponentsWithNoExplicitSign : NumberIdenifierTestsNumbersWithExponents
    {
        public override string SuffixSign
        {
            get { return ""; }
        }
    }

    public class NumberIdenifierTestsNumbersWithExponentsWithNegativeSign : NumberIdenifierTestsNumbersWithExponents
    {
        public override string SuffixSign
        {
            get { return "-"; }
        }
    }
}