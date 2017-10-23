namespace MVCHoldem.UnitTests.Web.Areas.Admin.Controllers.PostControllerTests
{
    using System;
    using Moq;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Areas.Admin.Controllers;
    using MVCHoldem.Web.Areas.Admin.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    public class HardDeletePost_Should
    {
        [Test]
        public void CallGetByIdAndHardDeleteOnce_WhenCalledWithValidPostGridViewModel()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(new Post());
            postsServiceMock
                .Setup(m => m.HardDelete(It.IsAny<Post>()));
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);
            var postDetailsViewModel = new PostGridViewModel()
            {
                Id = "00000000-0000-0000-0000-000000000000"
            };

            // Act
            postAdminController.HardDeletePost(postDetailsViewModel);

            // Assert
            postsServiceMock.Verify(m => m.GetById(It.IsAny<Guid>()), Times.Once);
            postsServiceMock.Verify(m => m.HardDelete(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void NeverCallGetByIdAndHardDelete_WhenCalledWithInvalidPostGridViewModel()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(new Post());
            postsServiceMock
                .Setup(m => m.HardDelete(It.IsAny<Post>()));
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);

            // Act
            postAdminController.HardDeletePost(null);

            // Assert
            postsServiceMock.Verify(m => m.GetById(It.IsAny<Guid>()), Times.Never);
            postsServiceMock.Verify(m => m.HardDelete(It.IsAny<Post>()), Times.Never);
        }

        [Test]
        public void ShouldReturnJson_WhenCalledWithValidDataSourceRequest()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(new Post());
            postsServiceMock
                .Setup(m => m.HardDelete(It.IsAny<Post>()));
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);
            var postDetailsViewModel = new PostGridViewModel()
            {
                Id = "00000000-0000-0000-0000-000000000000"
            };

            // Act & Assert
            postAdminController
                .WithCallTo(c => c.HardDeletePost(postDetailsViewModel))
                .ShouldReturnJson();
        }
    }
}
