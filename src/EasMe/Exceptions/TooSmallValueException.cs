namespace EasMe.Exceptions
{
    public class TooSmallValueException : Exception
    {
        public TooSmallValueException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public TooSmallValueException(string message) : base(message)
        {

        }
        public TooSmallValueException(Exception? Inner = null) : base("TooSmallValueException", Inner)
        {

        }
    }
}
