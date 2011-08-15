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
using System.Collections.Generic;
using System.Windows.Media.TextFormatting;
using EnvDTE;
using Microsoft.VisualStudio.Text.Formatting;

#endregion

namespace PowerStudio.LanguageService.Formatting
{
    public class PowerShellTextFormattingParagraphProperties : TextFormattingParagraphProperties
    {
        protected double _DefaultIncrementalTab;

        public PowerShellTextFormattingParagraphProperties( TextFormattingRunProperties textProperties,
                                                            IFormattedLineSource formattedLineSource,
                                                            IServiceProvider serviceProvider )
                : base( textProperties )
        {
            ServiceProvider = serviceProvider;
            ColumnWidth = formattedLineSource.ColumnWidth;
            var dte = (DTE) ServiceProvider.GetService( typeof (DTE) );

            UpdateProperties( formattedLineSource, dte );
        }

        public IServiceProvider ServiceProvider { get; set; }

        public override double DefaultIncrementalTab
        {
            get { return _DefaultIncrementalTab; }
        }

        /// <summary>
        /// Gets a collection of tab definitions.
        /// </summary>
        /// <returns>A list of <see cref="T:System.Windows.Media.TextFormatting.TextTabProperties"/> objects.</returns>
        public override IList<TextTabProperties> Tabs
        {
            get
            {
                double offset = ColumnWidth * InitialTabSize + 1;
                return new List<TextTabProperties>
                       {
                               new TextTabProperties( TextTabAlignment.Left, offset, 0, 0 )
                       };
            }
        }

        public double ColumnWidth { get; set; }
        public int InitialTabSize { get; set; }

        private void UpdateProperties( IFormattedLineSource formattedLineSource, DTE dte )
        {
            Properties properties = dte.Properties[LanguageConfiguration.Name, "PowerStudio"];

            if ( properties == null )
            {
                InitialTabSize = 4;
                _DefaultIncrementalTab = formattedLineSource.ColumnWidth * formattedLineSource.TabSize;
                return;
            }

            InitialTabSize = (int) properties.Item( "InitialTabSize" ).Value;
            var normalTabSize = (int) properties.Item( "NormalTabSize" ).Value;
            normalTabSize = ( normalTabSize != 0 ? normalTabSize : formattedLineSource.TabSize );
            _DefaultIncrementalTab = formattedLineSource.ColumnWidth * normalTabSize;
        }
    }
}