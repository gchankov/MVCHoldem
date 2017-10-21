namespace MVCHoldem.UnitTests.Services.MappingServiceTests
{
    using System;
    using MVCHoldem.Services;
    using NUnit.Framework;

    [TestFixture]
    public class Map_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenObjectFromIsNull()
        {
            // Arrange
            var postService = new MappingService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => postService.Map<Type>(null));
        }
    }
}
