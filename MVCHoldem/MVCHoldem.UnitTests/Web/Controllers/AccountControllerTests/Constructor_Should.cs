namespace MVCHoldem.UnitTests.Web.Controllers.AccountControllerTests
{
    using System;
    using Moq;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenSingInServiceIsNull()
        {
            // Arrange
            SignInService signInService = null;
            var userServiceMock = new Mock<IUserService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(signInService, userServiceMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Arrange
            var signInServiceMock = new Mock<ISignInService>();
            UserService userService = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountController(signInServiceMock.Object, userService));
        }
    }
}
