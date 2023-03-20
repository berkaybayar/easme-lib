namespace EasMe.Exceptions;

public class FailedToReadException : Exception
{
    public FailedToReadException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToReadException(string message) : base(message)
    {
    }

    public FailedToReadException(Exception? Inner = null) : base("FailedToReadException", Inner)
    {
    }
}