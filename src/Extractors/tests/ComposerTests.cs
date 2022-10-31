// <copyright file="ComposerTests.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Community.Indekso.Core.Extensions;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;

namespace Umbraco.Community.Indekso.Extractors.Tests
{
    /// <summary>
    /// Tests for the <see cref="IndeksoExtractorsComposer"/> composer.
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
        /// Checks if all property content extractors are registered.
        /// </summary>
        [Test]
        public void AllPropertyEditorContentExtractorShouldBeRegistered()
        {
            var composer = new IndeksoExtractorsComposer();

            composer.Compose(this.builder);

            Assert.Multiple(() =>
            {
                Assert.That(this.builder.PropertyEditorContentExtractors().GetTypes().Any(), Is.True);

                var assemblyProvider =
                    new DefaultUmbracoAssemblyProvider(Assembly.GetExecutingAssembly(), Mock.Of<ILoggerFactory>());

                var typeFinder = new TypeFinder(Mock.Of<ILogger<TypeFinder>>(), assemblyProvider);

                var types = typeFinder.FindClassesOfType(typeof(IPropertyEditorContentExtractor));

                foreach (var type in types)
                {
                    this.builder.PropertyEditorContentExtractors().Has(type);
                }
            });
        }
    }
}
