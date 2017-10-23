namespace MVCHoldem.UnitTests.Web.Areas.Admin.Controllers.PostAdminControllerTests
{
    using System.Web.Mvc;
    using Moq;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Areas.Admin.Controllers;
    using MVCHoldem.Web.Areas.Admin.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class CreatePost_Should
    {
        [Test]
        public void CallPostServiceAddNewPostOnce_WhenCalledWithValidDataSourceRequest()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.AddNewPost(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(new Post());
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);
            var contextMock = new Mock<ControllerContext>();
            contextMock
                .SetupGet(p => p.HttpContext.User.Identity.Name)
                .Returns("username");
            postAdminController.ControllerContext = contextMock.Object;
            var postDetailsViewModel = new PostGridViewModel()
            {
                Title = string.Empty,
                Description = string.Empty,
                Content = string.Empty
            };
            var mapperServiceMock = new Mock<IMappingService>();
            mapperServiceMock
                .Setup(x => x.Map<PostGridViewModel>(It.IsAny<Post>()))
                .Returns(postDetailsViewModel);
            MappingService.Provider = mapperServiceMock.Object;

            // Act
            postAdminController.CreatePost(postDetailsViewModel);

            // Assert
            postsServiceMock.Verify(
                m => m.AddNewPost(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Test]
        public void NeverCallPostServiceAddNewPost_WhenCalledWithInvalidDataSourceRequest()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.AddNewPost(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(new Post());
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);

            // Act
            postAdminController.CreatePost(null);

            // Assert
            postsServiceMock.Verify(
                m => m.AddNewPost(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        [Test]
        public void ShouldReturnJson_WhenCalledWithValidDataSourceRequest()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.AddNewPost(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(new Post());
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);
            var contextMock = new Mock<ControllerContext>();
            contextMock
                .SetupGet(p => p.HttpContext.User.Identity.Name)
                .Returns("username");
            postAdminController.ControllerContext = contextMock.Object;
            var postDetailsViewModel = new PostGridViewModel()
            {
                Title = string.Empty,
                Description = string.Empty,
                Content = string.Empty
            };
            var mapperServiceMock = new Mock<IMappingService>();
            mapperServiceMock
                .Setup(x => x.Map<PostGridViewModel>(It.IsAny<Post>()))
                .Returns(postDetailsViewModel);
            MappingService.Provider = mapperServiceMock.Object;

            // Act & Assert
            postAdminController
                .WithCallTo(c => c.CreatePost(postDetailsViewModel))
                .ShouldReturnJson();
        }
    }
}
