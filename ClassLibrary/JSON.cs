using System;
using System.IO;
using System.Text.Json;

namespace OrderSystemLibrary
{
    public class JSON : OutputData
    {
        public JSON() { }

        public void Write(Order order)
        {
            try
            {
                string json = JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText($"Order_{order.OrderNumber}.json", json);
                Console.WriteLine($"Order saved to JSON file: Order_{order.OrderNumber}.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving order to JSON: {ex.Message}");
            }
        }
    }
}