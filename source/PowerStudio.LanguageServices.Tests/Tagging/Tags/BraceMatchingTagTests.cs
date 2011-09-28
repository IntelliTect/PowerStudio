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
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerStudio.LanguageServices.Tagging;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging.Tags
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DescribeAttribute : TestCategoryBaseAttribute
    {
        private readonly IList<string> _TestCategories;

        public DescribeAttribute(string testCategory)
        {
            var list = new List<string>(1) { testCategory };
            _TestCategories = list.AsReadOnly();
        }

        public override IList<string> TestCategories { get { return _TestCategories; } }
    }

    [TestClass]
    [Describe("BraceMatchingTag")]
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