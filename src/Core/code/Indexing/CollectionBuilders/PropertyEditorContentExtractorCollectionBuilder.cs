// <copyright file="PropertyEditorContentExtractorCollectionBuilder.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Community.Indekso.Core.Indexing.Collections;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;

namespace Umbraco.Community.Indekso.Core.Indexing.CollectionBuilders
{
    /// <summary>
    /// A collection builder for the <see cref="PropertyEditorContentExtractorCollection"/>.
    /// </summary>
    public sealed class PropertyEditorContentExtractorCollectionBuilder : OrderedCollectionBuilderBase<PropertyEditorContentExtractorCollectionBuilder, PropertyEditorContentExtractorCollection, IPropertyEditorContentExtractor>
    {
        /// <inheritdoc/>
        protected override PropertyEditorContentExtractorCollectionBuilder This => this;
    }
}
