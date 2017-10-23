namespace MVCHoldem.UnitTests.Web.Areas.Admin.Controllers.PostAdminControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Kendo.Mvc.UI;
    using Moq;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Areas.Admin.Controllers;
    using MVCHoldem.Web.Areas.Admin.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class ReadPosts_Should
    {
        [Test]
        public void CallPostServiceAllIncludingDeletedOnce_WhenCalledWithValidDataSourceRequest()
        {
            // Arrange
            var postDetailsViewModel = new PostGridViewModel();
            var mapperServiceMock = new Mock<IMappingService>();
            mapperServiceMock.Setup(x => x.Map<PostGridViewModel>(It.IsAny<Post>())).Returns(postDetailsViewModel);
            MappingService.Provider = mapperServiceMock.Object;
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllIncludingDeleted())
                .Returns(postEnumerable);
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);

            // Act
            postAdminController.ReadPosts(new DataSourceRequest());

            // Assert
            postsServiceMock.Verify(m => m.AllIncludingDeleted(), Times.Once);
        }

        [Test]
        public void ShouldThrowNullArgumentExcepthin_WhenCalledWithNulldDataSourceRequest()
        {
            // Arrange
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllIncludingDeleted())
                .Returns(postEnumerable);
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => postAdminController.ReadPosts(null));
        }

        [Test]
        public void ShouldReturnJson_WhenCalledWithValidDataSourceRequest()
        {
            // Arrange
            var postDetailsViewModel = new PostGridViewModel();
            var mapperServiceMock = new Mock<IMappingService>();
            mapperServiceMock.Setup(x => x.Map<PostGridViewModel>(It.IsAny<Post>())).Returns(postDetailsViewModel);
            MappingService.Provider = mapperServiceMock.Object;
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllIncludingDeleted())
                .Returns(postEnumerable);
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);

            // Act & Assert
            postAdminController
                .WithCallTo(c => c.ReadPosts(new DataSourceRequest()))
                .ShouldReturnJson();
        }
    }
}
