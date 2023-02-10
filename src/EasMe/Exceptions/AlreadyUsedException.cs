namespace EasMe.Exceptions
{
    public class AlreadyUsedException : Exception
    {
        public AlreadyUsedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public AlreadyUsedException(string message) : base(message)
        {

        }
        public AlreadyUsedException(Exception? Inner = null) : base("AlreadyUsedException", Inner)
        {

        }
    }
}
