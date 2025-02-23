
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
    public class OrderDetail
    {
        public int OrderNumber { get; set; }
        public int DetailNumber { get; set; }
        public StockItem StockItem { get; set; }
        public int Quantity { get; set; }
        public bool IsElectronic { get; set; }

        // Constructor
        public OrderDetail() { }

        // Copy Constructor
        public OrderDetail(OrderDetail detail)
        {
            OrderNumber = detail.OrderNumber;
            DetailNumber = detail.DetailNumber;
            StockItem = new StockItem(detail.StockItem);
            Quantity = detail.Quantity;
            IsElectronic = detail.IsElectronic;
        }

        public decimal CalculateDetailTotal()
        {
            return StockItem.Price * Quantity;
        }

        public decimal CalculateTariff()
        {
            return IsElectronic ? CalculateDetailTotal() * 0.05m : 0;  // 5% tariff for electronics
        }
    }
}