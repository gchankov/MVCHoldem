namespace MVCHoldem.UnitTests.Web.Controllers.AccountControllerTests
{
    using Moq;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.UnitTests.Wrappers;
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Register_Should
    {
        [Test]
        public void RenderRegisterView_WhenCalledWithAboutViewModel()
        {
            // Arrange
            var authServiceMock = new Mock<ISignInService>();
            var userServiceMock = new Mock<IUserService>();
            AccountController accountController = new AccountController(authServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register())
                .ShouldRenderView("Register");
        }

        [Test]
        public void RedirectToLogin_WhenCalledWithRegisterViewModelAndUserCreationIsSucceeded()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel();
            var identityResultWrapper = new IdentityResultWrapper(true);
            var authServiceMock = new Mock<ISignInService>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(s => s.Create(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(identityResultWrapper.GetIdentityResult());
            AccountController accountController = new AccountController(authServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(registerViewModel))
                .ShouldRedirectTo(c => c.Login(string.Empty));
        }

        [Test]
        public void RenderRegisterView_WhenCalledWithRegisterViewModelAndUserCreationIsNotSucceeded()
        {
            // Arrange
            var registerViewModel = new RegisterViewModel();
            var identityResultWrapper = new IdentityResultWrapper(new string[] { "Error message." });
            var authServiceMock = new Mock<ISignInService>();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock
                .Setup(s => s.Create(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(identityResultWrapper.GetIdentityResult());
            AccountController accountController = new AccountController(authServiceMock.Object, userServiceMock.Object);

            // Act & Assert
            accountController
                .WithCallTo(c => c.Register(registerViewModel))
                .ShouldRenderView("Register")
                .WithModel<RegisterViewModel>()
                .AndModelError(string.Empty)
                .Containing("Error message.");
        }
    }
}
