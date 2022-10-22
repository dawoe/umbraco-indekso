// <copyright file="PropertyEditorContentExtractorCollection.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;

namespace Umbraco.Community.Indekso.Core.Indexing.Collections
{
    /// <summary>
    /// A collection of <see cref="IPropertyEditorContentExtractor"/> implementations.
    /// </summary>
    public sealed class PropertyEditorContentExtractorCollection : BuilderCollectionBase<IPropertyEditorContentExtractor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyEditorContentExtractorCollection"/> class.
        /// </summary>
        /// <param name="items">A <see cref="Func{TResult}"/> that get's the implementations of <see cref="IPropertyEditorContentExtractor"/>.</param>
        public PropertyEditorContentExtractorCollection(Func<IEnumerable<IPropertyEditorContentExtractor>> items)
            : base(items)
        {
        }
    }
}
