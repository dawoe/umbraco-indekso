// <copyright file="ContentTypeContentAppFactoryTests.cs" company="Umbraco Community">
// Copyright (c) Dave Woestenborghs
// </copyright>

using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Community.Indekso.BackOffice.Components;

namespace Umbraco.Community.Indekso.BackOffice.Tests.Components
{
    /// <summary>
    /// Tests for the <see cref="ContentTypeContentAppFactory"/>.
    /// </summary>
    [TestFixture]
    internal sealed class ContentTypeContentAppFactoryTests
    {
        private ContentTypeContentAppFactory appFactory = null!;

        /// <summary>
        /// Setup for each test.
        /// </summary>
        [SetUp]
        public void Setup() => this.appFactory = new ContentTypeContentAppFactory();

        /// <summary>
        /// Test that no content app is created when passed object is not a content type.
        /// </summary>
        [Test]
        public void NoContentAppShouldBeCreatedWhenONotContentType()
        {
            var result =
                this.appFactory.GetContentAppFor(Mock.Of<IContent>(), Mock.Of<IEnumerable<IReadOnlyUserGroup>>());

            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Tests that no content app is created when it is a new content type.
        /// </summary>
        [Test]
        public void NoContentAppShouldBeCreatedWhenContentTypeIsNew()
        {
            var contentTypeMock = new Mock<IContentType>();
            contentTypeMock.SetupGet(x => x.HasIdentity).Returns(false);

            var result =
                this.appFactory.GetContentAppFor(contentTypeMock.Object, Mock.Of<IEnumerable<IReadOnlyUserGroup>>());

            Assert.That(result, Is.Null);

            contentTypeMock.VerifyGet(x => x.HasIdentity, Times.Once);
        }

        /// <summary>
        /// Tests that a content app is created for existing doc types.
        /// </summary>
        [Test]
        public void ContentAppShouldBeCreatedWhenContentTypeIsNotNew()
        {
            var contentTypeMock = new Mock<IContentType>();
            contentTypeMock.SetupGet(x => x.HasIdentity).Returns(true);

            var result =
                this.appFactory.GetContentAppFor(contentTypeMock.Object, Mock.Of<IEnumerable<IReadOnlyUserGroup>>());

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Alias, Is.EqualTo("indeksoContentTypeContentApp"));
                Assert.That(result!.Icon, Is.EqualTo("icon-search"));
                Assert.That(result!.Name, Is.EqualTo("Indekso"));
                Assert.That(result!.View, Is.EqualTo("/App_Plugins/Indekso/app.html"));
                Assert.That(result.Active, Is.False);
                Assert.That(result.Badge, Is.Null);
                Assert.That(result.ViewModel, Is.Null);
            });

            contentTypeMock.VerifyGet(x => x.HasIdentity, Times.Once);
        }
    }
}
