#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class TokenClassification : ITokenClassification
    {
        public TokenClassification( IClassificationTypeRegistryService registryService )
        {
            RegistryService = registryService;
            TokenClassificationMapping = new Dictionary<PSTokenType, IClassificationType>();

            LoadTokenClassificationMappings( registryService );
        }

        private IClassificationTypeRegistryService RegistryService { get; set; }

        private Dictionary<PSTokenType, IClassificationType> TokenClassificationMapping { get; set; }

        protected virtual void LoadTokenClassificationMappings( IClassificationTypeRegistryService registryService )
        {
            TokenClassificationMapping[PSTokenType.Unknown] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.Command] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.CommandParameter] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.SymbolDefinition );

            TokenClassificationMapping[PSTokenType.CommandArgument] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.SymbolReference );

            TokenClassificationMapping[PSTokenType.Number] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Number );

            TokenClassificationMapping[PSTokenType.String] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.String );

            TokenClassificationMapping[PSTokenType.Variable] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.SymbolReference );

            TokenClassificationMapping[PSTokenType.Member] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.LoopLabel] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.SymbolDefinition );

            TokenClassificationMapping[PSTokenType.Attribute] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Operator );

            TokenClassificationMapping[PSTokenType.Type] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.Operator] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Operator );

            TokenClassificationMapping[PSTokenType.GroupStart] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.GroupEnd] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.Keyword] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Keyword );

            TokenClassificationMapping[PSTokenType.Comment] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Comment );

            TokenClassificationMapping[PSTokenType.StatementSeparator] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.NewLine] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.WhiteSpace );

            TokenClassificationMapping[PSTokenType.LineContinuation] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.Position] =
                    registryService.GetClassificationType( PredefinedClassificationTypeNames.Other );
        }

        #region Implementation of ITokenClassification

        public virtual IClassificationType this[ PSTokenType tokenType ]
        {
            get { return TokenClassificationMapping[tokenType]; }
        }

        #endregion
    }
}