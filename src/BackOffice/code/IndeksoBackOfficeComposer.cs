// <copyright file="IndeksoBackOfficeComposer.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.Indekso.BackOffice.Factories;

namespace Umbraco.Community.Indekso.BackOffice
{
    /// <summary>
    /// Composer for the indekso back office.
    /// </summary>
    internal sealed class IndeksoBackOfficeComposer : IComposer
    {
        /// <inheritdoc/>
        public void Compose(IUmbracoBuilder builder) => builder.ContentApps().Append<ContentTypeContentAppFactory>();
    }
}
