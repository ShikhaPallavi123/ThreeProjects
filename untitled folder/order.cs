using System;
using System.Collections.Generic;

namespace OrderSystemLibrary
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime DateTime { get; set; }
        public Customer Customer { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TariffAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        // Copy Constructor
        public Order(Order o)
        {
            OrderNumber = o.OrderNumber;
            DateTime = o.DateTime;
            Customer = new Customer(o.Customer);
            TaxAmount = o.TaxAmount;
            TariffAmount = o.TariffAmount;
            TotalAmount = o.TotalAmount;

            OrderDetails = new List<OrderDetail>();
            foreach (var detail in o.OrderDetails)
            {
                OrderDetails.Add(new OrderDetail(detail));
            }
        }

        public void AddOrderDetail(OrderDetail detail)
        {
            OrderDetails.Add(detail);
        }

        public void CalculateTotalAmount()
        {
            decimal total = 0;
            decimal tariff = 0;

            foreach (var detail in OrderDetails)
            {
                total += detail.CalculateDetailTotal();
                tariff += detail.CalculateTariff();
            }

            TaxAmount = total * 0.10m;  // Assuming a 10% tax rate
            TariffAmount = tariff;
            TotalAmount = total + TaxAmount + TariffAmount;
        }
    }
}