namespace OrderSystemLibrary
{
    public class OutputDataFactory
    {
        public OutputData CreateOutputData(bool isDBAvailable)
        {
            if (isDBAvailable)
            {
                return new MYSQL();  // Creates MYSQL instance if DB is available
            }
            return new JSON();  // Otherwise, creates JSON instance
        }
    }
}