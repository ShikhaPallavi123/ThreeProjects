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