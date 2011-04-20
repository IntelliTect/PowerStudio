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
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class OutliningTag : TokenTag, IOutliningRegionTag
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "OutliningTag" /> class.
        /// </summary>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "span">The span.</param>
        public OutliningTag( ITextSnapshot snapshot, SnapshotSpan span )
                : this( snapshot, span, false )
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "OutliningTag" /> class.
        /// </summary>
        /// <param name = "snapshot">The snapshot.</param>
        /// <param name = "span">The span.</param>
        /// <param name = "isImplementation">if set to <c>true</c> [is implementation].</param>
        public OutliningTag( ITextSnapshot snapshot, SnapshotSpan span, bool isImplementation )
        {
            Snapshot = snapshot;
            Span = span;
            IsImplementation = isImplementation;
        }

        public int EndLine { get; set; }

        public int StartLine { get; set; }

        public ITextSnapshot Snapshot { get; private set; }

        #region IOutliningRegionTag Members

        /// <summary>
        ///   Gets the data object for the collapsed UI. If the default is set, returns null.
        /// </summary>
        /// <value></value>
        public object CollapsedForm
        {
            get { return "..."; }
        }

        /// <summary>
        ///   Gets the data object for the collapsed UI tooltip. If the default is set, returns null.
        /// </summary>
        /// <value></value>
        public object CollapsedHintForm
        {
            get
            {
                string collapsedHint = Snapshot.GetText( Span );

                string[] lines = collapsedHint.Split( new[] { Environment.NewLine }, StringSplitOptions.None );

                if ( lines.Length == 0 )
                {
                    return null;
                }

                int whiteSpaceBufferSize = GetWhiteSpaceBufferSize( lines );

                string text = RemoveWhiteSpaceBufferFromText( lines, whiteSpaceBufferSize );

                return text;
            }
        }

        /// <summary>
        ///   Determines whether the region is collapsed by default.
        /// </summary>
        /// <value></value>
        public bool IsDefaultCollapsed { get; private set; }

        /// <summary>
        ///   Determines whether a region is an implementation region.
        /// </summary>
        /// <value></value>
        /// <remarks>
        ///   Implementation regions are the blocks of code following a method definition.
        ///   They are used for commands such as the Visual Studio Collapse to Definition command,
        ///   which hides the implementation region and leaves only the method definition exposed.
        /// </remarks>
        public bool IsImplementation { get; private set; }

        #endregion

        private static string RemoveWhiteSpaceBufferFromText( IEnumerable<string> lines, int whiteSpaceBufferSize )
        {
            var builder = new StringBuilder();
            foreach ( string line in lines.Where( text => text.Length >= whiteSpaceBufferSize ) )
            {
                builder.AppendLine( line.Substring( whiteSpaceBufferSize ) );
            }
            return builder.ToString();
        }

        private static int GetWhiteSpaceBufferSize( IEnumerable<string> lines )
        {
            int minBufferSize = Int32.MaxValue;
            foreach ( string line in lines )
            {
                for ( int currentBufferSize = 0; currentBufferSize < line.Length; currentBufferSize++ )
                {
                    if ( line[currentBufferSize] != ' ' )
                    {
                        minBufferSize = Math.Min( currentBufferSize, minBufferSize );
                    }
                }
            }
            return minBufferSize;
        }
    }
}