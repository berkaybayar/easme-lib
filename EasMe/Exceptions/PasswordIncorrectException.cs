namespace EasMe.Exceptions
{
    public class PasswordIncorrectException : Exception
    {
        public PasswordIncorrectException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public PasswordIncorrectException(string message) : base(message)
        {

        }
        public PasswordIncorrectException(Exception? Inner = null) : base("PasswordIncorrectException", Inner)
        {

        }
    }
}
