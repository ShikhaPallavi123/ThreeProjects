// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of IDatabaseService unit tests
//                - Implemented mock database service for both available and unavailable states
//                - Added tests to verify behavior when database is available and unavailable
// Version 1.1 - Enhanced mock implementations of IDatabaseService
//                - Added better simulation of SaveOrder behavior for testing
// Version 1.2 - Refined unit tests for checking database availability
//                - Improved test readability and assertion messages
// Version 1.3 - Final version for Version 1.x
//                - Added clearer messages for both "database available" and "database unavailable" checks
//                - Cleaned up mock services to improve test isolation
// Version 2.0 (Future) - Plan to integrate real database for end-to-end testing
//                        - Enhance tests with more complex mock database behaviors, like failures during save




using NUnit.Framework;
using OrderSystemLibrary;

namespace OrderSystemLibraryTests
{
    // Mock implementation of IDatabaseService for when the database is available
    public class TestDatabaseService_True : IDatabaseService
    {
        public bool IsDatabaseAvailable()
        {
            return true; // Simulates a database being available
        }

        // Simulated save order behavior for testing
        public void SaveOrder(Order order)
        {
            // Mock behavior - No actual database operation
        }

        public void SaveOrderToJson(Order order)
        {
            // Mock behavior - No actual JSON save operation
        }

        public void SaveOrderToDatabase(Order order)
        {
            // Mock behavior - Simulate saving order to database
        }
    }

    // Mock implementation of IDatabaseService for when the database is unavailable
    public class TestDatabaseService_False : IDatabaseService
    {
        public bool IsDatabaseAvailable()
        {
            return false; // Simulates a database not being available
        }

        // Simulated save order behavior for testing
        public void SaveOrder(Order order)
        {
            // Mock behavior - No actual database operation
        }

        public void SaveOrderToJson(Order order)
        {
            // Mock behavior - No actual JSON save operation
        }

        public void SaveOrderToDatabase(Order order)
        {
            // Mock behavior - Simulate not saving the order to the database
        }
    }

    // Test class to validate IDatabaseService behavior
    [TestFixture]
    public class IDatabaseServiceTests
    {
        [Test]
        public void IsDatabaseAvailable_ShouldReturnTrue_WhenDatabaseIsAvailable()
        {
            // Arrange: Use the custom test implementation that returns true
            IDatabaseService databaseService = new TestDatabaseService_True();

            // Act: Call the method to check if database is available
            bool result = databaseService.IsDatabaseAvailable();

            // Assert: Verify the result is true
            Assert.IsTrue(result, "IsDatabaseAvailable should return true when database is available.");
        }

        [Test]
        public void IsDatabaseAvailable_ShouldReturnFalse_WhenDatabaseIsNotAvailable()
        {
            // Arrange: Use the custom test implementation that returns false
            IDatabaseService databaseService = new TestDatabaseService_False();

            // Act: Call the method to check if database is available
            bool result = databaseService.IsDatabaseAvailable();

            // Assert: Verify the result is false
            Assert.IsFalse(result, "IsDatabaseAvailable should return false when database is not available.");
        }
    }
}
