// <copyright file="TextBoxContentExtractor.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;
using Umbraco.Extensions;

namespace Umbraco.Community.Indekso.Extractors.PropertyEditors
{
    /// <summary>
    /// The Content extractor for text box and text area property editor.
    /// </summary>
    public sealed class TextBoxContentExtractor : IPropertyEditorContentExtractor
    {
        /// <inheritdoc />
        public bool IsExtractFor(IPublishedPropertyType propertyType) =>
            propertyType.EditorAlias.InvariantEquals(Cms.Core.Constants.PropertyEditors.Aliases.TextBox) ||
            propertyType.EditorAlias.InvariantEquals(Cms.Core.Constants.PropertyEditors.Aliases.TextArea);

        /// <inheritdoc />
        public string ExtractContent(IPublishedElement content, string alias)
        {
            if (content.HasValue(alias) == false)
            {
                return string.Empty;
            }

            return content.Value<string>(alias) ?? string.Empty;
        }
    }
}
