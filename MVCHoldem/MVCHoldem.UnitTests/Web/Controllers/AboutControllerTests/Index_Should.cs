namespace MVCHoldem.UnitTests.Web.Controllers.AboutControllerTests
{
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderIndextView_WhenCalledWithAboutViewModel()
        {
            // Arrange
            AboutController aboutController = new AboutController();
            var aboutViewModel = new AboutViewModel();

            // Act & Assert
            aboutController
                .WithCallTo(c => c.Index(aboutViewModel))
                .ShouldRenderView("Index")
                .WithModel<AboutViewModel>();
        }
    }
}
