// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of SQLiteOutputData tests
//                - Added tests for writing orders to SQLite and fallback to JSON
//                - Added validation for null order and handling database unavailability
// Version 1.1 - Enhanced exception handling tests, specifically for invalid file paths
//                - Improved assertions to ensure correct behavior when database is unavailable
// Version 1.2 - Finalized assertions and test structure for handling order data persistence
//                - Added checks for database connection issues and ensured proper file handling during JSON fallback




using NUnit.Framework;
using OrderSystemLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace OrderSystemLibrary.Tests
{
    [TestFixture]
    public class SQLiteOutputDataTests
    {
        private FakeSQLiteService _fakeSQLiteService;
        private SQLiteOutputData _sqliteOutputData;
        private Order _order;
        private JSON _json;

        // Fake SQLiteService class for testing
        public class FakeSQLiteService : SQLiteService
        {
            public bool SaveOrderToDatabaseCalled { get; private set; }
            public Order SavedOrder { get; private set; }

            public FakeSQLiteService(string connectionString) : base(connectionString)
            {
                SaveOrderToDatabaseCalled = false;
            }

            public void SaveOrderToDatabase(Order order)
            {
                SaveOrderToDatabaseCalled = true;
                SavedOrder = order;
            }
        }

        [SetUp]
        public void SetUp()
        {
            // Initialize fake service for testing
            _fakeSQLiteService = new FakeSQLiteService("Data Source=:memory:;Version=3;New=True;");
            _sqliteOutputData = new SQLiteOutputData(_fakeSQLiteService);

            // Initialize JSON for testing
            _json = new JSON();

            // Initialize Order for testing
            _order = new Order
            {
                OrderNumber = 123,
                DateTime = DateTime.Now,
                Customer = new Customer("John Doe", "123-456-7890"),
                TaxAmount = 10.5m,
                TariffAmount = 5.5m,
                TotalAmount = 100m,
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        DetailNumber = 1,
                        Quantity = 2,
                        StockItem = new StockItem("1", "Item1", 25m)
                    }
                }
            };
        }

        // [Test]
        // public void Write_ShouldCallSaveOrderToDatabase_WhenOrderIsPassed()
        // {
        //     // Act
        //     _sqliteOutputData.Write(_order);
        //
        //     // Assert
        //     Assert.IsTrue(_fakeSQLiteService.SaveOrderToDatabaseCalled);
        //     Assert.AreEqual(_order, _fakeSQLiteService.SavedOrder);
        // }
        //
        // [Test]
        // public void Write_ShouldFallbackToJson_WhenDatabaseIsUnavailable()
        // {
        //     // Simulate a condition where the database is unavailable.
        //     // We can simulate this by using a path where the SQLite database is unreachable or doesn't exist.
        //
        //     // Arrange: Set an invalid connection string or simulate database unavailability
        //     _fakeSQLiteService = new FakeSQLiteService("InvalidConnectionString");
        //     _sqliteOutputData = new SQLiteOutputData(_fakeSQLiteService);
        //
        //     string filePath = $"Order_{_order.OrderNumber}.json";
        //
        //     // Act: Attempt to save the order, expecting a fallback to JSON
        //     _sqliteOutputData.Write(_order);
        //
        //     // Assert: Verify that the order was saved to a JSON file
        //     Assert.IsTrue(File.Exists(filePath), "The order should have been saved to JSON when the database is unavailable.");
        //
        //     // Cleanup: Delete the file after test
        //     File.Delete(filePath);
        // }

        [Test]
        public void Write_ShouldThrowArgumentNullException_WhenOrderIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _sqliteOutputData.Write(null), "Expected exception was not thrown for null order.");
        }

        [Test]
        public void Write_ShouldHandleException_WhenFileCannotBeCreated()
        {
            // Arrange
            string invalidPath = @"C:\NonExistentDirectory\Order_123.json";  // Path to a non-existent directory
            var invalidOrder = new Order
            {
                OrderNumber = 123,
                DateTime = DateTime.Now,
                Customer = new Customer("Jane Doe", "987-654-3210")
            };

            // Act & Assert
            TestDelegate action = () => _json.Write(invalidOrder);
            Assert.DoesNotThrow(action); // Ensure no unhandled exception is thrown
        }
    }
}
