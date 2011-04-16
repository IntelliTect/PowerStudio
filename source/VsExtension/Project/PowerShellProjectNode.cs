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
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Project;
using Microsoft.VisualStudio.Project.Automation;
using VSLangProj;

#endregion

namespace PowerStudio.VsExtension.Project
{
    [Guid( PsConstants.ProjectNodeGuid )]
    public class PowerShellProjectNode : ProjectNode
    {
        internal const string ProjectTypeName = "PsProject";

        internal static int ImageOffset;
        private PowerShellPackage _Package;
        private VSProject _VsProject;


        static PowerShellProjectNode()
        {
            /*ImageList =
                    Utilities.GetImageList(
                            typeof (PowerShellProjectNode).Assembly.GetManifestResourceStream(
                                    "PowerStudio.VsExtension.Project.Resources.ImageList.bmp" ) );*/
            ImageList = new ImageList();
            ImageList.Images.Add( Resources.ProjectIcon );
        }

        public PowerShellProjectNode( PowerShellPackage package )
        {
            _Package = package;

            InitializeImageList();

            CanProjectDeleteItems = true;
        }

        public static ImageList ImageList { get; private set; }

        protected internal VSProject VSProject
        {
            get
            {
                if ( _VsProject == null )
                {
                    _VsProject = new OAVSProject( this );
                }

                return _VsProject;
            }
        }

        public override Guid ProjectGuid
        {
            get { return typeof (PowerShellProjectFactory).GUID; }
        }

        public override string ProjectType
        {
            get { return ProjectTypeName; }
        }

        public override int ImageIndex
        {
            get { return ImageOffset; }
        }

        public override object GetAutomationObject()
        {
            return new OAPsProject( this );
        }

        public override FileNode CreateFileNode( ProjectElement item )
        {
            var node = new PowerShellProjectFileNode( this, item );

            node.OleServiceProvider.AddService( typeof (EnvDTE.Project),
                                                new OleServiceProvider.ServiceCreatorCallback( CreateServices ),
                                                false );
            node.OleServiceProvider.AddService( typeof (ProjectItem), node.ServiceCreator, false );
            node.OleServiceProvider.AddService( typeof (VSProject),
                                                new OleServiceProvider.ServiceCreatorCallback( CreateServices ),
                                                false );

            return node;
        }

        protected override Guid[] GetConfigurationIndependentPropertyPages()
        {
            var result = new Guid[1];
            result[0] = typeof (GeneralPropertyPage).GUID;
            return result;
        }

        protected override Guid[] GetPriorityProjectDesignerPages()
        {
            var result = new Guid[1];
            result[0] = typeof (GeneralPropertyPage).GUID;
            return result;
        }

        public override void AddFileFromTemplate( string source, string target )
        {
            if ( !File.Exists( source ) )
            {
                throw new FileNotFoundException( string.Format( "Template file not found: {0}", source ) );
            }

            // The class name is based on the new file name
            string scriptName = Path.GetFileNameWithoutExtension( target );

            FileTemplateProcessor.AddReplace( "%scriptName%", scriptName );
            FileTemplateProcessor.AddReplace( "%author%", WindowsIdentity.GetCurrent().Name );

            try
            {
                FileTemplateProcessor.UntokenFile( source, target );

                FileTemplateProcessor.Reset();
            }
            catch ( Exception e )
            {
                throw new FileLoadException( "Failed to add template file to project", target, e );
            }
        }

        private void InitializeImageList()
        {
            ImageOffset = ImageHandler.ImageList.Images.Count;

            foreach ( Image img in ImageList.Images )
            {
                ImageHandler.AddImage( img );
            }
        }

        private object CreateServices( Type serviceType )
        {
            object service = null;
            if ( typeof (VSProject) == serviceType )
            {
                service = VSProject;
            }
            else if ( typeof (EnvDTE.Project) == serviceType )
            {
                service = GetAutomationObject();
            }
            return service;
        }
    }
}