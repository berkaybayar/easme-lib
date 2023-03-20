namespace EasMe.Exceptions;

public class ApiFailedToGetResponseException : Exception
{
    public ApiFailedToGetResponseException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public ApiFailedToGetResponseException(string message) : base(message)
    {
    }

    public ApiFailedToGetResponseException(Exception? Inner = null) : base("ApiFailedToGetResponseException", Inner)
    {
    }
}