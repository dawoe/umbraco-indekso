// <copyright file="ContentTypeContentAppFactory.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Models.Membership;

namespace Umbraco.Community.Indekso.BackOffice.Factories
{
    /// <summary>
    /// Factory for creating the content type content app.
    /// </summary>
    internal sealed class ContentTypeContentAppFactory : IContentAppFactory
    {
        /// <inheritdoc />
        public ContentApp? GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            if (source is not IContentType contentType)
            {
                return null;
            }

            if (contentType.HasIdentity == false)
            {
                return null;
            }

            return new ContentApp
            {
                Alias = "indeksoContentTypeContentApp",
                Name = "Indekso",
                Icon = "icon-search",
                View = "/App_Plugins/Indekso/app.html",
            };
        }
    }
}
