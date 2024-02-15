namespace kvaksy_backend.helpers
{
    
    public class UnknownErrorException : Exception
    {
        public UnknownErrorException() : base("An unknown error occurred.")
        {
        }
    }
}
