namespace EasMe.Exceptions;

public class NotAuthenticatedException : Exception
{
    public NotAuthenticatedException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public NotAuthenticatedException(string message) : base(message)
    {
    }

    public NotAuthenticatedException(Exception? Inner = null) : base("NotAuthenticatedException", Inner)
    {
    }
}