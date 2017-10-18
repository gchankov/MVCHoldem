namespace MVCHoldem.UnitTests.Services.UserServiceTests
{
    using Microsoft.AspNet.Identity;
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Services;
    using MVCHoldem.UnitTests.Wrappers;
    using NUnit.Framework;

    [TestFixture]
    public class ChangePassword_Should
    {
        [Test]
        public void ReturnIdentityResult_WhenCalledWithUserIdCurrentPasswordAndNewPassword()
        {
            // Arrange
            var applicationUserManagerMock = new Mock<IApplicationUserManager>();
            var identityResultWrapper = new IdentityResultWrapper(true);
            applicationUserManagerMock.Setup(m => m.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(identityResultWrapper.GetIdentityResult());
            UserService userService = new UserService(applicationUserManagerMock.Object);

            // Act
            var result = userService.ChangePassword("id", "current password", "new password");

            // Assert
            Assert.IsInstanceOf<IdentityResult>(result);
            applicationUserManagerMock.Verify(m => m.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
