namespace MVCHoldem.UnitTests.Web.Controllers.ManageControllerTests
{
    using Moq;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Controllers;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class ChangePassword_Should
    {
        [Test]
        public void RendervChangePasswordView_WhenCalled()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var userServiceMock = new Mock<IUserService>();
            ManageController manageController = new ManageController(authServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            manageController
                .WithCallTo(c => c.ChangePassword())
                .ShouldRenderView("ChangePassword");
        }
    }
}
