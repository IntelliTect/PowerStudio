using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace PowerStudio.VsExtension.Tagging
{
    public interface ITokenClassification
    {
        IClassificationType this[ PSTokenType tokenType ] { get; }
    }
}
