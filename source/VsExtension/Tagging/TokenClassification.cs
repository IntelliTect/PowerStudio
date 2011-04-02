using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;

namespace PowerStudio.VsExtension.Tagging
{
    public class TokenClassification : ITokenClassification
    {
        private IClassificationTypeRegistryService RegistryService { get; set; }

        private Dictionary<PSTokenType, IClassificationType> TokenClassificationMapping { get; set; }

        public TokenClassification(IClassificationTypeRegistryService registryService)
        {
            RegistryService = registryService;
            TokenClassificationMapping = new Dictionary<PSTokenType, IClassificationType>();
            
            LoadTokenClassificationMappings( registryService );
        }

        protected virtual void LoadTokenClassificationMappings(IClassificationTypeRegistryService registryService)
        {
            TokenClassificationMapping[PSTokenType.Unknown] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Other);

            TokenClassificationMapping[PSTokenType.Command] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Other);

            TokenClassificationMapping[PSTokenType.CommandParameter] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Identifier);

            TokenClassificationMapping[PSTokenType.CommandArgument] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Identifier);

            TokenClassificationMapping[PSTokenType.Number] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Number);

            TokenClassificationMapping[PSTokenType.String] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.String);

            TokenClassificationMapping[PSTokenType.Variable] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Identifier);

            TokenClassificationMapping[PSTokenType.Member] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Identifier);

            TokenClassificationMapping[PSTokenType.LoopLabel] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Identifier);

            TokenClassificationMapping[PSTokenType.Attribute] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Operator);

            TokenClassificationMapping[PSTokenType.Type] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Identifier);

            TokenClassificationMapping[PSTokenType.Operator] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Operator);

            TokenClassificationMapping[PSTokenType.GroupStart] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Other);

            TokenClassificationMapping[PSTokenType.GroupEnd] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Other);

            TokenClassificationMapping[PSTokenType.Keyword] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Keyword);

            TokenClassificationMapping[PSTokenType.Comment] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Comment);

            TokenClassificationMapping[PSTokenType.StatementSeparator] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Other);

            TokenClassificationMapping[PSTokenType.NewLine] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.WhiteSpace);

            TokenClassificationMapping[PSTokenType.LineContinuation] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Other);

            TokenClassificationMapping[PSTokenType.Position] =
                    registryService.GetClassificationType(PredefinedClassificationTypeNames.Other);
        }

        #region Implementation of ITokenClassification

        public virtual IClassificationType this[ PSTokenType tokenType ]
        {
            get { return TokenClassificationMapping[tokenType]; }
        }

        #endregion
    }
}
