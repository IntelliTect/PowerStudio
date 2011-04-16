using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.IntegrationTestLibrary;
using Microsoft.VSSDK.Tools.VsIdeTesting;

namespace PowerShell.LanguageService_IntegrationTests.IntegrationTests
{
    [TestClass]
    public class SolutionTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        [TestMethod]
        [HostType( "VS IDE" )]
        public void AnEmptySolutionCanBeCreated()
        {
            UIThreadInvoker.Invoke( (Action) CreateEmptySolution );
        }

        public void CreateEmptySolution()
        {
            var testUtils = new TestUtils();
            testUtils.CloseCurrentSolution(
                    __VSSLNSAVEOPTIONS.SLNSAVEOPT_NoSave );
            testUtils.CreateEmptySolution( TestContext.TestDir, "EmptySolution" );
        }
    }
}