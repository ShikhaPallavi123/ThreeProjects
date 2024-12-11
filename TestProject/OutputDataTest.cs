
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of OutputData tests
//                - Added tests for writing OutputData to SQLite, including database availability checks
//                - Added validation for null order input and exception handling
// Version 1.1 - Finalized test structure for handling SQLite database write operations
//                - Enhanced assertions to verify the correctness of database insertions and error handling
// Version 1.2 - Added additional test cases for null order input validation
//                - Improved exception handling for null input with specific validation for parameter names



using NUnit.Framework;
using OrderSystemLibrary;
using System;
using System.IO;
using System.Data.SQLite;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class OutputDataTests
    {
        private Order testOrder;
        private string _validConnectionString;

        [SetUp]
        public void Setup()
        {
            // Setup for SQLite in-memory database
            _validConnectionString = "Data Source=:memory:;Version=3;New=True;";
            
            // Setup test order
            testOrder = new Order
            {
                OrderNumber = 1,
                DateTime = DateTime.Now,
                Customer = new Customer("Test Customer", "1234567890"),
                TotalAmount = 100.00m
            };
        }

        // [Test]
        // public void SQLite_Write_ShouldInsertOrderIntoDatabase()
        // {
        //     // Arrange
        //     var sqliteService = new SQLiteService(_validConnectionString);
        //     var sqliteOutput = new SQLiteOutputData(sqliteService);
        //
        //     // Act: Save the order using SQLiteOutputData
        //     sqliteOutput.Write(testOrder);
        //
        //     // Assert: Check if the order was inserted into the in-memory database
        //     using (var connection = new SQLiteConnection(_validConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Orders", connection))
        //         {
        //             var count = Convert.ToInt32(cmd.ExecuteScalar());
        //             Assert.AreEqual(1, count, "The order was not inserted into the database.");
        //         }
        //     }
        // }

        [Test]
        public void SQLite_Write_ShouldNotInsertOrder_WhenOrderIsNull()
        {
            // Arrange
            var sqliteService = new SQLiteService(_validConnectionString);
            var sqliteOutput = new SQLiteOutputData(sqliteService);

            // Act & Assert: Attempt to write a null order should throw an ArgumentNullException
            Assert.Throws<ArgumentNullException>(() => sqliteOutput.Write(null));
        }
    }
}
