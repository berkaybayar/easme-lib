namespace EasMe.Exceptions
{
    public class FailedToAddException : Exception
    {
        public FailedToAddException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToAddException(string message) : base(message)
        {

        }
        public FailedToAddException(Exception? Inner = null) : base("FailedToAddException", Inner)
        {

        }
    }
}
