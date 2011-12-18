using System;
using System.Runtime.InteropServices;
using PowerStudio.DebugEngine.Attributes;
using Microsoft.VisualStudio.Shell;
using Rhino.Mocks;

namespace PowerStudio.DebugEngine.Tests.Attributes
{
    public abstract class ProvideDebugEngineAttributeContext
    {
        protected const string GuidString = "44444444-4444-4444-4444-444444444444";
        protected RegistrationAttribute.RegistrationContext Context { get; private set; }
        protected ProvideDebugEngineAttribute EngineAttribute { get; private set; }
        protected RegistrationAttribute.Key Key { get; private set; }
        protected Type ComponentType { get; private set; }

        public virtual void Initialize()
        {
            ComponentType = typeof (DebugEngineUnderTest);
            Key = MockRepository.GenerateMock<RegistrationAttribute.Key>();
            EngineAttribute = MockRepository.GenerateMock<ProvideDebugEngineAttribute>(ComponentType);
            Context = MockRepository.GenerateMock<RegistrationAttribute.RegistrationContext>();
        }

        public virtual void Cleanup()
        {
            Key.VerifyAllExpectations();
            EngineAttribute.VerifyAllExpectations();
            Context.VerifyAllExpectations();
        }

        #region Nested type: DebugEngineUnderTest

        [Guid( GuidString )]
        public class DebugEngineUnderTest
        {
        }

        #endregion
    }
}