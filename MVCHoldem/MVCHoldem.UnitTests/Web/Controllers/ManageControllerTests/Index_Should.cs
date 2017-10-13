using Moq;
using MVCHoldem.Auth.Contracts;
using MVCHoldem.Web.Controllers;
using MVCHoldem.Web.Enums;
using MVCHoldem.Web.ViewModels;
using NUnit.Framework;
using System.IO;
using System.Security.Principal;
using System.Web;
using TestStack.FluentMVCTesting;

namespace MVCHoldem.UnitTests.Web.Controllers.ManageControllerTests
{
    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderIndexView_WhenCalledWithAnyManageMessageId()
        {
            // Arrange
            //var signInServiceMock = new Mock<ISignInService>();
            //var userServiceMock = new Mock<IUserService>();
            //ManageController manageController = new ManageController(userServiceMock.Object, signInServiceMock.Object);

            // Act & Assert
            //manageController
            //    .WithCallTo(c => c.Index(It.IsAny<ManageMessageId>()))
            //    .ShouldRenderView("Index")
            //    .WithModel<ManageViewModel>();
        }
    }
}
