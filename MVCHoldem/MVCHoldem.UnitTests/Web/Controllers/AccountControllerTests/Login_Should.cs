namespace MVCHoldem.UnitTests.Web.Controllers.AccountControllerTests
{
    using Microsoft.AspNet.Identity.Owin;
    using Moq;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels;
    using NUnit.Framework;
    using System.Web.Mvc;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Login_Should
    {
        [Test]
        public void RenderDefaultViewWithReturnUrlInViewBag()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var authServiceMock = new Mock<IAuthService>();
            var userServiceMock = new Mock<IUserService>();
            AccountController accountController = new AccountController(authServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRenderView("Login");
            Assert.AreEqual(returnUrl, accountController.ViewBag.ReturnUrl);
        }

        [Test]
        public void RedirectToGivenUrl_WhenLoginStatusIsSuccess()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock
                .Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(SignInStatus.Success);
            var userServiceMock = new Mock<IUserService>();
            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(uhm => uhm.IsLocalUrl(returnUrl)).Returns(true);
            var loginViewModel = new LoginViewModel();
            AccountController accountController = new AccountController(authServiceMock.Object, userServiceMock.Object);
            accountController.Url = urlHelperMock.Object;

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRedirectTo(returnUrl);
        }

        [Test]
        public void RenderLockoutView_WhenLoginStatusIsLockedOut()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock
                .Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(SignInStatus.LockedOut);
            var userServiceMock = new Mock<IUserService>();
            var loginViewModel = new LoginViewModel();
            AccountController accountController = new AccountController(authServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRenderView("Lockout");
        }

        [Test]
        public void RenderLoginViewWithModelError_WhenSingInStatusIsFailure()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var authServiceMock = new Mock<IAuthService>();
            authServiceMock
                .Setup(s => s.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(SignInStatus.Failure);
            var userServiceMock = new Mock<IUserService>();
            var loginViewModel = new LoginViewModel();
            AccountController accountController = new AccountController(authServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRenderView("Login")
                .WithModel<LoginViewModel>()
                .AndModelError(string.Empty)
                .Containing("Invalid login attempt.");
        }
    }
}
