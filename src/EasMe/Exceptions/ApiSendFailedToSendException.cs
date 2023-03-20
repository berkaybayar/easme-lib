namespace EasMe.Exceptions;

public class ApiSendFailedToSendException : Exception
{
    public ApiSendFailedToSendException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public ApiSendFailedToSendException(string message) : base(message)
    {
    }

    public ApiSendFailedToSendException(Exception? Inner = null) : base("ApiSendFailedToSendException", Inner)
    {
    }
}