using Umbraco.Cms.Core.Strings;
using Umbraco.Community.Indekso.Extractors.PropertyEditors;
using Umbraco.Extensions;

namespace Umbraco.Community.Indekso.Extractors.Tests.PropertyEditors
{
    /// <summary>
    /// Tests for the <see cref="TinyMceContentExtractor"/>.
    /// </summary>
    [TestFixture]
    internal sealed class TinyMceContentExtractorTests : BasePropertyEditorContentExtractorTest<TinyMceContentExtractor>
    {
        /// <inheritdoc/>
        public override string EditorAlias => Cms.Core.Constants.PropertyEditors.Aliases.TinyMce;

        /// <summary>
        /// Tests <see cref="TinyMceContentExtractor.ExtractContent"/> with content set.
        /// </summary>
        [Test]
        public void ExtractContentWithContentSet()
        {
            var text = "This is <b>some</b> text";

            var content = this.CreateContent(new HtmlEncodedString(text));

            var result = this.Extractor!.ExtractContent(content, Alias);

            Assert.That(result, Is.EqualTo(text.StripHtml()));
        }
    }
}
