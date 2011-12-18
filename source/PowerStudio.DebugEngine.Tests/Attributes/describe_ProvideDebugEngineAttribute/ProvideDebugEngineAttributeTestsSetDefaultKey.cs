using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace PowerStudio.DebugEngine.Tests.Attributes.describe_ProvideDebugEngineAttribute
{
    [TestClass]
    public class ProvideDebugEngineAttributeTestsSetDefaultKey : ProvideDebugEngineAttributeContext
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
        public void SetDefaultKey_uses_the_default_description_if_no_DescriptionAttribute_is_supplied()
        {
            string description = "default";
            Key.Expect( item => item.SetValue( string.Empty, description ) );
            EngineAttribute.Expect( item => item.SetDefaultKey( null, null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetDefaultKey( Key, ComponentType, description );
        }

        [TestMethod]
        public void SetDefaultKey_uses_the_default_description_if_the_DescriptionAttribute_is_supplied_but_the_description_is_null()
        {
            string defaultDescription = "default";
            string description = null;
            Key.Expect( item => item.SetValue( string.Empty, defaultDescription ) );
            EngineAttribute.Expect( item => item.GetAttribute<DescriptionAttribute>( ComponentType ) )
                    .Return( new DescriptionAttribute( description ) );
            EngineAttribute.Expect( item => item.SetDefaultKey( null, null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetDefaultKey( Key, ComponentType, defaultDescription );
        }

        [TestMethod]
        public void SetDefaultKey_uses_the_default_description_if_the_DescriptionAttribute_is_supplied_but_the_description_is_empty()
        {
            string defaultDescription = "default";
            string description = string.Empty;
            Key.Expect( item => item.SetValue( string.Empty, defaultDescription ) );
            EngineAttribute.Expect( item => item.GetAttribute<DescriptionAttribute>( ComponentType ) )
                    .Return( new DescriptionAttribute( description ) );
            EngineAttribute.Expect( item => item.SetDefaultKey( null, null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetDefaultKey( Key, ComponentType, defaultDescription );
        }

        [TestMethod]
        public void SetDefaultKey_uses_the_DescriptionAttribute_description_if_it_is_supplied_and_not_null_or_empty()
        {
            string defaultDescription = "default";
            string description = "nondefault";
            Key.Expect( item => item.SetValue( string.Empty, description ) );
            EngineAttribute.Expect( item => item.GetAttribute<DescriptionAttribute>( ComponentType ) )
                    .Return( new DescriptionAttribute( description ) );
            EngineAttribute.Expect( item => item.SetDefaultKey( null, null, null ) )
                    .CallOriginalMethod( OriginalCallOptions.NoExpectation );
            EngineAttribute.SetDefaultKey( Key, ComponentType, defaultDescription );
        }
    }
}