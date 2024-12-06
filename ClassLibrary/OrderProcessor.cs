using System;

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

            if (_databaseService.IsDatabaseAvailable())
            {
                outputData = new MYSQL();
            }
            else
            {
                outputData = new JSON();
            }

            outputData.Write(order);
            return true;
        }
    }
}