namespace EasMe.Exceptions
{
    public class FailedToWriteException : Exception
    {
        public FailedToWriteException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToWriteException(string message) : base(message)
        {

        }
        public FailedToWriteException(Exception? Inner = null) : base("FailedToWriteException", Inner)
        {

        }
    }
}
