namespace MVCHoldem.UnitTests.Web.Areas.Admin.Controllers.PostAdminControllerTests
{
    using Moq;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Areas.Admin.Controllers;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    public class Index_Should
    {
        [Test]
        public void RenderDefaultView_WhenCalled()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            PostAdminController postAdminController = new PostAdminController(postsServiceMock.Object);

            // Act & Assert
            postAdminController
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView();
        }
    }
}
