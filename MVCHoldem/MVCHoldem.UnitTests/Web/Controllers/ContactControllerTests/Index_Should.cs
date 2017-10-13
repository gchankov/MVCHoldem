namespace MVCHoldem.UnitTests.Web.Controllers.ContactControllerTests
{
    using MVCHoldem.Web.Controllers;
    using MVCHoldem.Web.ViewModels;
    using NUnit.Framework;
    using TestStack.FluentMVCTesting;

    [TestFixture]
    public class Index_Should
    {
        [Test]
        public void RenderIndexView_WhenCalledWithContactViewModel()
        {
            // Arrange
            ContactController contactController = new ContactController();
            var contactViewModel = new ContactViewModel();

            // Act & Assert
            contactController
                .WithCallTo(c => c.Index(contactViewModel))
                .ShouldRenderView("Index")
                .WithModel<ContactViewModel>();
        }
    }
}
