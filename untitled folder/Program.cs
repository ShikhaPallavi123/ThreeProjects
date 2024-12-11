using System;
using OrderSystemLibrary;

namespace OrderSystemDriver
{
    class Driver
    {
        static void Main(string[] args)
        {
            // Create a customer
            Customer customer = new Customer("John Doe", "123-456-7890");

            // Create stock items
            StockItem stockItem1 = new StockItem("ELECT001", "Laptop", 1200.00m);
            StockItem stockItem2 = new StockItem("S1002", "Headphones", 150.00m);

            // Create order details
            OrderDetail detail1 = new OrderDetail
            {
                OrderNumber = 1,
                DetailNumber = 1,
                StockItem = stockItem1,
                Quantity = 1,
                IsElectronic = stockItem1.StockID.StartsWith("ELECT")  // Automatically set based on StockID
            };

            OrderDetail detail2 = new OrderDetail
            {
                OrderNumber = 1,
                DetailNumber = 2,
                StockItem = stockItem2,
                Quantity = 2,
                IsElectronic = stockItem2.StockID.StartsWith("ELECT")  // Automatically set based on StockID
            };

            // Create an order and add details
            Order order = new Order
            {
                OrderNumber = 1,
                DateTime = DateTime.Now,
                Customer = customer
            };

            order.AddOrderDetail(detail1);
            order.AddOrderDetail(detail2);

            // Calculate the total amount of the order (including tax and tariffs)
            order.CalculateTotalAmount();

            // Display order details
            Console.WriteLine("----- Order Summary -----");
            Console.WriteLine($"Order Number: {order.OrderNumber}");
            Console.WriteLine($"Customer: {order.Customer.Name}");
            Console.WriteLine($"Date: {order.DateTime}");
            Console.WriteLine("\nOrder Items:");

            foreach (var detail in order.OrderDetails)
            {
                Console.WriteLine($"  Detail Number: {detail.DetailNumber}");
                Console.WriteLine($"  Item: {detail.StockItem.Name}");
                Console.WriteLine($"  Quantity: {detail.Quantity}");
                Console.WriteLine($"  Price per Item: ${detail.StockItem.Price}");
                Console.WriteLine($"  Electronic Tariff Applied: {(detail.IsElectronic ? "Yes" : "No")}");
                Console.WriteLine();
            }

            // Print price breakdown
            Console.WriteLine($"Subtotal: ${order.TotalAmount - order.TaxAmount - order.TariffAmount:F2}");
            Console.WriteLine($"Tax (10%): ${order.TaxAmount:F2}");
            Console.WriteLine($"Tariff Amount: ${order.TariffAmount:F2}");
            Console.WriteLine($"Total Amount: ${order.TotalAmount:F2}");

            // Simulate a database service
            IDatabaseService dbService = new MockDatabaseService();  // Mock service simulating database behavior

            // Process the order
            OrderProcessor processor = new OrderProcessor(dbService);
            processor.ProcessOrder(order);

            Console.WriteLine("\nOrder processing completed.");
        }
    }

    // Mock implementation of the IDatabaseService
    public class MockDatabaseService : IDatabaseService
    {
        // Simulate database availability: change the return value to test different outputs
        public bool IsDatabaseAvailable()
        {
            return false;  // Change to 'true' to simulate database availability
        }

        // Simulate saving an order to the database
        public void SaveOrder(Order order)
        {
            Console.WriteLine("\nOrder saved to the database successfully.");
        }

        // Simulate saving an order to JSON if the database is unavailable
        public void SaveOrderToJson(Order order)
        {
            Console.WriteLine("\nDatabase unavailable. Order saved to JSON file.");
        }
    }
}
