namespace EasMe.Exceptions
{
    public class FailedToDeleteException : Exception
    {
        public FailedToDeleteException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToDeleteException(string message) : base(message)
        {

        }
        public FailedToDeleteException(Exception? Inner = null) : base("FailedToDeleteException", Inner)
        {

        }
    }
}
