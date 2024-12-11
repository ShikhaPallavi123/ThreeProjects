

// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the OutputData interface
//                - Defined method signatures for Write and SaveOrderToJson to handle saving orders
// Version 1.1 - Clarified method responsibilities in the interface
//                - Write method designed to delegate order saving functionality
//                - SaveOrderToJson method for specific JSON file saving logic


namespace OrderSystemLibrary
{
    public interface OutputData
    {
        void Write(Order order);
        void SaveOrderToJson(Order order);
    }
}