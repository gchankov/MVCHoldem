namespace MVCHoldem.UnitTests.Services.PostServiceTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using NUnit.Framework;

    [TestFixture]
    public class AllWithoutDeleted_Should
    {
        [Test]
        public void CallPostsDbSetAllWithoutDeletedOnce_WhenCalled()
        {
            // Arrange
            var postsQueryable = new List<Post>() { new Post() }.AsQueryable<Post>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.AllWithoutDeleted)
                .Returns(postsQueryable);
            var contextSaveChanges = new Mock<IEfDbContextSaveChanges>();
            PostService postService = new PostService(setWrapperMock.Object, contextSaveChanges.Object);

            // Act
            var result = postService.AllWithoutDeleted();

            // Assert
            setWrapperMock.Verify(m => m.AllWithoutDeleted, Times.Once);
        }

        [Test]
        public void ReturnOnlyPostsContainingSearchTerm_WhenCalledWithSearchTearm()
        {
            // Arrange
            var searchTerm = "ab";
            var user = new User() { UserName = "username" };
            var matchingPost = new Post()
            {
                Title = "abc",
                Description = string.Empty,
                Content = string.Empty,
                Author = user
            };
            var mismatchinggPost = new Post()
            {
                Title = "123",
                Description = string.Empty,
                Content = string.Empty,
                Author = user
            };

            var postsQueryable = new List<Post>()
            {
                matchingPost, mismatchinggPost
            }
                .AsQueryable<Post>();

            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.AllWithoutDeleted)
                .Returns(postsQueryable);
            var contextSaveChanges = new Mock<IEfDbContextSaveChanges>();
            PostService postService = new PostService(setWrapperMock.Object, contextSaveChanges.Object);

            // Act
            var resultList = postService.AllWithoutDeleted(searchTerm).ToList();

            // Assert
            Assert.AreEqual(1, resultList.Count);
            Assert.AreEqual(matchingPost, resultList[0]);
        }
    }
}
