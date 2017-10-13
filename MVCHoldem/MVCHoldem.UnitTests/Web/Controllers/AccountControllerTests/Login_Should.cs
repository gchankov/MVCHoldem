namespace MVCHoldem.UnitTests.Web.Controllers.AccountControllerTests
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity.Owin;
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Login_Should
    {
        [Test]
        public void RenderDefaultViewWithReturnUrlInViewBag()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var signInServiceMock = new Mock<ISignInService>();
            var userServiceMock = new Mock<IUserService>();
            AccountController accountController = new AccountController(userServiceMock.Object, signInServiceMock.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(returnUrl))
                .ShouldRenderView("Login");
            Assert.AreEqual(returnUrl, accountController.ViewBag.ReturnUrl);
        }

        [Test]
        public void RedirectToGivenUrl_WhenSingInStatusIsSuccess()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var signInServiceMock = new Mock<ISignInService>();
            signInServiceMock
                .Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.Success));
            var userServiceMock = new Mock<IUserService>();
            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(uhm => uhm.IsLocalUrl(returnUrl)).Returns(true);
            var loginViewModel = new LoginViewModel();
            AccountController accountController = new AccountController(userServiceMock.Object, signInServiceMock.Object);
            accountController.Url = urlHelperMock.Object;

            // Act & Assert
            accountController
                .WithCallTo(c => c.Login(loginViewModel, returnUrl))
                .ShouldRedirectTo(returnUrl);
        }

        [Test]
        public void RenderLockoutView_WhenSingInStatusIsLockedOut()
        {
            // Arrange
            string returnUrl = "returnUrl";
            var signInServiceMock = new Mock<ISignInService>();
            signInServiceMock
                .Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.LockedOut));
            var userServiceMock = new Mock<IUserService>();
            var loginViewModel = new LoginViewModel();
            AccountController accountController = new AccountController(userServiceMock.Object, signInServiceMock.Object);

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
            var signInServiceMock = new Mock<ISignInService>();
            signInServiceMock
                .Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.Failure));
            var userServiceMock = new Mock<IUserService>();
            var loginViewModel = new LoginViewModel();
            AccountController accountController = new AccountController(userServiceMock.Object, signInServiceMock.Object);

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
