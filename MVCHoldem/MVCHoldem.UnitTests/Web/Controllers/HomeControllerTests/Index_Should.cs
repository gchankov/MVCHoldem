namespace MVCHoldem.UnitTests.Controllers.HomeControllerTests
{
    using MVCHoldem.Web.Controllers;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WhenCalled()
        {
            // Arrange
            HomeController homeController = new HomeController();

            // Act & Assert
            homeController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
