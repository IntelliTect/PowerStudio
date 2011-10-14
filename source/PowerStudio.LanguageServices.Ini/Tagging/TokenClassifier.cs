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
using PowerStudio.LanguageServices.Tagging;

#endregion

namespace PowerStudio.LanguageServices.Ini.Tagging
{
    public class TokenClassifier : ITokenClassifier<IniToken>
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

        public IClassificationType GetTokenTypeClassification( IniTokenType tokenType )
        {
            if ( _TokenClassifications == null )
            {
                _TokenClassifications = GetTokenClassifications();
            }
            var index = (int) tokenType;
            if ( ( index < 0 ) ||
                 ( index >= _TokenClassifications.Length ) )
            {
                index = 0;
            }
            return _TokenClassifications[index];
        }

        private IClassificationType[] GetTokenClassifications()
        {
            return new[]
                   {
                           StandardClassificationService.Other,
                           StandardClassificationService.WhiteSpace,
                           StandardClassificationService.Comment,
                           StandardClassificationService.Operator,
                           StandardClassificationService.SymbolDefinition,
                           StandardClassificationService.SymbolReference,
                           StandardClassificationService.Identifier,
                           StandardClassificationService.StringLiteral,
                           StandardClassificationService.NumberLiteral,
                   };
        }

        #region Implementation of ITokenClassifier

        /// <summary>
        ///   Gets the <see cref = "Microsoft.VisualStudio.Text.Classification.IClassificationType" /> with the specified token type.
        /// </summary>
        public virtual IClassificationType this[ IniToken token ]
        {
            get
            {
                IClassificationType clasificationType = GetTokenTypeClassification( token.Type );
                return clasificationType;
            }
        }

        #endregion
    }
}