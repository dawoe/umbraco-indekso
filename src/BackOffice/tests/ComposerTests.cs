// <copyright file="ComposerTests.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.Indekso.BackOffice.Components;

namespace Umbraco.Community.Indekso.BackOffice.Tests
{
    /// <summary>
    /// Tests for the <see cref="IndeksoBackOfficeComposer"/> composer.
    /// </summary>
    [TestFixture]
    internal sealed class ComposerTests
    {
        private IUmbracoBuilder builder = null!;

        /// <summary>
        /// Sets up the test.
        /// </summary>
        [SetUp]
        public void Setup() =>
            this.builder = new UmbracoBuilder(
                Mock.Of<IServiceCollection>(),
                Mock.Of<IConfiguration>(),
                new TypeLoader(Mock.Of<ITypeFinder>(), Mock.Of<ILogger<TypeLoader>>()));

        /// <summary>
        /// Checks if manifest filter is registered.
        /// </summary>
        /// <param name="alreadyRegistered">A value indicating whether the manifest filter is already registered.</param>
        [TestCase(false, TestName = "WhenNotYetRegistered")]
        [TestCase(true, TestName = "WhenNotAlreadyRegistered")]
        [Test]
        public void ManifestFilterShouldBeRegistered(bool alreadyRegistered)
        {
            if (alreadyRegistered)
            {
                this.builder.ManifestFilters().Append<ManifestFilter>();
            }

            var composer = new IndeksoBackOfficeComposer();

            composer.Compose(this.builder);

            Assert.That(this.builder.ManifestFilters().Has<ManifestFilter>(), Is.True);
        }

        /// <summary>
        /// Checks if content app is registered.
        /// </summary>
        /// <param name="alreadyRegistered">A value indicating whether the content app is already registered.</param>
        [TestCase(false, TestName = "WhenNotYetRegistered")]
        [TestCase(true, TestName = "WhenNotAlreadyRegistered")]
        [Test]
        public void ContentAppFactoryShouldBeRegistered(bool alreadyRegistered)
        {
            if (alreadyRegistered)
            {
                this.builder.ContentApps().Append<ContentTypeContentAppFactory>();
            }

            var composer = new IndeksoBackOfficeComposer();

            composer.Compose(this.builder);

            Assert.That(this.builder.ContentApps().Has<ContentTypeContentAppFactory>(), Is.True);
        }
    }
}
