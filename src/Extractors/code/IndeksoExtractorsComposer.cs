// <copyright file="IndeksoExtractorsComposer.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.Indekso.Core;
using Umbraco.Community.Indekso.Core.Extensions;
using Umbraco.Community.Indekso.Extractors.PropertyEditors;

namespace Umbraco.Community.Indekso.Extractors
{
    /// <summary>
    /// The composer for the extractors.
    /// </summary>
    [ComposeBefore(typeof(IndeksoCoreComposer))]
    internal sealed class IndeksoExtractorsComposer : IComposer
    {
        /// <inheritdoc/>
        public void Compose(IUmbracoBuilder builder) => this.RegisterPropertyEditorContentExtractors(builder);

        private void RegisterPropertyEditorContentExtractors(IUmbracoBuilder builder)
        {
            builder.PropertyEditorContentExtractors().Append<TextBoxContentExtractor>();
            builder.PropertyEditorContentExtractors().Append<TinyMceContentExtractor>();
        }
    }
}
