namespace EasMe.Exceptions
{
    public class FailedToAuthenticateException : Exception
    {
        public FailedToAuthenticateException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToAuthenticateException(string message) : base(message)
        {

        }
        public FailedToAuthenticateException(Exception? Inner = null) : base("FailedToAuthenticateException", Inner)
        {

        }
    }
}
