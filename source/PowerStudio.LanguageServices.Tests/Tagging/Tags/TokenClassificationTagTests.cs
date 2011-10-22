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
using Microsoft.VisualStudio.Text.Classification;
using Moq;
using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging.Tags
{
    [TestClass]
    public class TokenClassificationTagTests
    {
        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenNullIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            new TokenClassificationTag<object>( null );
        }

        [TestMethod]
        public void WhenAClassificationTypeIsPassedIntoTheCtor_ThenTheCorrespondingPropertyIsSetProperly()
        {
            IClassificationType classificationType = new Mock<IClassificationType>().Object;
            var tag = new TokenClassificationTag<object>( classificationType );
            Assert.AreEqual( classificationType, tag.ClassificationType );
        }
    }
}