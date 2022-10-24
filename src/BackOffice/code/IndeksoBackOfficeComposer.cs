// <copyright file="IndeksoBackOfficeComposer.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.Indekso.BackOffice.Components;

namespace Umbraco.Community.Indekso.BackOffice
{
    /// <summary>
    /// Composer for the indekso back office.
    /// </summary>
    internal sealed class IndeksoBackOfficeComposer : IComposer
    {
        /// <inheritdoc/>
        public void Compose(IUmbracoBuilder builder)
        {
            if (builder.ContentApps().Has<ContentTypeContentAppFactory>() == false)
            {
                builder.ContentApps().Append<ContentTypeContentAppFactory>();
            }

            if (builder.ManifestFilters().Has<ManifestFilter>() == false)
            {
                builder.ManifestFilters().Append<ManifestFilter>();
            }
        }
    }
}
