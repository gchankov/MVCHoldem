namespace MVCHoldem.UnitTests.Services.UserServiceTests
{
    using Microsoft.AspNet.Identity;
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.UnitTests.Wrappers;
    using NUnit.Framework;

    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ReturnIdentityResult_WhenCalledWithEmailAndPassword()
        {
            // Arrange
            var applicationUserManagerMock = new Mock<IApplicationUserManager>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<User>>();
            var identityResultWrapper = new IdentityResultWrapper(true);
            applicationUserManagerMock.Setup(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(identityResultWrapper.GetIdentityResult());
            UserService userService = new UserService(applicationUserManagerMock.Object, setWrapperMock.Object);

            // Act
            var result = userService.Create("username", "email", "password");

            // Assert
            Assert.IsInstanceOf<IdentityResult>(result);
            applicationUserManagerMock.Verify(m => m.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
