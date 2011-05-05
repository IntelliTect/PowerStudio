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
using PowerStudio.VsExtension.Tagging.Tags;
using PowerStudio.VsExtension.Tests.Mocks;

#endregion

namespace PowerStudio.VsExtension.Tests.Tagging.Tags
{
    [TestClass]
    public class OutliningTagTests
    {
        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenNullSnapShotIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            var textSnapshot = new TextSnapshotMock( new TextBufferMock( string.Empty ) );
            new OutliningTag( null, new SnapshotSpan( textSnapshot, new Span() ) );
        }

        [TestMethod]
        [ExpectedException( typeof (ArgumentNullException) )]
        public void WhenADefaultSnapshotSpanIsPassedIntoTheCtor_ThenAnExceptionIsThrown()
        {
            var textSnapshot = new TextSnapshotMock( new TextBufferMock( string.Empty ) );
            new OutliningTag( textSnapshot, default( SnapshotSpan ) );
        }
    }
}