#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Shell;
using PowerStudio.VsExtension.Parsing;
using PowerStudio.VsExtension.Project;

namespace PowerStudio.VsExtension
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    [PackageRegistration( UseManagedResourcesOnly = true )]
    [DefaultRegistryRoot( PsConstants.DefaultRegistryRoot )]
    [ProvideService( typeof (PowerShellLanguageService) )]
    [ProvideLanguageService(typeof(PowerShellLanguageService), LanguageConfiguration.Name, 0,
            CodeSense = true,
            EnableCommenting = true,
            MatchBraces = true,
            ShowCompletion = true,
            ShowMatchingBrace = true,
            AutoOutlining = true,
            EnableAsyncCompletion = true,
            CodeSenseDelay = 0 )]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration( "#110", "#112", "1.0", IconResourceID = 400 )]
    [ProvideObject( typeof (GeneralPropertyPage) )]
    [ProvideProjectFactory( typeof (PowerShellProjectFactory),
            "PowerShell Project",
            "PowerShell Project Files (*.psproj);*.psproj",
            PsConstants.PsProjExtension,
            PsConstants.PsProjExtension,
            @"Templates\Projects",
            LanguageVsTemplate = "PowerShell",
            NewProjectRequireNewFolderVsTemplate = false )]
    [ProvideProjectItem( typeof (PowerShellProjectFactory), "PowerShell", @"Templates\ProjectItems\PsProject", 500 )]
    [Guid( PsConstants.PsProjectPackageGuidString )]
    public sealed class PowerShellPackage : ProjectPackage, IOleComponent
    {
        private uint _ComponentId;

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public PowerShellPackage()
        {
            // proffer the LanguageService
            const bool promote = true;
            ( this as IServiceContainer ).AddService( typeof (PowerShellLanguageService), new PowerShellLanguageService(), promote );
        }

        public override string ProductUserContext
        {
            get { return "PowerShellProj"; }
        }

        #region IOleComponent Members

        public int FContinueMessageLoop(uint uReason, IntPtr pvLoopData, MSG[] pMsgPeeked)
        {
            return 1;
        }

        public int FDoIdle(uint grfidlef)
        {
            return 0;
        }

        public int FPreTranslateMessage(MSG[] pMsg)
        {
            return 0;
        }

        public int FQueryTerminate(int fPromptUser)
        {
            return 1;
        }

        public int FReserved1(uint dwReserved, uint message, IntPtr wParam, IntPtr lParam)
        {
            return 1;
        }

        public IntPtr HwndGetWindow(uint dwWhich, uint dwReserved)
        {
            return IntPtr.Zero;
        }

        public void OnActivationChange(IOleComponent pic,
                                        int fSameComponent,
                                        OLECRINFO[] pcrinfo,
                                        int fHostIsActivating,
                                        OLECHOSTINFO[] pchostinfo,
                                        uint dwReserved)
        {
        }

        public void OnAppActivate(int fActive, uint dwOtherThreadID)
        {
        }

        public void OnEnterState(uint uStateID, int fEnter)
        {
        }

        public void OnLoseActivation()
        {
        }

        public void Terminate()
        {
        }

        #endregion

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            RegisterProjectFactory( new PowerShellProjectFactory( this ) );
        }

        protected override void Dispose( bool disposing )
        {
            try
            {
                if ( _ComponentId != 0 )
                {
                    var mgr = GetService( typeof (SOleComponentManager) ) as IOleComponentManager;
                    if ( mgr != null )
                    {
                        mgr.FRevokeComponent( _ComponentId );
                    }
                    _ComponentId = 0;
                }
            }
            finally
            {
                base.Dispose( disposing );
            }
        }
    }
}