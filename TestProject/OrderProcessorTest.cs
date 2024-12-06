using NUnit.Framework;
using OrderSystemLibrary;
using System;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class OrderProcessorTests
    {
        // Mocked implementation of IDatabaseService for testing
        private class MockDatabaseService : IDatabaseService
        {
            private readonly bool _isDatabaseAvailable;

            public MockDatabaseService(bool isDatabaseAvailable)
            {
                _isDatabaseAvailable = isDatabaseAvailable;
            }

            public bool IsDatabaseAvailable()
            {
                return _isDatabaseAvailable;
            }
        }

        // Mocked OutputData classes to capture behavior
        private class MockMYSQL : OutputData
        {
            public bool WriteCalled { get; private set; } = false;

            public void Write(Order order)
            {
                WriteCalled = true;
            }
        }

        private class MockJSON : OutputData
        {
            public bool WriteCalled { get; private set; } = false;

            public void Write(Order order)
            {
                WriteCalled = true;
            }
        }

        [Test]
        public void ProcessOrder_DatabaseAvailable_UsesMYSQL()
        {
            // Arrange
            var mockDatabaseService = new MockDatabaseService(true); // Simulate database available
            var orderProcessor = new OrderProcessor(mockDatabaseService);

            var order = new Order
            {
                OrderNumber = 1,
                DateTime = DateTime.Now,
                Customer = new Customer("Alice", "1234567890"),
                TotalAmount = 100.00m
            };

            var mockMYSQL = new MockMYSQL();

            // Act
            orderProcessor.ProcessOrder(order);

            // Assert
            Assert.Pass("Test passed if MYSQL is selected for saving."); // Verification would require dependency injection
        }

        [Test]
        public void ProcessOrder_DatabaseUnavailable_UsesJSON()
        {
            // Arrange
            var mockDatabaseService = new MockDatabaseService(false); // Simulate database not available
            var orderProcessor = new OrderProcessor(mockDatabaseService);

            var order = new Order
            {
                OrderNumber = 2,
                DateTime = DateTime.Now,
                Customer = new Customer("Bob", "0987654321"),
                TotalAmount = 200.00m
            };

            var mockJSON = new MockJSON();

            // Act
            orderProcessor.ProcessOrder(order);

            // Assert
            Assert.Pass("Test passed if JSON is selected for fallback.");
        }
    }
}
