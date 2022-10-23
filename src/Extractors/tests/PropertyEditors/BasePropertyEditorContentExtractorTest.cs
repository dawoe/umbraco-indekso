// <copyright file="BasePropertyEditorContentExtractorTest.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.DependencyInjection;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;

namespace Umbraco.Community.Indekso.Extractors.Tests.PropertyEditors
{
    /// <summary>
    /// A base class for property editor content extractor unit tests.
    /// </summary>
    /// <typeparam name="TExtractor">A <see cref="IPropertyEditorContentExtractor"/>.</typeparam>
    internal abstract class BasePropertyEditorContentExtractorTest<TExtractor>
        where TExtractor : IPropertyEditorContentExtractor, new()
    {
        /// <summary>
        /// The property alias.
        /// </summary>
        public const string Alias = "propAlias";

        /// <summary>
        /// Gets the property editor alias.
        /// </summary>
        public abstract string EditorAlias { get; }

        /// <summary>
        /// Gets or sets the extractor.
        /// </summary>
        public TExtractor? Extractor { get; internal set; }

        /// <summary>
        /// Sets up the test.
        /// </summary>
        [SetUp]
        public virtual void Setup()
        {
            // setup service provider on static service provider, because this is used in extensions methods for getting content
            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(x => x.GetService(typeof(IPublishedValueFallback)))
                .Returns(Mock.Of<IPublishedValueFallback>());

            StaticServiceProvider.Instance = serviceProviderMock.Object;

            // setup the extractor instance.
            this.Extractor = this.SetupExtractor();
        }

        /// <summary>
        /// Tests IsExtractFor with incorrect editor alias.
        /// </summary>
        [Test]
        public void IsExtractForIncorrectEditorAlias()
        {
            var publishedPropertyType = new Mock<IPublishedPropertyType>();
            publishedPropertyType.SetupGet(x => x.EditorAlias).Returns("I_Do_Not_Exist");

            var result = this.Extractor!.IsExtractFor(publishedPropertyType.Object);

            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Tests IsExtractFor with correct editor alias.
        /// </summary>
        [Test]
        public void IsExtractForCorrectEditorAlias()
        {
            var publishedPropertyType = new Mock<IPublishedPropertyType>();
            publishedPropertyType.SetupGet(x => x.EditorAlias).Returns(this.EditorAlias);

            var result = this.Extractor!.IsExtractFor(publishedPropertyType.Object);

            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Tests ExtractContent with not content set.
        /// </summary>
        [Test]
        public void ExtractContentWithNoContentSet()
        {
            var content = this.CreateContent(null);

            var result = this.Extractor!.ExtractContent(content, Alias);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Sets up a extractor instance.
        /// </summary>
        /// <returns>A property editor extractor instance.</returns>
        public virtual TExtractor SetupExtractor() => new();

        /// <summary>
        /// Creates a content item, with the property to test set.
        /// </summary>
        /// <param name="propertyValue">The value for the property tot test.</param>
        /// <returns>A <see cref="IPublishedElement"/>.</returns>
        public virtual IPublishedElement CreateContent(object? propertyValue)
        {
            var content = new Mock<IPublishedElement>();

            var property = new Mock<IPublishedProperty>();
            property.Setup(x => x.Alias).Returns(Alias);
            property.Setup(x => x.GetValue(null, null)).Returns(propertyValue);
            property.Setup(x => x.HasValue(null, null)).Returns(propertyValue != null);

            content.Setup(x => x.GetProperty(Alias)).Returns(property.Object);

            return content.Object;
        }
    }
}
