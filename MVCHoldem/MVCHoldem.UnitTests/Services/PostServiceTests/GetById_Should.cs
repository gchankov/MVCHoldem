namespace MVCHoldem.UnitTests.Services.PostServiceTests
{
    using Moq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using NUnit.Framework;

    [TestFixture]
    public class GetById_Should
    {
        [Test]
        public void CallDbSetWrapperGetByIdOnce_WhenCalled()
        {
            // Arrange
            var post = new Post();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.GetById(post.Id))
                .Returns(post);
            var userServiceMock = new Mock<IUserService>();
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act
            var result = postService.GetById(post.Id);

            // Assert
            setWrapperMock.Verify(m => m.GetById(post.Id), Times.Once);
            Assert.AreEqual(post, result);
        }
    }
}
