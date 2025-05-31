namespace GlobalErrorHandler.CustomerException
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException() : base("Resource not found")
        {
        }
    }
}
