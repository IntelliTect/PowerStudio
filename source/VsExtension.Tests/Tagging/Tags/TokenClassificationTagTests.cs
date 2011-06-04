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
using PowerStudio.LanguageService.Tagging.Tags;
using Rhino.Mocks;

#endregion

namespace PowerStudio.VsExtension.Tests.Tagging.Tags
{
    [TestClass]
    public class TokenClassificationTagTests
    {
        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenNullIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            new TokenClassificationTag( null );
        }

        [TestMethod]
        public void WhenAClassificationTypeIsPassedIntoTheCtor_ThenTheCorrespondingPropertyIsSetProperly()
        {
            var classificationType = MockRepository.GenerateMock<IClassificationType>();
            var tag = new TokenClassificationTag( classificationType );
            Assert.AreEqual( classificationType, tag.ClassificationType );
        }
    }
}