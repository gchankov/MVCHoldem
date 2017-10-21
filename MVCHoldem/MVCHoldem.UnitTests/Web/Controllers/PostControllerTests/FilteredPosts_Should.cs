namespace MVCHoldem.UnitTests.Web.Controllers.PostControllerTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels.Post;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class FilteredPosts_Should
    {
        [Test]
        public void CallPostServiceAllWithoutDeletedOnceWithEmptyString_WhenCalledWithEmptyString()
        {
            // Arrange
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllWithoutDeleted(string.Empty))
                .Returns(postEnumerable);
            PostController postController = new PostController(postsServiceMock.Object);

            // Act
            postController.FilteredPosts(string.Empty);

            // Assert
            postsServiceMock.Verify(m => m.AllWithoutDeleted(string.Empty), Times.Once);
        }

        [Test]
        public void CallPostServiceAllWithoutDeletedOnceWithSearchTerm_WhenCalledWithSearchTerm()
        {
            // Arrange
            var searchTerm = "ab";
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllWithoutDeleted(searchTerm))
                .Returns(postEnumerable);
            PostController postController = new PostController(postsServiceMock.Object);

            // Act
            postController.FilteredPosts(searchTerm);

            // Assert
            postsServiceMock.Verify(m => m.AllWithoutDeleted(searchTerm), Times.Once);
        }

        [Test]
        public void ShouldRenderAllPostsPartial_WhenCalledWithSearchTerm()
        {
            // Arrange
            var searchTerm = "ab";
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllWithoutDeleted(searchTerm))
                .Returns(postEnumerable);
            PostController postController = new PostController(postsServiceMock.Object);

            // Act & Assert
            postController
                .WithCallTo(c => c.FilteredPosts(searchTerm))
                .ShouldRenderPartialView("_AllPostsPartial")
                .WithModel<IEnumerable<AllPostsViewModel>>();
        }
    }
}
