namespace EasMe.Exceptions;

public class FailedToCheck : Exception
{
    public FailedToCheck(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToCheck(string message) : base(message)
    {
    }

    public FailedToCheck(Exception? Inner = null) : base("FailedToCheck", Inner)
    {
    }
}