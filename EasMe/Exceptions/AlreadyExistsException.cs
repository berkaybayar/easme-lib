namespace EasMe.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public AlreadyExistsException(string message) : base(message)
        {

        }
        public AlreadyExistsException(Exception? Inner = null) : base("AlreadyExistsException", Inner)
        {

        }
    }
}
