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
using System.IO;
using System.Linq;
using System.Management.Automation;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using PowerStudio.Resources;

#endregion

namespace PowerStudio.LanguageService.Project.Build
{
    public class SignScripts : Task
    {
        [Required]
        public ITaskItem Certificate { get; set; }

        [Required]
        public ITaskItem[] Scripts { get; set; }

        /// <summary>
        /// When overridden in a derived class, executes the task.
        /// </summary>
        /// <returns>
        /// true if the task successfully executed; otherwise, false.
        /// </returns>
        public override bool Execute()
        {
            if ( !File.Exists( Certificate.ItemSpec ) )
            {
                Log.LogError( Errors.FailedToFindSigningCertificate0, Certificate.ItemSpec );
                return false;
            }

            try
            {
                using ( PowerShell powerShell = PowerShell.Create() )
                {
                    powerShell.AddCommand( "Set-AuthenticodeSignature" );
                    powerShell.AddParameter( "Cert", Certificate.ItemSpec );
                    powerShell.AddParameter( "Force" );
                    powerShell.Invoke( Scripts.Select( item => item.ItemSpec ) );
                }
            }
            catch ( Exception ex )
            {
                Log.LogErrorFromException( ex );
                return false;
            }

            return true;
        }
    }
}