using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Tagging;
using PowerStudio.VsExtension.Parsing;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellTokenTag : ITag
    {
        public Configuration.TokenDefinition TokenDefinition { get; set; }
    }
}
