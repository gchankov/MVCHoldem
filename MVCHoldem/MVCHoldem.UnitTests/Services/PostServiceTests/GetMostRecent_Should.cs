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
    public class GetMostRecent_Should
    {
        [Test]
        public void CallDbSetWrapperAllWithoutDeletedOnce_WhenCalled()
        {
            // Arrange
            var postsQueryable = new List<Post>() { new Post() }.AsQueryable<Post>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.AllWithoutDeleted)
                .Returns(postsQueryable);
            var contextSaveChanges = new Mock<IEfDbContextSaveChanges>();
            PostService postService = new PostService(setWrapperMock.Object, contextSaveChanges.Object);

            // Act
            var result = postService.GetMostRecent();

            // Assert
            setWrapperMock.Verify(m => m.AllWithoutDeleted, Times.Once);
        }
    }
}
