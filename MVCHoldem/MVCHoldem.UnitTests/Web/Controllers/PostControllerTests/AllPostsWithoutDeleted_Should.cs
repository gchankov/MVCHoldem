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
    public class AllPostsWithoutDeleted_Should
    {
        [Test]
        public void CallPostServiceAllWithoutDeletedOnce_WhenCalled()
        {
            // Arrange
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllWithoutDeleted(string.Empty))
                .Returns(postEnumerable);
            PostController postController = new PostController(postsServiceMock.Object);

            // Act
            postController.AllPostsWithoutDeleted();

            // Assert
            postsServiceMock.Verify(m => m.AllWithoutDeleted(string.Empty), Times.Once);
        }

        [Test]
        public void ShouldRenderAllPostsPartial_WhenCalled()
        {
            // Arrange
            var postEnumerable = new List<Post>() { new Post() }.AsEnumerable<Post>();
            var postsServiceMock = new Mock<IPostService>();
            postsServiceMock.Setup(m => m.AllWithoutDeleted(string.Empty))
                .Returns(postEnumerable);
            PostController postController = new PostController(postsServiceMock.Object);

            // Act & Assert
            postController
                .WithCallTo(c => c.AllPostsWithoutDeleted())
                .ShouldRenderPartialView("_AllPostsPartial")
                .WithModel<IEnumerable<AllPostsViewModel>>();
        }
    }
}
