// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the IDatabaseService interface
//                - Defined method signatures for saving an order to the database
//                - Included IsDatabaseAvailable method to check database availability
//                - Added SaveOrder method for general saving logic, allowing flexibility for different data storage mechanisms




namespace OrderSystemLibrary
{
    public interface IDatabaseService
    {
        // Define the method signature that needs to be implemented
        void SaveOrderToDatabase(Order order);
        
        // Define any other methods you might have in the interface
        bool IsDatabaseAvailable();
        void SaveOrder(Order order);  // You can use this for any general saving logic
    }
}