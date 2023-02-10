namespace EasMe.Exceptions
{
    public class UsernameIncorrectException : Exception
    {
        public UsernameIncorrectException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public UsernameIncorrectException(string message) : base(message)
        {

        }
        public UsernameIncorrectException(Exception? Inner = null) : base("UsernameIncorrectException", Inner)
        {

        }
    }
}
