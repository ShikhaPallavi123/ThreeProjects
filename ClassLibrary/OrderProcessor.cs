
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the OrderDetail class
//                - Defined properties for OrderNumber, DetailNumber, StockItem, Quantity, and IsElectronic
//                - Implemented constructor for initializing OrderDetail
// Version 1.1 - Added Copy Constructor to clone OrderDetail object and its associated StockItem
//                - Ensured deep copy for StockItem in the Copy Constructor
// Version 1.2 - Implemented CalculateDetailTotal method to calculate the total price for an OrderDetail
//                - Implemented CalculateTariff method to calculate a 5% tariff for electronic items
// Version 1.3 - Added logic for calculating tariff based on IsElectronic flag (5% tariff for electronics, 0% for others)
//                - Enhanced the functionality to handle both electronics and non-electronics properly



namespace OrderSystemLibrary
{
    public class OrderProcessor
    {
        private readonly IDatabaseService _databaseService;

        public OrderProcessor(IDatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        public bool ProcessOrder(Order order)
        {
            OutputData outputData;
            OutputDataFactory factory = new OutputDataFactory();

            if (_databaseService.IsDatabaseAvailable())
            {
                outputData = factory.CreateOutputData(true, "your_connection_string_here"); // Use SQLiteOutputData
            }
            else
            {
                outputData = factory.CreateOutputData(false); // Use JSON
            }

            outputData.Write(order); // Works for both SQLite and JSON
            return true;
        }
    }
}