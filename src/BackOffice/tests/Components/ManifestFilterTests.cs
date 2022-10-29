// <copyright file="ManifestFilterTests.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Manifest;
using Umbraco.Community.Indekso.BackOffice.Components;
using Umbraco.Extensions;

namespace Umbraco.Community.Indekso.BackOffice.Tests.Components
{
    /// <summary>
    /// The unit tests for the manifest filter.
    /// </summary>
    [TestFixture]
    internal sealed class ManifestFilterTests
    {
        private PackageManifest packageManifest = new();

        /// <summary>
        /// Setup for each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var manifestFilter = new ManifestFilter();

            var maniFestList = new List<PackageManifest>();

            manifestFilter.Filter(maniFestList);

            this.packageManifest = maniFestList.FirstOrDefault() ?? new PackageManifest();
        }

        /// <summary>
        /// Tests if package settings are correct.
        /// </summary>
        [Test]
        public void PackageSettingsShouldCorrect()
        {
            var assembly = typeof(ManifestFilterTests).Assembly;

            var version = assembly.GetName().Version?.ToString(3) ?? "1.0.0";

            Assert.Multiple(() =>
            {
                Assert.That(this.packageManifest.PackageName, Is.EqualTo("Indekso"));
                Assert.That(this.packageManifest.AllowPackageTelemetry, Is.True);
                Assert.That(this.packageManifest.BundleOptions, Is.EqualTo(BundleOptions.Default));
                Assert.That(this.packageManifest.Version, Is.EqualTo(version));
            });
        }

        /// <summary>
        /// Tests that all javascript files in the www root folder are registered in the package manifest.
        /// </summary>
        [Test]
        public void AllJavascriptFilesShouldBeRegistered()
        {
            var ds = Path.DirectorySeparatorChar;

            // get path to www root in the project
            var currentPath = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)?.TrimEnd($"{ds}bin{ds}Debug")
                .TrimEnd($"{ds}bin{ds}Release").TrimEnd($"{ds}tests") + $"{ds}code{ds}wwwroot{ds}";

            var diskFiles = this.FindFilesInDirectory(currentPath, "js");

            Assert.Multiple(() =>
            {
                Assert.That(this.packageManifest.Scripts.Length, Is.EqualTo(diskFiles.Count));

                foreach (var diskFile in diskFiles)
                {
                    var relativePath = diskFile.TrimStart(currentPath).Replace(ds.ToString(), "/", StringComparison.InvariantCulture)
                        .EnsureStartsWith("/App_Plugins/Indekso/");
                    Assert.That(this.packageManifest.Scripts.Contains(relativePath, StringComparer.OrdinalIgnoreCase), Is.True, "Javascript file {0} is not registered in package manifest", relativePath);
                }
            });
        }

        /// <summary>
        /// Tests that all css files in the www root folder are registered in the package manifest.
        /// </summary>
        [Test]
        public void AllCssFilesShouldBeRegistered()
        {
            var ds = Path.DirectorySeparatorChar;

            // get path to www root in the project
            var currentPath = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)?.TrimEnd($"{ds}bin{ds}Debug")
                .TrimEnd($"{ds}bin{ds}Release").TrimEnd($"{ds}tests") + $"{ds}code{ds}wwwroot{ds}";

            var diskFiles = this.FindFilesInDirectory(currentPath, "css");

            Assert.Multiple(() =>
            {
                Assert.That(this.packageManifest.Stylesheets.Length, Is.EqualTo(diskFiles.Count));

                foreach (var diskFile in diskFiles)
                {
                    var relativePath = diskFile.TrimStart(currentPath).Replace(ds.ToString(), "/", StringComparison.InvariantCulture)
                        .EnsureStartsWith("/App_Plugins/Indekso/");
                    Assert.That(this.packageManifest.Stylesheets.Contains(relativePath, StringComparer.OrdinalIgnoreCase), Is.True, "Css file {0} is not registered in package manifest", relativePath);
                }
            });
        }

        private IList<string> FindFilesInDirectory(string targetDirectory, string extension)
        {
            var foundFiles = new List<string>();

            // Process the list of files found in the directory.
            var fileEntries = Directory.GetFiles(targetDirectory, $"*.{extension}");
            foreach (var fileName in fileEntries)
            {
                foundFiles.Add(fileName);
            }

            // Recurse into subdirectories of this directory.
            var subdirectories = Directory.GetDirectories(targetDirectory);
            foreach (var directory in subdirectories)
            {
                foundFiles.AddRange(this.FindFilesInDirectory(directory, extension));
            }

            return foundFiles;
        }
    }
}
