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
using PowerStudio.VsExtension.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tests.Tagging
{
    [TestClass]
    public class BraceMatchingTagTests
    {
        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenNullIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            new BraceMatchingTag( null );
        }

        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenAnEmptyStringIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            new BraceMatchingTag( string.Empty );
        }

        [TestMethod]
        public void WhenAValueIsPassedIntoTheCtor_ThenTheTypeParameterMatches()
        {
            const string expected = PredefinedTextMarkerTags.BraceHighlight;
            var tag = new BraceMatchingTag( expected );
            Assert.AreEqual( expected, tag.Type );
        }
    }
}