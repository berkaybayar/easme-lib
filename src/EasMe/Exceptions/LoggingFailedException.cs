namespace EasMe.Exceptions;

public class LoggingFailedException : Exception
{
    public LoggingFailedException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public LoggingFailedException(string message) : base(message)
    {
    }

    public LoggingFailedException(Exception? Inner = null) : base("LoggingFailedException", Inner)
    {
    }
}