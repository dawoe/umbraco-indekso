// <copyright file="ManifestFilter.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Manifest;

namespace Umbraco.Community.Indekso.BackOffice.Components
{
    /// <summary>
    /// A <see cref="IManifestFilter"/> for registering back office assets.
    /// </summary>
    internal sealed class ManifestFilter : IManifestFilter
    {
        /// <inheritdoc/>
        public void Filter(List<PackageManifest> manifests)
        {
            var assembly = typeof(ManifestFilter).Assembly;

            manifests.Add(new()
            {
                PackageName = "Indekso",
                Version = assembly.GetName().Version?.ToString(3) ?? "1.0.0",
                Scripts = new[] { "/App_Plugins/Indekso/app.controller.js", },
                AllowPackageTelemetry = true,
                BundleOptions = BundleOptions.Default,
            });
        }
    }
}
