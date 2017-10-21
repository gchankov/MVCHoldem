namespace MVCHoldem.UnitTests.Web.Controllers.PostControllerTests
{
    using System;
    using Moq;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Details_Should
    {
        [Test]
        public void ShouldCallPostServiceGetByIdOnceAndRenderPostDetailsViewModel_WhenCalled()
        {
            // Arrange
            var post = new Post();
            var postDetailsViewModel = new PostDetailsViewModel();
            var mapperServiceMock = new Mock<IMappingService>();
            mapperServiceMock.Setup(x => x.Map<PostDetailsViewModel>(It.IsAny<Post>())).Returns(postDetailsViewModel);
            MappingService.Provider = mapperServiceMock.Object;
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.GetById(post.Id))
                .Returns(post);
            PostController postController = new PostController(postsServiceMock.Object);

            // Act & Assert
            postController
                .WithCallTo(c => c.Details(post.Id))
                .ShouldRenderView("Details")
                .WithModel<PostDetailsViewModel>();
            postsServiceMock.Verify(m => m.GetById(post.Id), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenPostServiceReturnsNull()
        {
            // Arrange
            Post post = null;
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(post);
            PostController postController = new PostController(postsServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => postController.Details(It.IsAny<Guid>()));
        }
    }
}
