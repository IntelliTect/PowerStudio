#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class TokenClassifier : ITokenClassifier
    {
        private IClassificationType[] _TokenClassifications;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "TokenClassifier" /> class.
        /// </summary>
        /// <param name = "registryService">The classification registry service.</param>
        /// <param name = "buffer"></param>
        public TokenClassifier( IClassificationTypeRegistryService registryService, ITextBuffer buffer )
        {
            RegistryService = registryService;
            Buffer = buffer;
            StandardClassificationService = new StandardClassificationService( registryService );
        }

        public ITextBuffer Buffer { get; set; }
        public IClassificationTypeRegistryService RegistryService { get; set; }
        internal IStandardClassificationService StandardClassificationService { get; set; }

        public IClassificationType GetTokenTypeClassification( TokenClass tokenClass )
        {
            if ( _TokenClassifications == null )
            {
                _TokenClassifications = new[]
                                        {
                                                StandardClassificationService.CharacterLiteral,
                                                StandardClassificationService.Comment,
                                                StandardClassificationService.ExcludedCode,
                                                StandardClassificationService.FormalLanguage,
                                                StandardClassificationService.Identifier,
                                                StandardClassificationService.Keyword,
                                                StandardClassificationService.Literal,
                                                StandardClassificationService.NaturalLanguage,
                                                StandardClassificationService.NumberLiteral,
                                                StandardClassificationService.Operator,
                                                StandardClassificationService.Other,
                                                StandardClassificationService.PreprocessorKeyword,
                                                StandardClassificationService.StringLiteral,
                                                StandardClassificationService.SymbolDefinition,
                                                StandardClassificationService.SymbolReference,
                                                StandardClassificationService.WhiteSpace
                                        };
            }
            var index = (int) tokenClass;
            if ( ( index < 0 ) ||
                 ( index >= _TokenClassifications.Length ) )
            {
                index = 10;
            }
            return _TokenClassifications[index];
        }

        #region Implementation of ITokenClassifier

        public virtual IClassificationType this[ TokenClass tokenClass ]
        {
            get
            {
                IClassificationType clasificationType = GetTokenTypeClassification( tokenClass );
                return clasificationType;
            }
        }

        #endregion
    }
}