namespace EasMe.Exceptions
{
    public class NotEnabledException : Exception
    {
        public NotEnabledException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotEnabledException(string message) : base(message)
        {

        }
        public NotEnabledException(Exception? Inner = null) : base("NotEnabledException", Inner)
        {

        }
    }
}
