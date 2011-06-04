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
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Project;

#endregion

namespace PowerStudio.LanguageService
{
    public abstract class PowerShellPackageBase : ProjectPackage, IOleComponent
    {
        #region Implementation of IOleComponent

        /// <param name="dwReserved"/><param name="message"/><param name="wParam"/><param name="lParam"/>
        public abstract int FReserved1( uint dwReserved, uint message, IntPtr wParam, IntPtr lParam );

        /// <param name="pMsg"/>
        public abstract int FPreTranslateMessage( MSG[] pMsg );

        /// <param name="uStateID"/><param name="fEnter"/>
        public abstract void OnEnterState( uint uStateID, int fEnter );

        /// <param name="fActive"/><param name="dwOtherThreadID"/>
        public abstract void OnAppActivate( int fActive, uint dwOtherThreadID );

        public abstract void OnLoseActivation();

        /// <param name="pic"/><param name="fSameComponent"/><param name="pcrinfo"/><param name="fHostIsActivating"/><param name="pchostinfo"/><param name="dwReserved"/>
        public abstract void OnActivationChange( IOleComponent pic,
                                                 int fSameComponent,
                                                 OLECRINFO[] pcrinfo,
                                                 int fHostIsActivating,
                                                 OLECHOSTINFO[] pchostinfo,
                                                 uint dwReserved );

        /// <param name="grfidlef"/>
        public abstract int FDoIdle( uint grfidlef );

        /// <param name="uReason"/><param name="pvLoopData"/><param name="pMsgPeeked"/>
        public abstract int FContinueMessageLoop( uint uReason, IntPtr pvLoopData, MSG[] pMsgPeeked );

        /// <param name="fPromptUser"/>
        public abstract int FQueryTerminate( int fPromptUser );

        public abstract void Terminate();

        /// <param name="dwWhich"/><param name="dwReserved"/>
        public abstract IntPtr HwndGetWindow( uint dwWhich, uint dwReserved );

        #endregion
    }
}