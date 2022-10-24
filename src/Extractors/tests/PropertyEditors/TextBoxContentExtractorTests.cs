// <copyright file="TextBoxContentExtractorTests.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Community.Indekso.Extractors.PropertyEditors;

namespace Umbraco.Community.Indekso.Extractors.Tests.PropertyEditors
{
    /// <summary>
    /// The tests for the <see cref="TextBoxContentExtractor"/>.
    /// </summary>
    [TestFixture]
    internal sealed class TextBoxContentExtractorTests : BasePropertyEditorContentExtractorTest<TextBoxContentExtractor>
    {
        /// <inheritdoc/>
        public override string EditorAlias => Cms.Core.Constants.PropertyEditors.Aliases.TextBox;

        /// <summary>
        /// Tests <see cref="TextBoxContentExtractor.IsExtractFor"/> with <see cref="Constants.PropertyEditors.Aliases.TextArea"/>.
        /// </summary>
        [Test]
        public void IsExtractForTextAreaEditor()
        {
            var publishedPropertyType = new Mock<IPublishedPropertyType>();
            publishedPropertyType.SetupGet(x => x.EditorAlias).Returns(Constants.PropertyEditors.Aliases.TextArea);

            var result = this.Extractor!.IsExtractFor(publishedPropertyType.Object);

            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Tests <see cref="TextBoxContentExtractor.ExtractContent"/> with content set.
        /// </summary>
        [Test]
        public void ExtractContentWithContentSet()
        {
            var text = "This is some text";

            var content = this.CreateContent(text);

            var result = this.Extractor!.ExtractContent(content, Alias);

            Assert.That(result, Is.EqualTo(text));
        }
    }
}
