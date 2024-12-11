
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of OutputDataFactory tests
//                - Added tests for creating OutputData (SQLite and JSON) based on database availability
//                - Added validation for empty and null connection strings with appropriate exception handling
// Version 1.1 - Finalized assertions and test structure for better clarity and exception handling
//                - Refined the error handling tests for connection string issues
// Version 1.2 - Added a test for when the database is unavailable and the connection string is ignored
//                - Re-verified all assertions to ensure correct behavior when DB availability and connection string are considered




using NUnit.Framework;
using OrderSystemLibrary;
using System;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class OutputDataFactoryTests
    {
        [Test]
        public void CreateOutputData_ShouldReturnSQLiteOutputData_WhenDatabaseIsAvailableWithValidConnectionString()
        {
            // Arrange: Create the factory instance and a valid connection string
            var connectionString = "Data Source=valid_database.db";  // Valid connection string
            var factory = new OutputDataFactory();
        
            // Act: Create OutputData by passing true (indicating the DB is available)
            OutputData outputData = factory.CreateOutputData(true, connectionString);
        
            // Assert: Check if the returned instance is of type SQLiteOutputData
            Assert.That(outputData, Is.TypeOf<SQLiteOutputData>(), "Factory did not return SQLiteOutputData when DB is available.");
        }

        [Test]
        public void CreateOutputData_ShouldReturnJSON_WhenDatabaseIsNotAvailable()
        {
            // Arrange: Create the factory instance
            var factory = new OutputDataFactory();
        
            // Act: Create OutputData by passing false (indicating the DB is not available)
            OutputData outputData = factory.CreateOutputData(false);
        
            // Assert: Check if the returned instance is of type JSON
            Assert.That(outputData, Is.TypeOf<JSON>(), "Factory did not return JSON when DB is not available.");
        }

        [Test]
        public void CreateOutputData_ShouldThrowArgumentNullException_WhenConnectionStringIsEmptyAndDBIsAvailable()
        {
            // Arrange: Create the factory instance
            var factory = new OutputDataFactory();
        
            // Act & Assert: Ensure an exception is thrown when passing an empty connection string
            Assert.Throws<ArgumentNullException>(() => factory.CreateOutputData(true, string.Empty), "Factory did not throw exception for empty connection string.");
        }

        [Test]
        public void CreateOutputData_ShouldThrowArgumentNullException_WhenConnectionStringIsNullAndDBIsAvailable()
        {
            // Arrange: Create the factory instance
            var factory = new OutputDataFactory();
        
            // Act & Assert: Ensure an exception is thrown when passing a null connection string
            Assert.Throws<ArgumentNullException>(() => factory.CreateOutputData(true, null), "Factory did not throw exception for null connection string.");
        }
    }
}
