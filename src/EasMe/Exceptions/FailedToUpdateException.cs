namespace EasMe.Exceptions
{
    public class FailedToUpdateException : Exception
    {
        public FailedToUpdateException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToUpdateException(string message) : base(message)
        {

        }
        public FailedToUpdateException(Exception? Inner = null) : base("FailedToUpdateException", Inner)
        {

        }
    }
}
