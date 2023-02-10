namespace EasMe.Exceptions
{
    public class FailedToRemoveException : Exception
    {
        public FailedToRemoveException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToRemoveException(string message) : base(message)
        {

        }
        public FailedToRemoveException(Exception? Inner = null) : base("FailedToRemoveException", Inner)
        {

        }
    }
}
