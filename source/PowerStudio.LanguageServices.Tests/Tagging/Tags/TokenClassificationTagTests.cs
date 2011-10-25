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
using Microsoft.VisualStudio.Text.Classification;
using PowerStudio.LanguageServices.Tagging.Tags;
using Moq;
using Xunit;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging.Tags
{
    public class TokenClassificationTagTests
    {
        [Fact]
        public void WhenNullIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>( () => new TokenClassificationTag<object>( null ) );
        }

        [Fact]
        public void WhenAClassificationTypeIsPassedIntoTheCtor_ThenTheCorrespondingPropertyIsSetProperly()
        {
            IClassificationType classificationType = new Mock<IClassificationType>().Object;
            var tag = new TokenClassificationTag<object>( classificationType );
            Assert.Equal( classificationType, tag.ClassificationType );
        }
    }
}