using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.VisualStudio.Text.Tagging;
using PowerStudio.VsExtension.Parsing;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellTokenTag : ITag
    {
        public PSTokenType TokenType { get; set; }
    }
}
