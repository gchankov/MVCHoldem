namespace MVCHoldem.UnitTests.Services.PostServiceTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using NUnit.Framework;

    [TestFixture]
    public class AllIncludingDeleted_Should
    {
        [Test]
        public void CallDbSetWrapperAllIncludingDeletedOnce_WhenCalled()
        {
            // Arrange
            var postQueryable = new List<Post>()
            {
                new Post()
            }
                .AsQueryable();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.AllIncludingDeleted)
                    .Returns(postQueryable);
            var userServiceMock = new Mock<IUserService>();
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act
            var result = postService.AllIncludingDeleted();

            // Assert
            setWrapperMock.Verify(m => m.AllIncludingDeleted, Times.Once);
        }
    }
}
