using NUnit.Framework;
using OrderSystemLibrary;

namespace OrderSystemLibraryTests
{
    [TestFixture]
    public class OutputDataFactoryTests
    {
        // [Test]
        // public void CreateOutputData_ShouldReturnMYSQL_WhenDatabaseIsAvailable()
        // {
        //     // Arrange: Create the factory instance
        //     var factory = new OutputDataFactory();
        //
        //     // Act: Create OutputData by passing true (indicating the DB is available)
        //     OutputData outputData = factory.CreateOutputData(true);
        //
        //     // Assert: Check if the returned instance is of type MYSQL
        //     Assert.That(outputData, Is.TypeOf<MYSQL>(), "Factory did not return instance of MYSQL when DB is available.");
        // }

        [Test]
        public void CreateOutputData_ShouldReturnJSON_WhenDatabaseIsNotAvailable()
        {
            // Arrange: Create the factory instance
            var factory = new OutputDataFactory();

            // Act: Create OutputData by passing false (indicating the DB is not available)
            OutputData outputData = factory.CreateOutputData(false);

            // Assert: Check if the returned instance is of type JSON
            Assert.That(outputData, Is.TypeOf<JSON>(), "Factory did not return instance of JSON when DB is not available.");
        }
    }
}