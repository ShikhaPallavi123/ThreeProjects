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