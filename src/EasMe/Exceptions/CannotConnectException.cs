namespace EasMe.Exceptions
{
    public class CannotConnectException : Exception
    {
        public CannotConnectException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public CannotConnectException(string message) : base(message)
        {

        }
        public CannotConnectException(Exception? Inner = null) : base("CannotConnectException", Inner)
        {

        }
    }
}
