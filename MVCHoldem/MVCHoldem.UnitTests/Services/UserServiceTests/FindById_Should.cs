namespace MVCHoldem.UnitTests.Services.UserServiceTests
{
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using NUnit.Framework;

    [TestFixture]
    public class FindById_Should
    {
        [Test]
        public void CallUserManagerFindById_WhenCalledWithValidId()
        {
            // Arrange
            var id = "id";
            var applicationUserManagerMock = new Mock<IApplicationUserManager>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<User>>();
            applicationUserManagerMock.Setup(m => m.FindById(It.IsAny<string>()))
                .Returns(new User()
                {
                    Id = id
                });
            UserService userService = new UserService(applicationUserManagerMock.Object, setWrapperMock.Object);

            // Act
            var result = userService.FindById(id);

            // Assert
            Assert.AreEqual(id, result.Id);
            applicationUserManagerMock.Verify(m => m.FindById(It.IsAny<string>()), Times.Once);
        }
    }
}
