namespace MVCHoldem.UnitTests.Services.UserServiceTests
{
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using NUnit.Framework;

    [TestFixture]
    public class FindById_Should
    {
        [Test]
        public void ReturnUser_WhenCalledWithId()
        {
            // Arrange
            var applicationUserManagerMock = new Mock<IApplicationUserManager>();
            applicationUserManagerMock.Setup(m => m.FindById(It.IsAny<string>()))
                .Returns(new User());
            UserService userService = new UserService(applicationUserManagerMock.Object);

            // Act
            var result = userService.FindById("id");

            // Assert
            Assert.IsInstanceOf<User>(result);
            applicationUserManagerMock.Verify(m => m.FindById(It.IsAny<string>()), Times.Once);
        }
    }
}
