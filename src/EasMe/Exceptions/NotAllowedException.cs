namespace EasMe.Exceptions
{
    public class NotAllowedException : Exception
    {
        public NotAllowedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotAllowedException(string message) : base(message)
        {

        }
        public NotAllowedException(Exception? Inner = null) : base("NotAllowedException", Inner)
        {

        }
    }
}
