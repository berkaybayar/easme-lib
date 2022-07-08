namespace EasMe.Exceptions
{
    public class NoOnlineNetworkException : Exception
    {
        public NoOnlineNetworkException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NoOnlineNetworkException(string message) : base(message)
        {

        }
        public NoOnlineNetworkException(Exception? Inner = null) : base("NoOnlineNetworkException", Inner)
        {

        }
    }
}
