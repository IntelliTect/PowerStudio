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
using PowerStudio.Resources;
using VSLangProj;

#endregion

namespace PowerStudio.LanguageService.Project
{
    [Guid( PsConstants.ProjectNodeGuid )]
    public class PowerShellProjectNode : ProjectNode
    {
        internal const string ProjectTypeName = "PsProject";

        internal static int ImageOffset;
        private PowerShellPackageBase _Package;
        private VSProject _VsProject;

        static PowerShellProjectNode()
        {
            ImageList = new ImageList();
            ImageList.Images.Add( Resources.ProjectIcon );
        }

        public PowerShellProjectNode( PowerShellPackageBase package )
        {
            _Package = package;

            InitializeImageList();

            CanProjectDeleteItems = true;
        }

        public static ImageList ImageList { get; private set; }

        protected internal VSProject VsProject
        {
            get { return _VsProject ?? ( _VsProject = new OAVSProject( this ) ); }
        }

        /// <summary>
        /// This Guid must match the Guid you registered under
        /// HKLM\Software\Microsoft\VisualStudio\%version%\Projects.
        /// Among other things, the Project framework uses this
        /// guid to find your project and item templates.
        /// </summary>
        /// <value></value>
        public override Guid ProjectGuid
        {
            get { return typeof (PowerShellProjectFactory).GUID; }
        }

        /// <summary>
        /// Returns a caption for VSHPROPID_TypeName.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public override string ProjectType
        {
            get { return ProjectTypeName; }
        }

        /// <summary>
        /// Gets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        public override int ImageIndex
        {
            get { return ImageOffset; }
        }

        /// <summary>
        /// Gets the automation object for the project node.
        /// </summary>
        /// <returns>
        /// An instance of an EnvDTE.Project implementation object representing the automation object for the project.
        /// </returns>
        public override object GetAutomationObject()
        {
            return new OAPsProject( this );
        }

        /// <summary>
        /// Create a file node based on an msbuild item.
        /// </summary>
        /// <param name="item">msbuild item</param>
        /// <returns>FileNode added</returns>
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

        /// <summary>
        /// List of Guids of the config independent property pages. It is called by the GetProperty for VSHPROPID_PropertyPagesCLSIDList property.
        /// </summary>
        /// <returns></returns>
        protected override Guid[] GetConfigurationIndependentPropertyPages()
        {
            var result = new Guid[1];
            result[0] = typeof (GeneralPropertyPage).GUID;
            return result;
        }

        /// <summary>
        /// An ordered list of guids of the prefered property pages. See <see cref="__VSHPROPID.VSHPROPID_PriorityPropertyPagesCLSIDList"/>
        /// </summary>
        /// <returns>An array of guids.</returns>
        protected override Guid[] GetPriorityProjectDesignerPages()
        {
            var result = new Guid[1];
            result[0] = typeof (GeneralPropertyPage).GUID;
            return result;
        }

        /// <summary>
        /// Called to add a file to the project from a template.
        /// Override to do it yourself if you want to customize the file
        /// </summary>
        /// <param name="source">Full path of template file</param>
        /// <param name="target">Full path of file once added to the project</param>
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
                service = VsProject;
            }
            else if ( typeof (EnvDTE.Project) == serviceType )
            {
                service = GetAutomationObject();
            }
            return service;
        }
    }
}