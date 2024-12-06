using System;

namespace OrderSystemLibrary
{
    public class MYSQL : OutputData
    {
        public MYSQL() { }

        public void Write(Order order)
        {
            // Simulate writing to MySQL
            Console.WriteLine($"Order {order.OrderNumber} saved to MySQL database.");
        }
    }
}