namespace MVCHoldem.UnitTests.Web.Controllers.PostControllerTests
{
    using Moq;
    using MVCHoldem.Services.Contracts;
    using MVCHoldem.Web.Controllers;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class All_Should
    {
        [Test]
        public void RenderAllPostsViewModel_WhenCalled()
        {
            // Arrange
            var postsServiceMock = new Mock<IPostService>();
            PostController postController = new PostController(postsServiceMock.Object);

            // Act & Assert
            postController
                .WithCallTo(c => c.All())
                .ShouldRenderDefaultView();
        }
    }
}
