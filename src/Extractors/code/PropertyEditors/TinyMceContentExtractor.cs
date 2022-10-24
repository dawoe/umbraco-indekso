// <copyright file="TinyMceContentExtractor.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Strings;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;
using Umbraco.Extensions;

namespace Umbraco.Community.Indekso.Extractors.PropertyEditors
{
    /// <summary>
    /// The content extractor for the tiny mce property editor.
    /// </summary>
    public sealed class TinyMceContentExtractor : IPropertyEditorContentExtractor
    {
        /// <inheritdoc />
        public bool IsExtractFor(IPublishedPropertyType propertyType) => propertyType.EditorAlias.InvariantEquals(Cms.Core.Constants.PropertyEditors.Aliases.TinyMce);

        /// <inheritdoc/>
        public string ExtractContent(IPublishedElement content, string alias)
        {
            if (content.HasValue(alias) == false)
            {
                return string.Empty;
            }

            var htmlValue = content.Value<IHtmlEncodedString>(alias);

            if (htmlValue is null || htmlValue.ToString().IsNullOrWhiteSpace())
            {
                return string.Empty;
            }

            return (htmlValue.ToString() ?? string.Empty).StripHtml();
        }
    }
}
