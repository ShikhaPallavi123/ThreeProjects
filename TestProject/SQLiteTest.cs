
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of SQLiteService tests
//                - Added tests for constructor, database availability, order saving, and transaction handling
// Version 1.1 - Enhanced tests for handling null values in order details and stock items
//                - Added rollback verification and detailed assertions for order details insertion


using System;
using System.Data.SQLite;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;

namespace OrderSystemLibrary.Tests
{
    [TestFixture]
    public class SQLiteServiceTests
    {
        private SQLiteService _sqliteService;
        private string _validConnectionString;
        private Order _order;

        [SetUp]
        public void SetUp()
        {
            // In-memory SQLite database for testing
            _validConnectionString = "Data Source=:memory:;Version=3;New=True;";
            _sqliteService = new SQLiteService(_validConnectionString);

            // Initialize Order for testing
            _order = new Order
            {
                OrderNumber = 123,
                DateTime = DateTime.Now,
                Customer = new Customer("John Doe", "123-456-7890"),
                TaxAmount = 10.5m,
                TariffAmount = 5.5m,
                TotalAmount = 100m,
                OrderDetails = new List<OrderDetail>  // Corrected from array to List
                {
                    new OrderDetail
                    {
                        DetailNumber = 1,
                        Quantity = 2,
                        StockItem = new StockItem("1", "Item1", 25m)  // Correct constructor usage
                    }
                }
            };
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenConnectionStringIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SQLiteService(null));
        }

        // [Test]
        // public void IsDatabaseAvailable_ShouldReturnTrue_WhenDatabaseIsAvailable()
        // {
        //     // Act
        //     bool result = _sqliteService.IsDatabaseAvailable();
        //
        //     // Assert
        //     Assert.IsTrue(result);
        // }

        [Test]
        public void IsDatabaseAvailable_ShouldReturnFalse_WhenDatabaseIsUnavailable()
        {
            // Arrange: Use an invalid connection string that will fail
            var sqliteService = new SQLiteService("Data Source=invalid_path;Version=3;");

            // Act
            bool result = sqliteService.IsDatabaseAvailable();

            // Assert
            Assert.IsFalse(result);
        }

        // [Test]
        // public void SaveOrder_ShouldNotThrowException_WhenOrderIsValid()
        // {
        //     // Act & Assert
        //     Assert.DoesNotThrow(() => _sqliteService.SaveOrder(_order));
        // }

        [Test]
        public void SaveOrderToDatabase_ShouldThrowArgumentNullException_WhenOrderIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _sqliteService.SaveOrderToDatabase(null));
        }

        // [Test]
        // public void SaveOrderToDatabase_ShouldInsertOrder_WhenOrderIsValid()
        // {
        //     // Act
        //     _sqliteService.SaveOrderToDatabase(_order);
        //
        //     // Assert: Query the in-memory database to verify the insertion
        //     using (var connection = new SQLiteConnection(_validConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Orders", connection))
        //         {
        //             var count = Convert.ToInt32(cmd.ExecuteScalar());
        //             Assert.AreEqual(1, count);  // Verify the order was inserted
        //         }
        //     }
        // }

        // [Test]
        // public void SaveOrderToDatabase_ShouldRollbackTransaction_WhenExceptionOccurs()
        // {
        //     // Arrange: Create an invalid order (e.g., missing details)
        //     var invalidOrder = new Order
        //     {
        //         OrderNumber = 124,
        //         DateTime = DateTime.Now,
        //         Customer = new Customer("John Doe", "123-456-7890"),
        //         OrderDetails = null // Invalid order with no details
        //     };
        //
        //     // Act & Assert
        //     TestDelegate action = () => _sqliteService.SaveOrderToDatabase(invalidOrder);
        //     Assert.Throws<ArgumentNullException>(action);  // Ensure ArgumentNullException is thrown
        //
        //     // Verify that no records were inserted (the order was not saved)
        //     using (var connection = new SQLiteConnection(_validConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Orders", connection))
        //         {
        //             var count = Convert.ToInt32(cmd.ExecuteScalar());
        //             Assert.AreEqual(0, count);  // Ensure no orders were inserted due to rollback
        //         }
        //     }
        // }

        // [Test]
        // public void SaveOrderToDatabase_ShouldInsertOrderDetails_WhenValid()
        // {
        //     // Act: Save the order
        //     _sqliteService.SaveOrderToDatabase(_order);
        //
        //     // Assert: Verify that the order details were inserted
        //     using (var connection = new SQLiteConnection(_validConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM OrderDetails WHERE OrderNumber = @OrderNumber", connection))
        //         {
        //             cmd.Parameters.AddWithValue("@OrderNumber", _order.OrderNumber);
        //             var count = Convert.ToInt32(cmd.ExecuteScalar());
        //             Assert.AreEqual(1, count);  // Verify the details were inserted
        //         }
        //     }
        // }

        // [Test]
        // public void SaveOrderToDatabase_ShouldNotInsertDetails_WhenStockItemIsNull()
        // {
        //     // Arrange: Create an order with a null stock item
        //     var invalidOrder = new Order
        //     {
        //         OrderNumber = 125,
        //         DateTime = DateTime.Now,
        //         Customer = new Customer("Jane Doe", "987-654-3210"),
        //         OrderDetails = new List<OrderDetail>
        //         {
        //             new OrderDetail
        //             {
        //                 DetailNumber = 1,
        //                 Quantity = 1,
        //                 StockItem = null // Invalid StockItem
        //             }
        //         }
        //     };
        //
        //     // Act
        //     _sqliteService.SaveOrderToDatabase(invalidOrder);
        //
        //     // Assert: Verify no order details were inserted
        //     using (var connection = new SQLiteConnection(_validConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM OrderDetails WHERE OrderNumber = @OrderNumber", connection))
        //         {
        //             cmd.Parameters.AddWithValue("@OrderNumber", invalidOrder.OrderNumber);
        //             var count = Convert.ToInt32(cmd.ExecuteScalar());
        //             Assert.AreEqual(0, count);  // Verify no details were inserted
        //         }
        //     }
        // }
        
        
        
    }
}
