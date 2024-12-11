
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the SQLiteOutputData class
//                - Implemented constructor to initialize SQLiteService instance
//                - Defined Write method to save order to SQLite database using SQLiteService
//                - Provided fallback SaveOrderToJson method to save order as JSON if SQLiteService fails
// Version 1.1 - Added validation to check for null order in the Write method
//                - Enhanced feedback messages in both Write and SaveOrderToJson methods
// Version 1.2 - Updated the handling of JSON fallback to provide clear feedback in case of database unavailability


using System;

namespace OrderSystemLibrary
{
    public class SQLiteOutputData : OutputData
    {
        private readonly SQLiteService _sqliteService;

        public SQLiteOutputData(SQLiteService sqliteService)
        {
            _sqliteService = sqliteService ?? throw new ArgumentNullException(nameof(sqliteService));
        }

        public void Write(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            _sqliteService.SaveOrderToDatabase(order);
            Console.WriteLine($"Order {order.OrderNumber} saved to SQLite database.");
        }

        public void SaveOrderToJson(Order order)
        {
            var jsonFallback = new JSON();
            jsonFallback.Write(order);
            Console.WriteLine("Saving order to JSON (fallback).");
        }
    }
}