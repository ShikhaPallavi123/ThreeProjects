using System;
using System.Collections.Generic;

// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the Order class
//                - Defined properties for OrderNumber, DateTime, Customer, TaxAmount, TariffAmount, TotalAmount, and OrderDetails
//                - Implemented constructor for initializing OrderDetails as an empty list
//                - Added Copy Constructor to clone Order object and its associated Customer and OrderDetails
// Version 1.1 - Implemented AddOrderDetail method to add individual OrderDetail objects to OrderDetails list
//                - Added CalculateTotalAmount method to calculate total amounts (Tax, Tariff, and Total) based on OrderDetails
//                - Assumed a 10% tax rate for TaxAmount calculation
// Version 1.2 - Refined calculation logic and handling for multiple details and tariff values
//                - Ensured proper handling of deep copy in the Copy Constructor for OrderDetails


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