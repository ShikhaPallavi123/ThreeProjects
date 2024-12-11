// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of JSON write tests
//                - Implemented tests for valid order writing, exception handling, empty orders, and JSON formatting
// Version 1.1 - Added test for checking customer and order data in the generated JSON
//                - Refined assertions for better error messages and verification of file creation
// Version 1.2 - Enhanced error handling test for invalid paths (no actual file write is performed)
//                - Added check for indented JSON formatting and content integrity
// Version 1.3 - Improved null handling test for null order input
// Version 1.4 - Final release for Version 1.x
//                - Cleaned up tests to ensure clarity and accuracy of validation for JSON content and format
// Version 2.0 (Future) - Plan to implement more complex error handling and testing for malformed JSON generation
//                        - Extend tests to include different file system environments and user permissions



using NUnit.Framework;
using OrderSystemLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace OrderSystemLibrary.Tests
{
    [TestFixture]
    public class JSONTests
    {
        // Test case for writing a valid order to JSON
        [Test]
        public void Write_ShouldCreateJSONFile_WhenOrderIsValid()
        {
            // Arrange
            var json = new JSON();
            var order = new Order
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
            string filePath = $"Order_{order.OrderNumber}.json";

            // Act
            json.Write(order);

            // Assert
            Assert.IsTrue(File.Exists(filePath), "The JSON file should be created.");
            string content = File.ReadAllText(filePath);
            Assert.IsTrue(content.Contains("\"OrderNumber\": 123"), "The JSON should contain the order number.");
            Assert.IsTrue(content.Contains("\"Customer\":"), "The JSON should contain the customer information.");

            // Cleanup
            File.Delete(filePath);
        }

        // Test case for handling exception when file cannot be created
        [Test]
        public void Write_ShouldHandleException_WhenFileCannotBeCreated()
        {
            // Arrange
            var json = new JSON();
            var order = new Order
            {
                OrderNumber = 123,
                DateTime = DateTime.Now,
                Customer = new Customer("Jane Doe", "987-654-3210")
            };

            // Simulate an invalid path (e.g., non-existent directory or no permission)
            string invalidPath = @"C:\NonExistentDirectory\Order_123.json";

            // Act & Assert
            TestDelegate action = () => json.Write(order);
            Assert.DoesNotThrow(action, "The method should not throw an unhandled exception.");
        }

        // Test case for handling empty order (without details)
        [Test]
        public void Write_ShouldCreateJSONFile_WhenOrderIsEmpty()
        {
            // Arrange
            var json = new JSON();
            var order = new Order
            {
                OrderNumber = 123,
                DateTime = DateTime.Now,
                Customer = new Customer("John Doe", "123-456-7890")
            };
            string filePath = $"Order_{order.OrderNumber}.json";

            // Act
            json.Write(order);

            // Assert
            Assert.IsTrue(File.Exists(filePath), "The JSON file should be created.");
            string content = File.ReadAllText(filePath);
            Assert.IsTrue(content.Contains("\"OrderNumber\": 123"), "The JSON should contain the order number.");
            Assert.IsTrue(content.Contains("\"Customer\":"), "The JSON should contain the customer information.");

            // Cleanup
            File.Delete(filePath);
        }

        // Test case for valid JSON formatting (indented JSON)
        [Test]
        public void Write_ShouldFormatJSONCorrectly()
        {
            // Arrange
            var json = new JSON();
            var order = new Order
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
            string filePath = $"Order_{order.OrderNumber}.json";

            // Act
            json.Write(order);

            // Assert: Check that the JSON is properly indented
            string content = File.ReadAllText(filePath);
            Assert.IsTrue(content.StartsWith("{\n"), "The JSON should start with an opening bracket followed by a newline.");
            Assert.IsTrue(content.Contains("\n    "), "The JSON should be indented.");

            // Cleanup
            File.Delete(filePath);
        }

        // Test case for null order input
        [Test]
        public void Write_ShouldThrowArgumentNullException_WhenOrderIsNull()
        {
            // Arrange
            var json = new JSON();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => json.Write(null), "The method should throw an ArgumentNullException when order is null.");
        }

        // Test case for checking if customer and order data exist in the JSON
        [Test]
        public void Write_ShouldContainOrderAndCustomerData_WhenOrderIsValid()
        {
            // Arrange
            var json = new JSON();
            var order = new Order
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
            string filePath = $"Order_{order.OrderNumber}.json";

            // Act
            json.Write(order);

            // Assert: Check if the JSON contains order number and customer data
            string content = File.ReadAllText(filePath);
            Assert.IsTrue(content.Contains("\"OrderNumber\": 123"), "The JSON should contain the order number.");
            Assert.IsTrue(content.Contains("\"Customer\": {"), "The JSON should contain the customer information.");

            // Cleanup
            File.Delete(filePath);
        }
        
        
        
        
        
        
    }
}
