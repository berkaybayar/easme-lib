namespace EasMe.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public InvalidTokenException(string message) : base(message)
        {

        }
        public InvalidTokenException(Exception? Inner = null) : base("InvalidTokenException", Inner)
        {

        }
    }
}
