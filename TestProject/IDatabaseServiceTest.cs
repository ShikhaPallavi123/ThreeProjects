using NUnit.Framework;
using OrderSystemLibrary;

namespace OrderSystemLibraryTests
{
    // Custom test implementation of IDatabaseService
    public class TestDatabaseService_True : IDatabaseService
    {
        public bool IsDatabaseAvailable()
        {
            return true; // Simulates a database being available
        }
    }

    public class TestDatabaseService_False : IDatabaseService
    {
        public bool IsDatabaseAvailable()
        {
            return false; // Simulates a database not being available
        }
    }

    [TestFixture]
    public class IDatabaseServiceTests
    {
        [Test]
        public void IsDatabaseAvailable_ShouldReturnTrue()
        {
            // Arrange: Use the custom test implementation that returns true
            IDatabaseService databaseService = new TestDatabaseService_True();

            // Act: Call the method
            bool result = databaseService.IsDatabaseAvailable();

            // Assert: Verify the result is true
            Assert.IsTrue(result, "IsDatabaseAvailable should return true.");
        }

        [Test]
        public void IsDatabaseAvailable_ShouldReturnFalse()
        {
            // Arrange: Use the custom test implementation that returns false
            IDatabaseService databaseService = new TestDatabaseService_False();

            // Act: Call the method
            bool result = databaseService.IsDatabaseAvailable();

            // Assert: Verify the result is false
            Assert.IsFalse(result, "IsDatabaseAvailable should return false.");
        }
    }
}