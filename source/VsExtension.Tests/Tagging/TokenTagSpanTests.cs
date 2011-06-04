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
using Microsoft.VisualStudio.Text;
using PowerStudio.LanguageService.Tagging;
using PowerStudio.LanguageService.Tagging.Tags;

#endregion

namespace PowerStudio.VsExtension.Tests.Tagging
{
    [TestClass]
    public class TokenTagSpanTests
    {
        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenNullIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            new TokenTagSpan<ITokenTag>( null );
        }

        [TestMethod]
        public void WhenAClassificationTypeIsPassedIntoTheCtor_ThenTheCorrespondingPropertiesAreSetProperly()
        {
            var tag = new ErrorTokenTag( "Error" ) { Span = new SnapshotSpan() };
            var tagSpan = new TokenTagSpan<ITokenTag>( tag );
            Assert.AreEqual( tag.Span, tagSpan.Span );
            Assert.AreEqual( tag, tagSpan.Tag );
        }
    }
}