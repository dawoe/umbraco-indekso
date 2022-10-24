// <copyright file="PropertyEditorContentExtractorCollectionTests.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Community.Indekso.Core.Indexing.Collections;
using Umbraco.Community.Indekso.Core.Indexing.Extractors;

namespace Umbraco.Community.Indekso.Core.Tests.Indexing.Collections
{
    /// <summary>
    /// The tests for the <see cref="PropertyEditorContentExtractorCollection"/>.
    /// </summary>
    [TestFixture]
    internal sealed class PropertyEditorContentExtractorCollectionTests
    {
        /// <summary>
        /// Tests if get extractors for property returns a empty list if no extractors for the property type are registered.
        /// </summary>
        [Test]
        public void GetExtractorsForPropertyTypeShouldReturnEmptyListWhenNonAreFound()
        {
            // arrange
            var publishedPropertyType = new Mock<IPublishedPropertyType>();

            var extractor = new Mock<IPropertyEditorContentExtractor>();
            extractor.Setup(x => x.IsExtractFor(publishedPropertyType.Object)).Returns(false);

            var collection =
                new PropertyEditorContentExtractorCollection(() => new List<IPropertyEditorContentExtractor> { extractor.Object });

            // act
            var result = collection.GetExtractorsForPropertyType(publishedPropertyType.Object);

            var cachedResult = collection.GetExtractorsForPropertyType(publishedPropertyType.Object);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Any(), Is.False);
                Assert.That(cachedResult.Any(), Is.False);

                extractor.Verify(x => x.IsExtractFor(publishedPropertyType.Object), Times.Once);
            });
        }

        /// <summary>
        /// Tests if get extractors for property returns a list if extractors are found for the property type.
        /// </summary>
        [Test]
        public void GetExtractorsForPropertyTypeShouldMatchingExtractorsWhenFound()
        {
            // arrange
            var firstPropertyType = new Mock<IPublishedPropertyType>();
            var secondProperty = new Mock<IPublishedPropertyType>();

            var firstExtractor = new Mock<IPropertyEditorContentExtractor>();
            firstExtractor.Setup(x => x.IsExtractFor(firstPropertyType.Object)).Returns(true);
            firstExtractor.Setup(x => x.IsExtractFor(secondProperty.Object)).Returns(false);

            var secondExtractor = new Mock<IPropertyEditorContentExtractor>();
            secondExtractor.Setup(x => x.IsExtractFor(firstPropertyType.Object)).Returns(false);
            secondExtractor.Setup(x => x.IsExtractFor(secondProperty.Object)).Returns(true);

            var thirdExtractor = new Mock<IPropertyEditorContentExtractor>();
            thirdExtractor.Setup(x => x.IsExtractFor(firstPropertyType.Object)).Returns(true);
            thirdExtractor.Setup(x => x.IsExtractFor(secondProperty.Object)).Returns(false);

            var collection =
                new PropertyEditorContentExtractorCollection(() => new List<IPropertyEditorContentExtractor> { firstExtractor.Object, secondExtractor.Object, thirdExtractor.Object });

            // act
            var firstPropertyResult = collection.GetExtractorsForPropertyType(firstPropertyType.Object);

            var firstPropertyCachedResult = collection.GetExtractorsForPropertyType(firstPropertyType.Object);

            var secondPropertyResult = collection.GetExtractorsForPropertyType(secondProperty.Object);

            var secondPropertyCachedResult = collection.GetExtractorsForPropertyType(secondProperty.Object);

            // assert
            Assert.Multiple(() =>
            {
                Assert.That(firstPropertyResult.Any(), Is.True);
                Assert.That(firstPropertyCachedResult.Any(), Is.True);
                Assert.That(firstPropertyResult.Count, Is.EqualTo(firstPropertyCachedResult.Count));

                Assert.That(firstPropertyResult.Contains(firstExtractor.Object), Is.True);
                Assert.That(firstPropertyResult.Contains(thirdExtractor.Object), Is.True);
                Assert.That(firstPropertyResult.Contains(secondExtractor.Object), Is.False);

                firstExtractor.Verify(x => x.IsExtractFor(firstPropertyType.Object), Times.Once);
                secondExtractor.Verify(x => x.IsExtractFor(firstPropertyType.Object), Times.Once);
                thirdExtractor.Verify(x => x.IsExtractFor(firstPropertyType.Object), Times.Once);

                Assert.That(secondPropertyResult.Any(), Is.True);
                Assert.That(secondPropertyCachedResult.Any(), Is.True);
                Assert.That(secondPropertyResult.Count, Is.EqualTo(secondPropertyCachedResult.Count));

                Assert.That(secondPropertyResult.Contains(secondExtractor.Object), Is.True);
                Assert.That(secondPropertyResult.Contains(firstExtractor.Object), Is.False);
                Assert.That(secondPropertyResult.Contains(thirdExtractor.Object), Is.False);

                firstExtractor.Verify(x => x.IsExtractFor(secondProperty.Object), Times.Once);
                secondExtractor.Verify(x => x.IsExtractFor(secondProperty.Object), Times.Once);
                thirdExtractor.Verify(x => x.IsExtractFor(secondProperty.Object), Times.Once);
            });
        }
    }
}
