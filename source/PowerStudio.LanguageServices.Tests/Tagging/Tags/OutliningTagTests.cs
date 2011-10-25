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
using PowerStudio.LanguageServices.Tagging.Tags;
using PowerStudio.LanguageServices.Tests.Mocks;
using Xunit;

#endregion

namespace PowerStudio.LanguageServices.Tests.Tagging.Tags
{
    public class OutliningTagTests
    {
        [Fact]
        public void WhenNullSnapShotIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            var textSnapshot = new TextSnapshotMock( new TextBufferMock( string.Empty ) );
            Assert.Throws<ArgumentNullException>(
                    () => new OutliningTag<object>( null, new SnapshotSpan( textSnapshot, new Span() ) )
                    );
        }

        [Fact]
        public void WhenADefaultSnapshotSpanIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            var textSnapshot = new TextSnapshotMock( new TextBufferMock( string.Empty ) );
            Assert.Throws<ArgumentNullException>(
                    () => new OutliningTag<object>( textSnapshot, default( SnapshotSpan ) )
                    );
        }
    }
}