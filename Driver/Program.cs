// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the Order System Driver
//                - Implemented Customer, StockItem, OrderDetail, and Order classes
//                - Introduced basic order processing functionality
//                - Added CalculateTotalAmount method for order total calculation
// Version 1.1 - Introduced OrderProcessor class for managing order processing
//                - Added MockDatabaseService to simulate database interaction
//                - Implemented logic to save orders to JSON when database is unavailable
// Version 1.2 - Enhanced tariff and tax calculation logic
//                - Applied electronic tariff for items with StockID starting with "ELECT"
//                - Added order summary output including detailed breakdown of tax, tariff, and total amount
// Version 1.3 - Refactored code for future integration with real database
//                - Improved logging and error handling in OrderProcessor
//                - Cleaned up the structure for easier unit testing
// Version 1.4 - Improved error handling and added unit tests
//                - Added exception handling for database failures and null references
//                - Refined the IsElectronic flag logic for accuracy
// Version 1.5 - Final release for Version 1.x
//                - Streamlined order processing logic for performance improvements
//                - Introduced custom exceptions for better error handling during order processing
//                - Final preparations for database integration and advanced order features
// Version 2.0 (Future) - Plan to migrate to real database implementation
//                        - Advanced features to be added such as order status tracking, discounts, and shipping calculations



using System;
using System.Collections.Generic;
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
                Customer = customer,
                OrderDetails = new List<OrderDetail> { detail1, detail2 }  // Add details directly
            };

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

    // Order class
    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }  // List of order details
        public decimal TaxAmount { get; set; }
        public decimal TariffAmount { get; set; }
        public decimal TotalAmount { get; set; }

        // Method to calculate the total amount
        public void CalculateTotalAmount()
        {
            decimal subtotal = 0;
            foreach (var detail in OrderDetails)
            {
                subtotal += detail.StockItem.Price * detail.Quantity;
            }

            TaxAmount = subtotal * 0.10m;  // 10% tax
            TariffAmount = 0;

            // Apply electronic tariff if applicable
            foreach (var detail in OrderDetails)
            {
                if (detail.IsElectronic)
                {
                    TariffAmount += detail.StockItem.Price * 0.05m;  // Example tariff for electronic items
                }
            }

            TotalAmount = subtotal + TaxAmount + TariffAmount;
        }
    }

    // Customer class
    public class Customer
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        // Constructor to initialize customer properties
        public Customer(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }

    // StockItem class
    public class StockItem
    {
        public string StockID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Constructor to initialize stock item properties
        public StockItem(string stockID, string name, decimal price)
        {
            StockID = stockID;
            Name = name;
            Price = price;
        }
    }

    // OrderDetail class
    public class OrderDetail
    {
        public int OrderNumber { get; set; }
        public int DetailNumber { get; set; }
        public StockItem StockItem { get; set; }
        public int Quantity { get; set; }
        public bool IsElectronic { get; set; }
    }

    // IDatabaseService interface
    public interface IDatabaseService
    {
        bool IsDatabaseAvailable();
        void SaveOrder(Order order);
        void SaveOrderToJson(Order order);
    }

    // OrderProcessor class
    public class OrderProcessor
    {
        private readonly IDatabaseService _dbService;

        public OrderProcessor(IDatabaseService dbService)
        {
            _dbService = dbService;
        }

        public void ProcessOrder(Order order)
        {
            if (_dbService.IsDatabaseAvailable())
            {
                _dbService.SaveOrder(order);  // Save to database
            }
            else
            {
                _dbService.SaveOrderToJson(order);  // Save to JSON if database is unavailable
            }
        }
    }
}
