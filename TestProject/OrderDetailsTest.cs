using NUnit.Framework;
using OrderSystemLibrary;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class OrderDetailTests
    {
        [Test]
        public void CalculateDetailTotal_ShouldReturnCorrectTotal()
        {
            // Arrange
            var stockItem = new StockItem("S123", "Laptop", 1000m);
            var orderDetail = new OrderDetail
            {
                StockItem = stockItem,
                Quantity = 2,
                IsElectronic = true
            };

            // Act
            var total = orderDetail.CalculateDetailTotal();

            // Assert
            Assert.AreEqual(2000m, total, "Detail total should be calculated as StockItem price times quantity.");
        }

        [Test]
        public void CalculateTariff_ForElectronicItem_ShouldReturnCorrectTariff()
        {
            // Arrange
            var stockItem = new StockItem("S123", "Laptop", 1000m);
            var orderDetail = new OrderDetail
            {
                StockItem = stockItem,
                Quantity = 2,
                IsElectronic = true
            };

            // Act
            var tariff = orderDetail.CalculateTariff();

            // Assert
            Assert.AreEqual(100m, tariff, "Tariff should be 5% of the detail total for electronic items.");
        }

        [Test]
        public void CalculateTariff_ForNonElectronicItem_ShouldReturnZero()
        {
            // Arrange
            var stockItem = new StockItem("S124", "Chair", 100m);
            var orderDetail = new OrderDetail
            {
                StockItem = stockItem,
                Quantity = 3,
                IsElectronic = false
            };

            // Act
            var tariff = orderDetail.CalculateTariff();

            // Assert
            Assert.AreEqual(0m, tariff, "Tariff should be zero for non-electronic items.");
        }

        [Test]
        public void CopyConstructor_ShouldCreateDeepCopy()
        {
            // Arrange
            var stockItem = new StockItem("S123", "Laptop", 1000m);
            var originalDetail = new OrderDetail
            {
                OrderNumber = 1,
                DetailNumber = 1,
                StockItem = stockItem,
                Quantity = 2,
                IsElectronic = true
            };

            // Act
            var copiedDetail = new OrderDetail(originalDetail);

            // Assert
            Assert.AreEqual(originalDetail.OrderNumber, copiedDetail.OrderNumber, "Order numbers should match.");
            Assert.AreEqual(originalDetail.DetailNumber, copiedDetail.DetailNumber, "Detail numbers should match.");
            Assert.AreEqual(originalDetail.StockItem.StockID, copiedDetail.StockItem.StockID, "StockItem IDs should match.");
            Assert.AreEqual(originalDetail.Quantity, copiedDetail.Quantity, "Quantities should match.");
            Assert.AreEqual(originalDetail.IsElectronic, copiedDetail.IsElectronic, "IsElectronic flag should match.");

            // Verify deep copy (object references should not be the same)
            Assert.AreNotSame(originalDetail.StockItem, copiedDetail.StockItem, "StockItem objects should be different instances.");
        }
    }
}
