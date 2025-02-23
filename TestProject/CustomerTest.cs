
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of Customer unit tests
//                - Implemented tests for constructor, copy constructor, and null handling in Customer class
//                - Added checks for correct property assignment and object copying
// Version 1.1 - Enhanced error handling in tests
//                - Improved error messages for failed assertions in tests
//                - Added clearer messages for property checks and copy constructor verification
// Version 1.2 - Refined the copy constructor null handling test
//                - Ensured proper handling of NullReferenceException when passing null to the copy constructor
// Version 1.3 - Final version for Version 1.x
//                - Improved overall structure for better clarity and reliability of unit tests
//                - All tests now have clear and concise error messages
// Version 2.0 (Future) - Plan to expand unit testing to other components of the Order System
//                        - Tests for Order, OrderDetail, and StockItem classes
//                        - Include mock database tests and integration tests for the OrderProcessor



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