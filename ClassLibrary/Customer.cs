// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the Customer class
//                - Implemented properties for Name and Phone
//                - Added constructor to initialize customer details
// Version 1.1 - Added a copy constructor for creating a new instance from another Customer object
//                - Enhanced code to allow easy duplication of customer data





namespace OrderSystemLibrary
{
    public class Customer
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        // Constructor
        public Customer(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        // Copy Constructor
        public Customer(Customer obj)
        {
            Name = obj.Name;
            Phone = obj.Phone;
        }
    }
}