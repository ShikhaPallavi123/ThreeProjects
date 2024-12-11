// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the JSON class that implements OutputData
//                - Implemented Write method to save Order object to a JSON file
//                - Introduced SaveOrderToJson method to handle JSON serialization and file writing
//                - Added error handling for null order input and file writing exceptions
// Version 1.1 - Refined error handling and logging mechanisms
//                - Enhanced exception messaging for better diagnostics
//                - Ensured compatibility with larger order data through JSON serialization options



namespace OrderSystemLibrary
{
    public class JSON : OutputData
    {
        public void Write(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            SaveOrderToJson(order); // Delegate to the JSON-specific implementation
        }

        public void SaveOrderToJson(Order order)
        {
            string filePath = $"Order_{order.OrderNumber}.json";
            try
            {
                string jsonData = System.Text.Json.JsonSerializer.Serialize(order, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine($"Order saved to JSON: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save order to JSON: {ex.Message}");
                // Handle or log exception
            }
        }
    }
}