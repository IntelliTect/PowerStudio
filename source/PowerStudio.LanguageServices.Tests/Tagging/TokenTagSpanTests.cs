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
using Microsoft.VisualStudio.Text;
using PowerStudio.LanguageServices.Tagging;
using PowerStudio.LanguageServices.Tagging.Tags;
using Xunit;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging
{
    public class TokenTagSpanTests
    {
        [Fact]
        public void WhenNullIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>( () => new TokenTagSpan<ITokenTag<object>, object>( null ) );
        }

        [Fact]
        public void WhenAClassificationTypeIsPassedIntoTheCtor_ThenTheCorrespondingPropertiesAreSetProperly()
        {
            var tag = new ErrorTokenTag<object>( "Error" ) { Span = new SnapshotSpan() };
            var tagSpan = new TokenTagSpan<ITokenTag<object>, object>( tag );
            Assert.Equal( tag.Span, tagSpan.Span );
            Assert.Equal( tag, tagSpan.Tag );
        }
    }
}