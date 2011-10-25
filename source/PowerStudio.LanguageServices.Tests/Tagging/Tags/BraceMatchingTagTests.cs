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
using PowerStudio.LanguageServices.Tagging;
using PowerStudio.LanguageServices.Tagging.Tags;
using Xunit;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging.Tags
{
    public class BraceMatchingTagTests
    {
        [Fact]
        public void WhenNullIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>( () => new BraceMatchingTag<object>( null ) );
        }

        [Fact]
        public void WhenAnEmptyStringIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>( () => new BraceMatchingTag<object>( string.Empty ) );
        }

        [Fact]
        public void WhenAValueIsPassedIntoTheCtor_ThenTheTypeParameterMatches()
        {
            const string expected = PredefinedTextMarkerTags.BraceHighlight;
            var tag = new BraceMatchingTag<object>( expected );
            Assert.Equal( expected, tag.Type );
        }
    }
}