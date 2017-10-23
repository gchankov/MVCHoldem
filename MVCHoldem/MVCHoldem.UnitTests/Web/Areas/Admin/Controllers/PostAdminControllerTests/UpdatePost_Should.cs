namespace MVCHoldem.UnitTests.Web.Areas.Admin.Controllers.PostAdminControllerTests
{
    using System;
    using Moq;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Areas.Admin.Controllers;
    using MVCHoldem.Web.Areas.Admin.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class UpdatePost_Should
    {
        [Test]
        public void CallGetByIdAndUpdateOnce_WhenCalledWithValidViewModelData()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(new Post());
            postsServiceMock
                .Setup(m => m.Update(It.IsAny<Post>()));
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);
            var postDetailsViewModel = new PostGridViewModel()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Title = string.Empty,
                Description = string.Empty,
                Content = string.Empty
            };

            // Act
            postAdminController.UpdatePost(postDetailsViewModel);

            // Assert
            postsServiceMock.Verify(m => m.GetById(It.IsAny<Guid>()), Times.Once);
            postsServiceMock.Verify(m => m.Update(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void CallGetByIdUpdateAndDeleteOnce_WhenCalledWithIsDeletedTrue()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(new Post());
            postsServiceMock
                .Setup(m => m.Update(It.IsAny<Post>()));
            postsServiceMock
                .Setup(m => m.Delete(It.IsAny<Post>()));
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);
            var postDetailsViewModel = new PostGridViewModel()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Title = string.Empty,
                Description = string.Empty,
                Content = string.Empty,
                IsDeleted = true
            };

            // Act
            postAdminController.UpdatePost(postDetailsViewModel);

            // Assert
            postsServiceMock.Verify(m => m.GetById(It.IsAny<Guid>()), Times.Once);
            postsServiceMock.Verify(m => m.Update(It.IsAny<Post>()), Times.Once);
            postsServiceMock.Verify(m => m.Delete(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void NeverCallGetByIdUpdateAndDeleteOnce_WhenCalledWithNullPostGridViewModel()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(new Post());
            postsServiceMock
                .Setup(m => m.Update(It.IsAny<Post>()));
            postsServiceMock
                .Setup(m => m.Delete(It.IsAny<Post>()));
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);

            // Act
            postAdminController.UpdatePost(null);

            // Assert
            postsServiceMock.Verify(m => m.GetById(It.IsAny<Guid>()), Times.Never);
            postsServiceMock.Verify(m => m.Update(It.IsAny<Post>()), Times.Never);
            postsServiceMock.Verify(m => m.Delete(It.IsAny<Post>()), Times.Never);
        }

        [Test]
        public void ShouldReturnJson_WhenCalledWithValidViewModelData()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock
                .Setup(m => m.GetById(It.IsAny<Guid>()))
                .Returns(new Post());
            postsServiceMock
                .Setup(m => m.Update(It.IsAny<Post>()));
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);
            var postDetailsViewModel = new PostGridViewModel()
            {
                Id = "00000000-0000-0000-0000-000000000000",
                Title = string.Empty,
                Description = string.Empty,
                Content = string.Empty
            };

            // Act & Assert
            postAdminController
                .WithCallTo(c => c.UpdatePost(postDetailsViewModel))
                .ShouldReturnJson();
        }
    }
}
