namespace EasMe.Exceptions;

public class FailedToLogException : Exception
{
    public FailedToLogException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToLogException(string message) : base(message)
    {
    }

    public FailedToLogException(Exception? Inner = null) : base("FailedToLogException", Inner)
    {
    }
}