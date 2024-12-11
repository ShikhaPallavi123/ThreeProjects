// Author: Shikha Pallavi
// Date: 12/10/2024
// Revision History:
// Version 1.0 - Initial creation of the OutputDataFactory class
//                - Created CreateOutputData method to return either a SQLite or JSON output data handler
//                - Implemented logic to validate if a connection string is provided when the database is available
// Version 1.1 - Added exception handling for empty connection string when database is available
//                - Ensured that SQLiteOutputData is returned when the database connection is available and valid
//                - Defaulted to JSON output when database is not available or connection string is not provided


namespace OrderSystemLibrary
{
    public class OutputDataFactory
    {
        public OutputData CreateOutputData(bool isDBAvailable, string connectionString = "")
        {
            if (isDBAvailable && string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be empty when the database is available.");
            }

            if (isDBAvailable)
            {
                // Return SQLiteOutputData which implements OutputData
                var sqliteService = new SQLiteService(connectionString);
                return new SQLiteOutputData(sqliteService);
            }
            
            return new JSON();  // Otherwise, create JSON instance
        }
    }
}