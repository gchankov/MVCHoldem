namespace MVCHoldem.UnitTests.Web.Controllers.ChatControllerTests
{
    using MVCHoldem.Web.Controllers;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderDefaultViewWithHomeViewModelAndMostRecentPostPartialWithMostRecentPostViewModel_WhenCalled()
        {
            // Arrange
            ChatController homeController = new ChatController();

            // Act & Assert
            homeController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
