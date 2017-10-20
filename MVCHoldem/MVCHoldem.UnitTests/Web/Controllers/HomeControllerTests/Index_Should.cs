namespace MVCHoldem.UnitTests.Controllers.HomeControllerTests
{
    using AutoMapper;
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
        public void RenderDefaultViewWithHomeViewModel_WhenCalled()
        {
            // Arrange
            var postServiceMock = new Mock<IPostService>();
            HomeController homeController = new HomeController(postServiceMock.Object);

            // Act & Assert
            homeController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<HomeViewModel>();
        }
    }
}
