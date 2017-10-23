namespace MVCHoldem.UnitTests.Web.Areas.Admin.Controllers.PostAdminControllerTests
{
    using System;
    using MVCHoldem.Services;
    using MVCHoldem.Web.Areas.Admin.Controllers;
    using NUnit.Framework;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenPostServiceIsNull()
        {
            // Arrange
            PostService postsService = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostAdminController(postsService));
        }
    }
}
