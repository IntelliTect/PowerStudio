using System;
using EnvDTE;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VsSDK.IntegrationTestLibrary;
using Microsoft.VSSDK.Tools.VsIdeTesting;

namespace PowerShell.LanguageService_IntegrationTests.IntegrationTests
{
    [TestClass]
    public class VisualBasicProjectTests
    {
        #region fields

        private delegate void ThreadInvoker();

        #endregion

        #region properties

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region ctors

        #endregion

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [HostType( "VS IDE" )]
        [TestMethod]
        public void VBWinformsApplication()
        {
            UIThreadInvoker.Invoke( (ThreadInvoker) delegate
                                                    {
                                                        //Solution and project creation parameters
                                                        string solutionName = "VBWinApp";
                                                        string projectName = "VBWinApp";

                                                        //Template parameters
                                                        string language = "VisualBasic";
                                                        string projectTemplateName = "WindowsApplication.Zip";
                                                        string itemTemplateName = "CodeFile.zip";
                                                        string newFileName = "Test.vb";

                                                        var dte =
                                                                (DTE)
                                                                VsIdeTestHostContext.ServiceProvider.GetService(
                                                                        typeof (DTE) );

                                                        var testUtils = new TestUtils();

                                                        testUtils.CreateEmptySolution( TestContext.TestDir, solutionName );
                                                        Assert.AreEqual( 0, testUtils.ProjectCount() );

                                                        //Add new  Windows application project to existing solution
                                                        testUtils.CreateProjectFromTemplate( projectName,
                                                                                             projectTemplateName,
                                                                                             language,
                                                                                             false );

                                                        //Verify that the new project has been added to the solution
                                                        Assert.AreEqual( 1, testUtils.ProjectCount() );

                                                        //Get the project
                                                        Project project = dte.Solution.Item( 1 );
                                                        Assert.IsNotNull( project );
                                                        Assert.IsTrue(
                                                                string.Compare( project.Name,
                                                                                projectName,
                                                                                StringComparison.
                                                                                        InvariantCultureIgnoreCase ) ==
                                                                0 );

                                                        //Verify Adding new code file to project
                                                        ProjectItem newCodeFileItem =
                                                                testUtils.AddNewItemFromVsTemplate(
                                                                        project.ProjectItems,
                                                                        itemTemplateName,
                                                                        language,
                                                                        newFileName );
                                                        Assert.IsNotNull( newCodeFileItem,
                                                                          "Could not create new project item" );
                                                    } );
        }
    }
}