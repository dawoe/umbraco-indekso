// <copyright file="PropertyEditorContentExtractorCollection.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;

namespace Umbraco.Community.Indekso.Core.Indexing.Collections
{
    /// <summary>
    /// A collection of <see cref="IPropertyEditorContentExtractor"/> implementations.
    /// </summary>
    public sealed class PropertyEditorContentExtractorCollection : BuilderCollectionBase<IPropertyEditorContentExtractor>
    {
        private readonly object locker = new();
        private Dictionary<IPublishedPropertyType, IList<IPropertyEditorContentExtractor>>? extractors;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyEditorContentExtractorCollection"/> class.
        /// </summary>
        /// <param name="items">A <see cref="Func{TResult}"/> that get's the implementations of <see cref="IPropertyEditorContentExtractor"/>.</param>
        public PropertyEditorContentExtractorCollection(Func<IEnumerable<IPropertyEditorContentExtractor>> items)
            : base(items)
        {
        }

        /// <summary>
        /// Gets the registered extractors for a property type.
        /// </summary>
        /// <param name="propertyType">A <see cref="IPublishedPropertyType"/> to get the extractors for.</param>
        /// <returns>A <see cref="IList{T}"/> of registered extractors for the property type.</returns>
        public IList<IPropertyEditorContentExtractor> GetExtractorsForPropertyType(IPublishedPropertyType propertyType)
        {
            lock (this.locker)
            {
                this.extractors ??= new Dictionary<IPublishedPropertyType, IList<IPropertyEditorContentExtractor>>();

                if (this.extractors.ContainsKey(propertyType))
                {
                    return this.extractors[propertyType];
                }

                var foundExtractors = this.Where(x => x.IsExtractFor(propertyType)).ToList();

                this.extractors.Add(propertyType, foundExtractors);

                return foundExtractors;
            }
        }
    }
}
