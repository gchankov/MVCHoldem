namespace MVCHoldem.UnitTests.Services.PostServiceTests
{
    using System;
    using Moq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Services;
    using MVCHoldem.Services.Contracts;
    using NUnit.Framework;

    [TestFixture]
    public class AddNewPost_Should
    {
        [Test]
        public void CallFindByUserNameAndAddOnce_WhenCalledWithExistingUserName()
        {
            // Arrange
            var postTitle = "Post title";
            var postDescription = "Post description";
            var postContent = "Post content.";
            var postAuthor = "Post Author";
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.FindByUserName(It.IsAny<string>()))
                .Returns(new User() { UserName = postAuthor });
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.Add(It.IsAny<Post>()));
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act
            var result = postService.AddNewPost(postTitle, postDescription, postContent, postAuthor);

            // Assert
            userServiceMock.Verify(m => m.FindByUserName(It.IsAny<string>()), Times.Once);
            setWrapperMock.Verify(m => m.Add(It.IsAny<Post>()), Times.Once);
            Assert.AreEqual(postTitle, result.Title);
            Assert.AreEqual(postDescription, result.Description);
            Assert.AreEqual(postContent, result.Content);
            Assert.AreEqual(postAuthor, result.Author.UserName);
        }

        [Test]
        public void ShouldThrow_WhenAuthorNull()
        {
            // Arrange
            var postTitle = "Post title";
            var postDescription = "Post description";
            var postContent = "Post content.";
            string postAuthor = null;
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(m => m.FindByUserName(It.IsAny<string>()))
                .Returns(new User() { UserName = postAuthor });
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.Add(It.IsAny<Post>()));
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => postService
                .AddNewPost(postTitle, postDescription, postContent, postAuthor));
        }

        [Test]
        public void ShouldThrow_WhenUserServiceFindByUserNameReturnsNull()
        {
            // Arrange
            var postTitle = "Post title";
            var postDescription = "Post description";
            var postContent = "Post content.";
            var postAuthor = "Post Author";
            var userServiceMock = new Mock<IUserService>();
            User user = null;
            userServiceMock.Setup(m => m.FindByUserName(It.IsAny<string>()))
                .Returns(user);
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            setWrapperMock.Setup(m => m.Add(It.IsAny<Post>()));
            PostService postService = new PostService(setWrapperMock.Object, userServiceMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => postService
                .AddNewPost(postTitle, postDescription, postContent, postAuthor));
        }
    }
}
