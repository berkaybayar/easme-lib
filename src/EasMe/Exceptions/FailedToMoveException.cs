namespace EasMe.Exceptions;

public class FailedToMoveException : Exception
{
    public FailedToMoveException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToMoveException(string message) : base(message)
    {
    }

    public FailedToMoveException(Exception? Inner = null) : base("FailedToMoveException", Inner)
    {
    }
}