namespace MVCHoldem.UnitTests.Services.AuthServiceTests
{
    using Microsoft.AspNet.Identity.Owin;
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using NUnit.Framework;
    using System.Threading.Tasks;

    [TestFixture]
    public class Login_Should
    {
        [Test]
        public void ReturnSignInStatus_WhenCalledWithUsernamePasswordIsPersistentAndShouldLockout()
        {
            // Arrange
            var applicationSignInManagerMock = new Mock<IApplicationSignInManager>();
            var applicationUserManagerMock = new Mock<IApplicationUserManager>();
            applicationSignInManagerMock.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInStatus.Success);
            AuthService authService = new AuthService(applicationSignInManagerMock.Object, applicationUserManagerMock.Object);

            // Act
            var result = authService.Login("username", "password", false, false);

            // Assert
            Assert.AreEqual(SignInStatus.Success, result);
            applicationSignInManagerMock.Verify(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Test]
        public void CallFindByIdAndSignInAsyncOnce_WhenCalledWithUserIdIsPersistentAndShouldLockout()
        {
            // Arrange
            var applicationSignInManagerMock = new Mock<IApplicationSignInManager>();
            var applicationUserManagerMock = new Mock<IApplicationUserManager>();
            applicationUserManagerMock.Setup(m => m.FindById(It.IsAny<string>()))
                .Returns(new User());
            applicationSignInManagerMock.Setup(m => m.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(default(object)));
            AuthService authService = new AuthService(applicationSignInManagerMock.Object, applicationUserManagerMock.Object);

            // Act
            authService.Login("user id", false, false);

            // Assert
            applicationUserManagerMock.Verify(m => m.FindById(It.IsAny<string>()), Times.Once);
            applicationSignInManagerMock.Verify(m => m.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
