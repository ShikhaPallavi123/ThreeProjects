using System;

namespace OrderSystemLibrary
{
    public class MYSQL : OutputData
    {
        private readonly TextWriter _outputWriter;

        // Constructor that accepts TextWriter (e.g., StringWriter or Console.Out)
        public MYSQL(TextWriter outputWriter = null)
        {
            _outputWriter = outputWriter ?? Console.Out;  // Default to Console.Out if no output writer is passed
        }

        public void Write(Order order)
        {
            // Simulate writing to MySQL and writing to the provided TextWriter
            _outputWriter.WriteLine($"Order {order.OrderNumber} saved to MySQL database.");
        }
    }
}