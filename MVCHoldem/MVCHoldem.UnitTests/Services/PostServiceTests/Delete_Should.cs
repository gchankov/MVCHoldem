namespace MVCHoldem.UnitTests.Services.PostServiceTests
{
    using System;
    using Moq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using NUnit.Framework;

    [TestFixture]
    public class Delete_Should
    {
        [Test]
        public void CallPostsDbSetDeleteOnce_WhenCalledWithValidPost()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.Delete(It.IsAny<Post>()));
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act
            postService.Delete(new Post());

            // Assert
            setWrapperMock.Verify(m => m.Delete(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void ShouldThrow_WhenCalledWithNullPost()
        {
            // Arrange
            Post post = null;
            var userServiceMock = new Mock<IUserService>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.Delete(It.IsAny<Post>()));
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => postService
                .Delete(post));
        }
    }
}
