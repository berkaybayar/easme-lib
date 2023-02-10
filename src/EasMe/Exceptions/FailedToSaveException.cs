namespace EasMe.Exceptions
{
    public class FailedToSaveException : Exception
    {
        public FailedToSaveException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToSaveException(string message) : base(message)
        {

        }
        public FailedToSaveException(Exception? Inner = null) : base("FailedToSaveException", Inner)
        {

        }
    }
}
