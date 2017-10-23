namespace MVCHoldem.UnitTests.Services.UserServiceTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using MVCHoldem.Auth.Contracts;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using NUnit.Framework;

    [TestFixture]
    public class FindByUserName_Should
    {
        [Test]
        public void CallUsersDbSetAllIncludingDeleted_WhenCalledWithValidUserName()
        {
            // Arrange
            var username = "username";
            var applicationUserManagerMock = new Mock<IApplicationUserManager>();
            var setWrapperMock = new Mock<IEfDbSetWrapper<User>>();
            setWrapperMock.Setup(m => m.AllIncludingDeleted)
                .Returns(new List<User>()
                {
                    new User()
                    {
                        UserName = username
                    }
                }
                    .AsQueryable());
            UserService userService = new UserService(applicationUserManagerMock.Object, setWrapperMock.Object);

            // Act
            var result = userService.FindByUserName(username);

            // Assert
            Assert.AreEqual(username, result.UserName);
            setWrapperMock.Verify(m => m.AllIncludingDeleted, Times.Once);
        }
    }
}
