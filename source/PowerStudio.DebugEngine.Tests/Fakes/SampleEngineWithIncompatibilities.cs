using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PowerStudio.DebugEngine.Attributes;

namespace PowerStudio.DebugEngine.Tests.Fakes
{
    [IncompatibleWithComPlusNativeEngine]
    [IncompatibleWithComPlusNativeEngine(AutoSelect = true)]
    public class SampleEngineWithIncompatibilities
    {
    }
}
