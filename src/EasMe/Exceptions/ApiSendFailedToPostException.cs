namespace EasMe.Exceptions
{
    public class ApiSendFailedToPostException : Exception
    {
        public ApiSendFailedToPostException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToPostException(string message) : base(message)
        {

        }
        public ApiSendFailedToPostException(Exception? Inner = null) : base("ApiSendFailedToPostException", Inner)
        {

        }
    }
}
