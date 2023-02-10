namespace EasMe.Exceptions
{
    public class TooBigValueException : Exception
    {
        public TooBigValueException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public TooBigValueException(string message) : base(message)
        {

        }
        public TooBigValueException(Exception? Inner = null) : base("TooBigValueException", Inner)
        {

        }
    }
}
