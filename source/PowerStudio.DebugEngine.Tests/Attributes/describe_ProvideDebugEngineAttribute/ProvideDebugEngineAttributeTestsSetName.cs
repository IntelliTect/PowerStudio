using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace PowerStudio.DebugEngine.Tests.Attributes.describe_ProvideDebugEngineAttribute
{
    [TestClass]
    public class ProvideDebugEngineAttributeTestsSetName : ProvideDebugEngineAttributeContext
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            Initialize();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Cleanup();
        }

        [TestMethod]
        public void SetName_does_not_call_SetValue_if_no_DisplayNameAttribute_is_supplied()
        {
            EngineAttribute.Expect( item => item.SetName( null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetName( Key, ComponentType );
        }

        [TestMethod]
        public void SetName_does_not_call_SetValue_if_the_DisplayNameAttribute_is_supplied_but_the_DisplayName_is_null()
        {
            string description = null;
            EngineAttribute.Expect( item => item.GetAttribute<DisplayNameAttribute>( ComponentType ) )
                    .Return( new DisplayNameAttribute( description ) );
            EngineAttribute.Expect( item => item.SetName( null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetName( Key, ComponentType );
        }

        [TestMethod]
        public void SetName_does_not_call_SetValue_if_the_DisplayNameAttribute_is_supplied_but_the_DisplayName_is_empty()
        {
            string description = string.Empty;
            EngineAttribute.Expect( item => item.GetAttribute<DisplayNameAttribute>( ComponentType ) )
                    .Return( new DisplayNameAttribute( description ) );
            EngineAttribute.Expect( item => item.SetName( null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetName( Key, ComponentType );
        }

        [TestMethod]
        public void SetName_uses_the_DisplayNameAttribute_DisplayName_if_it_is_supplied_and_not_null_or_empty()
        {
            string description = "nondefault";
            Key.Expect( item => item.SetValue( "Name", description ) );
            EngineAttribute.Expect( item => item.GetAttribute<DisplayNameAttribute>( ComponentType ) )
                    .Return( new DisplayNameAttribute( description ) );
            EngineAttribute.Expect( item => item.SetName( null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetName( Key, ComponentType );
        }
    }
}