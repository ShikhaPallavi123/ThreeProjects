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