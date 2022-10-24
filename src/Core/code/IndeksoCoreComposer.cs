// <copyright file="IndeksoCoreComposer.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.Indekso.Core.Extensions;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;

namespace Umbraco.Community.Indekso.Core
{
    /// <summary>
    /// A composer for indekso core.
    /// </summary>
    public sealed class IndeksoCoreComposer : IComposer
    {
        /// <inheritdoc/>
        public void Compose(IUmbracoBuilder builder) => this.RegisterPropertyEditorContentExtractors(builder);

        private void RegisterPropertyEditorContentExtractors(IUmbracoBuilder builder) =>
            builder.PropertyEditorContentExtractors().Append(builder.TypeLoader
                .GetTypes<IPropertyEditorContentExtractor>().Where(x =>
                    x.Namespace is not null &&
                    x.Namespace.StartsWith("Umbraco.Community.Indekso", StringComparison.InvariantCultureIgnoreCase)));
    }
}
