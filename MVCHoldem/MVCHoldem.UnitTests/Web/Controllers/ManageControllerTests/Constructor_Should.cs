namespace MVCHoldem.UnitTests.Web.Controllers.ManageControllerTests
{
    using System;
    using MVCHoldem.Services;
    using MVCHoldem.Web.Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenUserServiceIsNull()
        {
            // Arrange
            UserService userService = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ManageController(userService));
        }
    }
}
