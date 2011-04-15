#region Using Directives

using System.Management.Automation;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class ErrorTokenTag : ErrorTag, ISpanningTag
    {
        public ErrorTokenTag( string errorType, string toolTipContent )
                : base( errorType, toolTipContent )
        {
        }

        public PSTokenType TokenType { get; set; }
        public SnapshotSpan Span { get; set; }
    }
}