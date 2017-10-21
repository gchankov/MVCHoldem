namespace MVCHoldem.UnitTests.Services.SignInServiceTests
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using NUnit.Framework;

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
            SignInService authService = new SignInService(applicationSignInManagerMock.Object, applicationUserManagerMock.Object);

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
            SignInService authService = new SignInService(applicationSignInManagerMock.Object, applicationUserManagerMock.Object);

            // Act
            authService.Login("user id", false, false);

            // Assert
            applicationUserManagerMock.Verify(m => m.FindById(It.IsAny<string>()), Times.Once);
            applicationSignInManagerMock.Verify(m => m.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}
