using NUnit.Framework;
using OrderSystemLibrary;
using System;
using System.Collections.Generic;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class OrderTests
    {
        [Test]
        public void AddOrderDetail_ShouldAddDetailToOrder()
        {
            // Arrange
            var order = new Order();
            var stockItem = new StockItem("S123", "Laptop", 1000m);
            var detail = new OrderDetail
            {
                OrderNumber = 1,
                DetailNumber = 1,
                StockItem = stockItem,
                Quantity = 2,
                IsElectronic = true
            };

            // Act
            order.AddOrderDetail(detail);

            // Assert
            Assert.AreEqual(1, order.OrderDetails.Count, "Order should contain one detail.");
            Assert.AreEqual(detail, order.OrderDetails[0], "The added detail should match the input.");
        }

        [Test]
        public void CalculateTotalAmount_ShouldCalculateCorrectTotal()
        {
            // Arrange
            var customer = new Customer("John Doe", "1234567890");
            var order = new Order
            {
                OrderNumber = 101,
                DateTime = DateTime.Now,
                Customer = customer
            };

            var stockItem1 = new StockItem("S123", "Laptop", 1000m);
            var stockItem2 = new StockItem("S124", "Headphones", 200m);

            var detail1 = new OrderDetail
            {
                StockItem = stockItem1,
                Quantity = 1,
                IsElectronic = true
            };

            var detail2 = new OrderDetail
            {
                StockItem = stockItem2,
                Quantity = 2,
                IsElectronic = false
            };

            order.AddOrderDetail(detail1);
            order.AddOrderDetail(detail2);

            // Act
            order.CalculateTotalAmount();

            // Assert
            decimal expectedTotal = (1000m * 1) + (200m * 2); // Total price of items
            decimal expectedTariff = 1000m * 0.05m;           // 5% tariff on electronic item (Laptop)
            decimal expectedTax = expectedTotal * 0.10m;      // 10% tax on total price before tariff
            decimal expectedTotalAmount = expectedTotal + expectedTax + expectedTariff;

            Assert.AreEqual(expectedTax, order.TaxAmount, "Tax amount should be correctly calculated.");
            Assert.AreEqual(expectedTariff, order.TariffAmount, "Tariff amount should be correctly calculated for electronic items.");
            Assert.AreEqual(expectedTotalAmount, order.TotalAmount, "Total amount should include base total, tax, and tariff.");
        }

        [Test]
        public void CopyConstructor_ShouldCreateDeepCopy()
        {
            // Arrange
            var customer = new Customer("John Doe", "1234567890");
            var originalOrder = new Order
            {
                OrderNumber = 101,
                DateTime = DateTime.Now,
                Customer = customer
            };

            var stockItem = new StockItem("S123", "Laptop", 1000m);
            var detail = new OrderDetail
            {
                OrderNumber = 101,
                DetailNumber = 1,
                StockItem = stockItem,
                Quantity = 1,
                IsElectronic = true
            };

            originalOrder.AddOrderDetail(detail);

            // Act
            var copiedOrder = new Order(originalOrder);

            // Assert
            Assert.AreEqual(originalOrder.OrderNumber, copiedOrder.OrderNumber, "Order number should match.");
            Assert.AreEqual(originalOrder.Customer.Name, copiedOrder.Customer.Name, "Customer name should match.");
            Assert.AreEqual(originalOrder.OrderDetails.Count, copiedOrder.OrderDetails.Count, "Order details count should match.");

            // Verify deep copy by checking references
            Assert.AreNotSame(originalOrder.Customer, copiedOrder.Customer, "Customer objects should be different instances.");
            Assert.AreNotSame(originalOrder.OrderDetails[0], copiedOrder.OrderDetails[0], "OrderDetail objects should be different instances.");
        }
    }
}
