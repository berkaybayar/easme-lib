namespace EasMe.Exceptions
{
    public class ApiSendFailedToDeleteException : Exception
    {
        public ApiSendFailedToDeleteException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToDeleteException(string message) : base(message)
        {

        }
        public ApiSendFailedToDeleteException(Exception? Inner = null) : base("ApiSendFailedToDeleteException", Inner)
        {

        }
    }
}
