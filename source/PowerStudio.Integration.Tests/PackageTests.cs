#region Using Directives

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VsSDK.UnitTestLibrary;
using Xunit;

#endregion

namespace PowerStudio.Integration.Tests
{
    public class PackageTests
    {
        [Fact]
        public void CreateInstance()
        {
            var package = new PowerShellPackage();
        }

        [Fact]
        public void IsIVsPackage()
        {
            var package = new PowerShellPackage();
            var ivsPackage = package as IVsPackage;
            Assert.NotNull( ivsPackage );
        }

        [Fact( Skip = "Not Implemented" )]
        public void SetSite()
        {
            // Create the package
            IVsPackage package = new PowerShellPackage();
            Assert.NotNull( package );

            // Create a basic service provider
            OleServiceProvider serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

            // Site the package
            Assert.Equal( VSConstants.S_OK, package.SetSite( serviceProvider ) );

            // Unsite the package
            Assert.Equal( VSConstants.S_OK, package.SetSite( null ) );
        }
    }
}