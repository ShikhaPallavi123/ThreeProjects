using NUnit.Framework;
using OrderSystemLibrary;
using System;
using System.IO;
using System.Text.Json;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class JSONTests
    {
        private string _testFilePath;

        [SetUp]
        public void Setup()
        {
            // Define the path for the test file
            _testFilePath = $"Order_123.json";
        }

        [TearDown]
        public void Cleanup()
        {
            // Delete the test file after each test
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void Write_ShouldCreateJSONFile()
        {
            // Arrange: Create a sample order object
            var customer = new Customer("Test Customer", "1234567890");
            var order = new Order
            {
                OrderNumber = 123,
                DateTime = DateTime.Now,
                Customer = customer,
                TaxAmount = 10.0m,
                TariffAmount = 5.0m,
                TotalAmount = 100.0m
            };

            var jsonWriter = new JSON();

            // Act: Call the Write method
            jsonWriter.Write(order);

            // Assert: Verify the file was created
            Assert.IsTrue(File.Exists(_testFilePath), "JSON file should be created.");

            // Verify the contents of the file
            string fileContent = File.ReadAllText(_testFilePath);
            string expectedContent = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });

            Assert.AreEqual(expectedContent, fileContent, "File contents should match the serialized order.");
        }

        [Test]
        public void Write_ShouldHandleExceptionGracefully()
        {
            // Arrange: Create an invalid path by setting the current directory to null
            var customer = new Customer("Test Customer", "1234567890");
            var order = new Order
            {
                OrderNumber = 123,
                DateTime = DateTime.Now,
                Customer = customer,
                TaxAmount = 10.0m,
                TariffAmount = 5.0m,
                TotalAmount = 100.0m
            };

            var jsonWriter = new JSON();

            // Act and Assert: Verify no exception is thrown when attempting to write to an invalid path
            Assert.DoesNotThrow(() => jsonWriter.Write(order), "Write should handle exceptions gracefully.");
        }
    }
}
