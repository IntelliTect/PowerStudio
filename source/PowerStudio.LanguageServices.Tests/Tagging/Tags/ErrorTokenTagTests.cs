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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.Text.Adornments;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging.Tags
{
    [TestClass]
    public class ErrorTokenTagTests
    {
        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenNullToolTipIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            new ErrorTokenTag( null );
        }

        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenAnEmptyStringToolTipIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            new ErrorTokenTag( string.Empty );
        }

        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenNullErrorTypeIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            const string tooltip = "tooltip";
            new ErrorTokenTag( tooltip, null );
        }

        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenAnEmptyStringErrorTypeIsPassedIntoTheCtorForToolTip_ThenAnExceptionIsThrown()
        {
            const string tooltip = "tooltip";
            new ErrorTokenTag( tooltip, string.Empty );
        }

        [TestMethod]
        public void WhenAnErrorTypeIsPassedIntoTheCtorForToolTip_ThenTheErrorTypePropertyMatches()
        {
            const string expected = PredefinedErrorTypeNames.SyntaxError;
            const string tooltip = "tooltip";
            var tag = new ErrorTokenTag( tooltip, expected );
            Assert.AreEqual( expected, tag.ErrorType );
        }

        [TestMethod]
        public void WhenAToolTipIsPassedIntoTheCtorForToolTip_ThenTheToolTipContentMatches()
        {
            const string errorType = PredefinedErrorTypeNames.SyntaxError;
            const string expected = "tooltip";
            var tag = new ErrorTokenTag( expected, errorType );
            Assert.AreEqual( expected, tag.ToolTipContent );
        }
    }
}