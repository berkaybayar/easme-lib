namespace EasMe.Exceptions
{
    public class FailedToLoadException : Exception
    {
        public FailedToLoadException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToLoadException(string message) : base(message)
        {

        }
        public FailedToLoadException(Exception? Inner = null) : base("FailedToLoadException", Inner)
        {

        }
    }
}
