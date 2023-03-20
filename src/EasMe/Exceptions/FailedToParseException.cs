namespace EasMe.Exceptions;

public class FailedToParseException : Exception
{
    public FailedToParseException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToParseException(string message) : base(message)
    {
    }

    public FailedToParseException(Exception? Inner = null) : base("FailedToParseException", Inner)
    {
    }
}