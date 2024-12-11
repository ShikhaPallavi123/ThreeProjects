using NUnit.Framework;
using OrderSystemLibrary;
using System;
using System.IO;
using System.Text.Json;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class OutputDataTests
    {
        private Order testOrder;

        [SetUp]
        public void Setup()
        {
            testOrder = new Order
            {
                OrderNumber = 1,
                DateTime = DateTime.Now,
                Customer = new Customer("Test Customer", "1234567890"),
                TotalAmount = 100.00m
            };
        }

        // [Test]
        // public void MYSQL_Write_ShouldLogToConsole()
        // {
        //     // Arrange
        //     var mysqlOutput = new MYSQL();
        //     using (var consoleOutput = new StringWriter())
        //     {
        //         Console.SetOut(consoleOutput);
        //
        //         // Act
        //         mysqlOutput.Write(testOrder);
        //
        //         // Assert
        //         string output = consoleOutput.ToString();
        //         Assert.IsTrue(output.Contains($"Order {testOrder.OrderNumber} saved to MySQL database."));
        //     }
        // }

        [Test]
        public void JSON_Write_ShouldCreateJsonFile()
        {
            // Arrange
            var jsonOutput = new JSON();
            string expectedFileName = $"Order_{testOrder.OrderNumber}.json";

            // Ensure no residual file
            if (File.Exists(expectedFileName))
            {
                File.Delete(expectedFileName);
            }

            // Act
            jsonOutput.Write(testOrder);

            // Assert
            Assert.IsTrue(File.Exists(expectedFileName), "JSON file was not created.");

            // Cleanup
            File.Delete(expectedFileName);
        }
    }
}