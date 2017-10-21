namespace MVCHoldem.UnitTests.Controllers.HomeControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using MVCHoldem.Data.Models;
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
            HomeController homeController = new HomeController(postServiceMock.Object);

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
            List<Post> enumeralbePost = null;
            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(m => m.GetMostRecent())
                .Returns(enumeralbePost.AsEnumerable());
            HomeController homeController = new HomeController(postServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => homeController.Index());
        }
    }
}
