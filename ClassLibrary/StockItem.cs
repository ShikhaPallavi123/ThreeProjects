
// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the StockItem class
//                - Defined properties for StockID, Name, and Price
//                - Implemented constructor for initializing StockItem with values
// Version 1.1 - Added Copy Constructor to clone StockItem object
//                - Ensured deep copy for StockItem properties in the Copy Constructor


namespace OrderSystemLibrary
{
    public class StockItem
    {
        public string StockID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Constructor
        public StockItem(string stockID, string name, decimal price)
        {
            StockID = stockID;
            Name = name;
            Price = price;
        }

        // Copy Constructor
        public StockItem(StockItem si)
        {
            StockID = si.StockID;
            Name = si.Name;
            Price = si.Price;
        }
    }
}