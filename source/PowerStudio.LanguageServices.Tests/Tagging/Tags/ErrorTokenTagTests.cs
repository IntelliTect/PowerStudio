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
using Microsoft.VisualStudio.Text.Adornments;
using PowerStudio.LanguageServices.Tagging.Tags;
using Xunit;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging.Tags
{
    public class ErrorTokenTagTests
    {
        [Fact]
        public void WhenNullToolTipIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>( () => new ErrorTokenTag<object>( null ) );
        }

        [Fact]
        public void WhenAnEmptyStringToolTipIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>( () => new ErrorTokenTag<object>( string.Empty ) );
        }

        [Fact]
        public void WhenNullErrorTypeIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            const string tooltip = "tooltip";
            Assert.Throws<ArgumentNullException>( () => new ErrorTokenTag<object>( tooltip, null ) );
        }

        [Fact]
        public void WhenAnEmptyStringErrorTypeIsPassedIntoTheCtorForToolTip_ThenAnExceptionIsThrown()
        {
            const string tooltip = "tooltip";
            Assert.Throws<ArgumentNullException>( () => new ErrorTokenTag<object>( tooltip, string.Empty ) );
        }

        [Fact]
        public void WhenAnErrorTypeIsPassedIntoTheCtorForToolTip_ThenTheErrorTypePropertyMatches()
        {
            const string expected = PredefinedErrorTypeNames.SyntaxError;
            const string tooltip = "tooltip";
            var tag = new ErrorTokenTag<object>( tooltip, expected );
            Assert.Equal( expected, tag.ErrorType );
        }

        [Fact]
        public void WhenAToolTipIsPassedIntoTheCtorForToolTip_ThenTheToolTipContentMatches()
        {
            const string errorType = PredefinedErrorTypeNames.SyntaxError;
            const string expected = "tooltip";
            var tag = new ErrorTokenTag<object>( expected, errorType );
            Assert.Equal( expected, tag.ToolTipContent );
        }
    }
}