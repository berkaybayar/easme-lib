namespace EasMe.Exceptions;

public class FailedToReloadException : Exception
{
    public FailedToReloadException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToReloadException(string message) : base(message)
    {
    }

    public FailedToReloadException(Exception? Inner = null) : base("FailedToReloadException", Inner)
    {
    }
}