namespace EasMe.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotConnectedException(string message) : base(message)
        {

        }
        public NotConnectedException(Exception? Inner = null) : base("NotConnectedException", Inner)
        {

        }
    }
}
