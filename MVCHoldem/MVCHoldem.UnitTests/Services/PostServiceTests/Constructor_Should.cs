namespace MVCHoldem.UnitTests.Services.PostServiceTests
{
    using System;
    using Moq;
    using MVCHoldem.Data.Contracts;
    using MVCHoldem.Data.Models;
    using MVCHoldem.Data.SaveChanges;
    using MVCHoldem.Data.SetWrappers;
    using MVCHoldem.Services;
    using NUnit.Framework;

    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEfDbSetWrapperIsNull()
        {
            // Arrange
            EfDbSetWrapper<Post> postsDbSet = null;
            var contextSaveChanges = new Mock<IEfDbContextSaveChanges>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(postsDbSet, contextSaveChanges.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenEfDbContextSaveChangesIsNull()
        {
            // Arrange
            var setWrapperMock = new Mock<IEfDbSetWrapper<Post>>();
            EfDbContextSaveChanges contextSaveChanges = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PostService(setWrapperMock.Object, contextSaveChanges));
        }
    }
}
