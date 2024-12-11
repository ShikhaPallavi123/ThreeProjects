
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the SQLiteService class
//                - Implemented constructor to initialize connection string
//                - Defined IsDatabaseAvailable method to check database availability
//                - Implemented SaveOrder and SaveOrderToDatabase methods to save orders to SQLite database
//                - Included transaction management for database operations
// Version 1.1 - Added error handling for null order input and invalid order details
//                - Implemented console logging for transaction success/failure
//                - Enhanced exception handling to rollback transaction in case of error
// Version 1.2 - Introduced SaveOrderToJson method as a fallback mechanism when the database is unavailable
//                - Delegated the actual JSON saving logic to the JSON class
//                - Provided console feedback when saving order to JSON in case of database unavailability


using System;
using System.Data.SQLite;

namespace OrderSystemLibrary
{
    public class SQLiteService : IDatabaseService
    {
        private readonly string _connectionString;

        public SQLiteService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null");
        }

        // Method to check if the database is available
        public bool IsDatabaseAvailable()
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log exception (can be replaced with a logging framework)
                Console.WriteLine($"Error checking database availability: {ex.Message}");
                return false;
            }
        }

        // Method to save the order to the database
        public void SaveOrder(Order order)
        {
            SaveOrderToDatabase(order);  // Delegate to the SaveOrderToDatabase method
        }

        // Implement SaveOrderToDatabase (method of IDatabaseService interface)
        public void SaveOrderToDatabase(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null");
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    // Insert Order into the database
                    string insertOrderQuery = "INSERT INTO Orders (OrderNumber, DateTime, CustomerName, CustomerPhone, TaxAmount, TariffAmount, TotalAmount) " +
                                              "VALUES (@OrderNumber, @DateTime, @CustomerName, @CustomerPhone, @TaxAmount, @TariffAmount, @TotalAmount)";
                    using (var cmd = new SQLiteCommand(insertOrderQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                        cmd.Parameters.AddWithValue("@DateTime", order.DateTime);
                        cmd.Parameters.AddWithValue("@CustomerName", order.Customer.Name);
                        cmd.Parameters.AddWithValue("@CustomerPhone", order.Customer.Phone);
                        cmd.Parameters.AddWithValue("@TaxAmount", order.TaxAmount);
                        cmd.Parameters.AddWithValue("@TariffAmount", order.TariffAmount);
                        cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                        cmd.ExecuteNonQuery();
                    }

                    // Insert each OrderDetail into the database
                    foreach (var detail in order.OrderDetails)
                    {
                        if (detail == null || detail.StockItem == null)
                        {
                            Console.WriteLine($"Skipping invalid order detail with OrderNumber {order.OrderNumber}");
                            continue;
                        }

                        string insertDetailQuery = "INSERT INTO OrderDetails (OrderNumber, DetailNumber, StockID, StockName, StockPrice, Quantity) " +
                                                   "VALUES (@OrderNumber, @DetailNumber, @StockID, @StockName, @StockPrice, @Quantity)";
                        using (var cmd = new SQLiteCommand(insertDetailQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@OrderNumber", order.OrderNumber);
                            cmd.Parameters.AddWithValue("@DetailNumber", detail.DetailNumber);
                            cmd.Parameters.AddWithValue("@StockID", detail.StockItem.StockID);
                            cmd.Parameters.AddWithValue("@StockName", detail.StockItem.Name);
                            cmd.Parameters.AddWithValue("@StockPrice", detail.StockItem.Price);
                            cmd.Parameters.AddWithValue("@Quantity", detail.Quantity);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Commit the transaction if all inserts are successful
                    transaction.Commit();
                    Console.WriteLine($"Order {order.OrderNumber} and its details saved successfully.");
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if there is an error
                    transaction.Rollback();
                    Console.WriteLine($"Error saving order to SQLite: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Ensure connection is closed
                }
            }
        }

        // Save to JSON (for fallback if database is unavailable)
        public void SaveOrderToJson(Order order)
        {
            var json = new JSON();
            json.Write(order); // Delegate to JSON class
            Console.WriteLine("Saving order to JSON (database unavailable).");
        }

    }
}
