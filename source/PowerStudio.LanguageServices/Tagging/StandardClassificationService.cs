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
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text.Classification;

#endregion

namespace PowerStudio.LanguageServices.Tagging
{
    /// <summary>
    ///   For some reason, this interface isn't imported via MEF - it just comes through null.
    /// </summary>
    public class StandardClassificationService : IStandardClassificationService
    {
        internal IClassificationTypeRegistryService ClassificationTypeRegistry;

        public StandardClassificationService( IClassificationTypeRegistryService classificationTypeRegistryService )
        {
            if ( classificationTypeRegistryService == null )
            {
                throw new ArgumentNullException( "classificationTypeRegistryService" );
            }
            ClassificationTypeRegistry = classificationTypeRegistryService;
        }

        #region IStandardClassificationService Members

        public IClassificationType NaturalLanguage
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.NaturalLanguage ); }
        }

        public IClassificationType FormalLanguage
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.FormalLanguage ); }
        }

        public IClassificationType Comment
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Comment ); }
        }

        public IClassificationType Identifier
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Identifier ); }
        }

        public IClassificationType Keyword
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Keyword ); }
        }

        public IClassificationType WhiteSpace
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.WhiteSpace ); }
        }

        public IClassificationType Operator
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Operator ); }
        }

        public IClassificationType Literal
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Literal ); }
        }

        public IClassificationType NumberLiteral
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Number ); }
        }

        public IClassificationType StringLiteral
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.String ); }
        }

        public IClassificationType CharacterLiteral
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Character ); }
        }

        public IClassificationType Other
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.Other ); }
        }

        public IClassificationType ExcludedCode
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.ExcludedCode ); }
        }

        public IClassificationType PreprocessorKeyword
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.PreprocessorKeyword ); }
        }

        public IClassificationType SymbolDefinition
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.SymbolDefinition ); }
        }

        public IClassificationType SymbolReference
        {
            get { return GetClassificationType( PredefinedClassificationTypeNames.SymbolReference ); }
        }

        #endregion

        private IClassificationType GetClassificationType( string type )
        {
            return ClassificationTypeRegistry.GetClassificationType( type );
        }
    }
}