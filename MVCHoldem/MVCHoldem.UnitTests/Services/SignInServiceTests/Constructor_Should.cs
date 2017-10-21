namespace MVCHoldem.UnitTests.Services.SignInServiceTests
{
    using System;
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Auth.Managers;
    using MVCHoldem.Services;
    using NUnit.Framework;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenApplicationSignInManagerIsNull()
        {
            // Arrange
            ApplicationSignInManager signInManager = null;
            var userManager = new Mock<IApplicationUserManager>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SignInService(signInManager, userManager.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenApplicationUserManagerIsNull()
        {
            // Arrange
            var signInManager = new Mock<IApplicationSignInManager>();
            ApplicationUserManager userManager = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SignInService(signInManager.Object, userManager));
        }
    }
}
