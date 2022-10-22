// <copyright file="UmbracoBuilderExtensions.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.Indekso.Core.Indexing.CollectionBuilders;

namespace Umbraco.Community.Indekso.Core.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="IUmbracoBuilder"/>.
    /// </summary>
    public static class UmbracoBuilderExtensions
    {
        /// <summary>
        /// Gives easy access to the <see cref="PropertyEditorContentExtractorCollectionBuilder"/>.
        /// </summary>
        /// <param name="builder">A <see cref="IUmbracoBuilder"/>.</param>
        /// <returns>A <see cref="PropertyEditorContentExtractorCollectionBuilder"/></returns>
        public static PropertyEditorContentExtractorCollectionBuilder PropertyEditorContentExtractors(
            this IUmbracoBuilder builder) =>
            builder.WithCollectionBuilder<PropertyEditorContentExtractorCollectionBuilder>();
    }
}
