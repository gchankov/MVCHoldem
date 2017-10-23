namespace MVCHoldem.UnitTests.Services.PostServiceTests
{
    using System;
    using Moq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Data.SetWrappers;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using NUnit.Framework;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEfDbSetWrapperIsNull()
        {
            // Arrange
            EfDbSetWrapper<Post> postsDbSet = null;
            var userServiceMock = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(postsDbSet, userServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Arrange
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            UserService userService = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(setWrapperMock.Object, userService));
        }
    }
}
