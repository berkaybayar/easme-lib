namespace EasMe.Exceptions;

public class InternalDbException : Exception
{
    public InternalDbException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public InternalDbException(string message) : base(message)
    {
    }

    public InternalDbException(Exception? Inner = null) : base("InternalDbException", Inner)
    {
    }
}