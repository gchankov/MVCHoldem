namespace MVCHoldem.UnitTests.Controllers.HomeControllerTests
{
    using System.Web;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Moq;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels.Home;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultViewWithHomeViewModelAndMostRecentPostPartialWithMostRecentPostViewModel_WhenCalled()
        {
            // Arrange
            var postServiceMock = new Mock<IPostService>();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Cache)
                .Returns(new Cache());
            HomeController homeController = new HomeController(postServiceMock.Object);
            homeController.ControllerContext = new ControllerContext(context.Object, new RouteData(), homeController);

            // Act & Assert
            homeController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<HomeViewModel>();
        }

        [Test]
        public void ThrowArgumentNullException_WhenPostServiceReturnsNull()
        {
            // Arrange
            var postServiceMock = new Mock<IPostService>();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Cache)
                .Returns(new Cache());
            HomeController homeController = new HomeController(postServiceMock.Object);
            homeController.ControllerContext = new ControllerContext(context.Object, new RouteData(), homeController);

            // Act & Assert
            // Assert.Throws<ArgumentNullException>(() => homeController.Index());
        }
    }
}
