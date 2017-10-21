namespace MVCHoldem.UnitTests.Web.Controllers.HomeControllerTests
{
    using System;
    using MVCHoldem.Services;
    using MVCHoldem.Web.Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPostServiceIsNull()
        {
            // Arrange
            PostService postService = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HomeController(postService));
        }
    }
}
