using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace PowerStudio.VsExtension.Tagging
{
    public class PowerShellClassificationTagger : ITagger<ClassificationTag>
    {
        public PowerShellClassificationTagger( ITextBuffer buffer,
                                               ITagAggregator<PowerShellTokenTag> aggregator,
                                               IClassificationTypeRegistryService registryService )
        {
            Buffer = buffer;
            Aggregator = aggregator;
            RegistryService = registryService;
            TokenClassificationMapping = new Dictionary<PSTokenType, IClassificationType>();
            LoadTokenClassificationMappings();
        }

        public Dictionary<PSTokenType, IClassificationType> TokenClassificationMapping { get; set; }
        public ITextBuffer Buffer { get; set; }
        public ITagAggregator<PowerShellTokenTag> Aggregator { get; set; }
        public IClassificationTypeRegistryService RegistryService { get; set; }

        private void LoadTokenClassificationMappings()
        {
            TokenClassificationMapping[PSTokenType.Unknown] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.Command] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.CommandParameter] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.CommandArgument] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.Number] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Number );

            TokenClassificationMapping[PSTokenType.String] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.String );

            TokenClassificationMapping[PSTokenType.Variable] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.Member] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.LoopLabel] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.Attribute] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Operator );

            TokenClassificationMapping[PSTokenType.Type] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Identifier );

            TokenClassificationMapping[PSTokenType.Operator] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Operator );

            TokenClassificationMapping[PSTokenType.GroupStart] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.GroupEnd] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.Keyword] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Keyword );

            TokenClassificationMapping[PSTokenType.Comment] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Comment );

            TokenClassificationMapping[PSTokenType.StatementSeparator] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.NewLine] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.WhiteSpace );

            TokenClassificationMapping[PSTokenType.LineContinuation] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Other );

            TokenClassificationMapping[PSTokenType.Position] =
                    RegistryService.GetClassificationType( PredefinedClassificationTypeNames.Other );
        }

        #region Implementation of ITagger<out ClassificationTag>

        /// <summary>
        /// Gets all the tags that overlap the <paramref name="spans"/>.
        /// </summary>
        /// <param name="spans">The spans to visit.</param>
        /// <returns>
        /// A <see cref="T:Microsoft.VisualStudio.Text.Tagging.ITagSpan`1"/> for each tag.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Taggers are not required to return their tags in any specific order.
        /// </para>
        /// <para>
        /// The recommended way to implement this method is by using generators ("yield return"),
        ///             which allows lazy evaluation of the entire tagging stack.
        /// </para>
        /// </remarks>
        public IEnumerable<ITagSpan<ClassificationTag>> GetTags( NormalizedSnapshotSpanCollection spans )
        {
            foreach ( var span in spans )
            {
                foreach (var tagSpan in Aggregator.GetTags(span))
                {
                    var tokenSpan = tagSpan.Span.GetSpans( span.Snapshot );

                    yield return
                            new TagSpan<ClassificationTag>(tokenSpan[0],
                                                            new ClassificationTag(
                                                                    TokenClassificationMapping[tagSpan.Tag.TokenType]));
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        #endregion
    }
}