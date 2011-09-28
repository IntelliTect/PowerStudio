#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System.Management.Automation;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using PowerStudio.LanguageServices.Tagging;

#endregion

namespace PowerStudio.LanguageServices.PowerShell.Tagging
{
    public class TokenClassifier : ITokenClassifier<PSToken>
    {
        private static readonly TokenClass[] TokenClasses = new[]
                                                            {
                                                                    TokenClass.Other, TokenClass.FormalLanguage,
                                                                    TokenClass.Other, TokenClass.Other,
                                                                    TokenClass.NumberLiteral, TokenClass.StringLiteral,
                                                                    TokenClass.Identifier, TokenClass.Identifier,
                                                                    TokenClass.Literal, TokenClass.SymbolReference,
                                                                    TokenClass.SymbolReference, TokenClass.Operator,
                                                                    TokenClass.Operator, TokenClass.Operator,
                                                                    TokenClass.Keyword, TokenClass.Comment,
                                                                    TokenClass.Other, TokenClass.Other,
                                                                    TokenClass.Other, TokenClass.Operator
                                                            };

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
                _TokenClassifications = GetTokenClassifications();
            }
            var index = (int) tokenClass;
            if ( ( index < 0 ) ||
                 ( index >= _TokenClassifications.Length ) )
            {
                index = 10;
            }
            return _TokenClassifications[index];
        }

        private IClassificationType[] GetTokenClassifications()
        {
            return new[]
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

        private static TokenClass GetTokenClass( PSTokenType psTokenType )
        {
            var index = (int) psTokenType;
            if ( ( index >= 0 ) &&
                 ( index < TokenClasses.Length ) )
            {
                return TokenClasses[index];
            }
            return TokenClass.Other;
        }

        #region Implementation of ITokenClassifier

        /// <summary>
        ///   Gets the <see cref = "Microsoft.VisualStudio.Text.Classification.IClassificationType" /> with the specified token type.
        /// </summary>
        public virtual IClassificationType this[ PSToken token ]
        {
            get
            {
                TokenClass tokenClass = GetTokenClass( token.Type );
                IClassificationType clasificationType = GetTokenTypeClassification( tokenClass );
                return clasificationType;
            }
        }

        #endregion
    }
}