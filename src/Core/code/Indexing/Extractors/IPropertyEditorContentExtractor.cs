// <copyright file="IPropertyEditorContentExtractor.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Umbraco.Community.Indekso.Core.Indexing.Extractors
{
    /// <summary>
    /// Represents a extractor for getting content out of a property editor for full text indexing.
    /// </summary>
    public interface IPropertyEditorContentExtractor : IDiscoverable
    {
        /// <summary>
        /// Checks whether this is a property editor content extractor for the property type.
        /// </summary>
        /// <param name="propertyType">A <see cref="IPublishedPropertyType"/>.</param>
        /// <returns>A value indicating whether this is a property editor content extractor for the property type.</returns>
        bool IsExtractFor(IPublishedPropertyType propertyType);

        /// <summary>
        /// Extracts the content of a property of a <see cref="IPublishedElement"/>.
        /// </summary>
        /// <param name="content">The <see cref="IPublishedElement"/> content item.</param>
        /// <param name="alias">The alias of the property to extract content from.</param>
        /// <returns>A string representation of the content.</returns>
        string ExtractContent(IPublishedElement content, string alias);
    }
}
