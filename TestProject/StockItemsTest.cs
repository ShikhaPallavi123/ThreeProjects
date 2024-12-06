using NUnit.Framework;
using OrderSystemLibrary;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class StockItemTests
    {
        [Test]
        public void StockItem_Constructor_ShouldSetProperties()
        {
            // Arrange
            string stockID = "S123";
            string name = "Laptop";
            decimal price = 999.99m;

            // Act
            StockItem stockItem = new StockItem(stockID, name, price);

            // Assert
            Assert.AreEqual(stockID, stockItem.StockID);
            Assert.AreEqual(name, stockItem.Name);
            Assert.AreEqual(price, stockItem.Price);
        }

        [Test]
        public void StockItem_CopyConstructor_ShouldCreateIdenticalObject()
        {
            // Arrange
            string stockID = "S123";
            string name = "Laptop";
            decimal price = 999.99m;

            StockItem originalStockItem = new StockItem(stockID, name, price);

            // Act
            StockItem copiedStockItem = new StockItem(originalStockItem);

            // Assert
            Assert.AreEqual(originalStockItem.StockID, copiedStockItem.StockID);
            Assert.AreEqual(originalStockItem.Name, copiedStockItem.Name);
            Assert.AreEqual(originalStockItem.Price, copiedStockItem.Price);
        }
    }
}