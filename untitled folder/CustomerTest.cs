using NUnit.Framework;
using OrderSystemLibrary;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void Customer_Constructor_ShouldSetProperties()
        {
            // Arrange
            string expectedName = "John Doe";
            string expectedPhone = "123-456-7890";

            // Act
            Customer customer = new Customer(expectedName, expectedPhone);

            // Assert
            Assert.AreEqual(expectedName, customer.Name, "Name property did not match expected value.");
            Assert.AreEqual(expectedPhone, customer.Phone, "Phone property did not match expected value.");
        }

        [Test]
        public void Customer_CopyConstructor_ShouldCopyProperties()
        {
            // Arrange
            Customer originalCustomer = new Customer("Jane Doe", "987-654-3210");

            // Act
            Customer copiedCustomer = new Customer(originalCustomer);

            // Assert
            Assert.AreEqual(originalCustomer.Name, copiedCustomer.Name, "Name property was not copied correctly.");
            Assert.AreEqual(originalCustomer.Phone, copiedCustomer.Phone, "Phone property was not copied correctly.");
            Assert.AreNotSame(originalCustomer, copiedCustomer, "Copy constructor should create a new object.");
        }

        [Test]
        public void Customer_CopyConstructor_ShouldHandleNullCustomer()
        {
            // Arrange & Act & Assert
            Assert.Throws<NullReferenceException>(() => new Customer(null));
        }
    }
}