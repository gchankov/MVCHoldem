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
    public class HardDelete_Should
    {
        [Test]
        public void CallPostsDbSetHardDeleteOnce_WhenCalledWithValidPost()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.HardDelete(It.IsAny<Post>()));
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act
            postService.HardDelete(new Post());

            // Assert
            setWrapperMock.Verify(m => m.HardDelete(It.IsAny<Post>()), Times.Once);
        }

        [Test]
        public void ShouldThrow_WhenCalledWithNullPost()
        {
            // Arrange
            Post post = null;
            var userServiceMock = new Mock<IUserService>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.HardDelete(It.IsAny<Post>()));
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => postService
                .HardDelete(post));
        }
    }
}
