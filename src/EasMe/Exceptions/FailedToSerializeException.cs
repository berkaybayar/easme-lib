namespace EasMe.Exceptions;

public class FailedToSerializeException : Exception
{
    public FailedToSerializeException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToSerializeException(string message) : base(message)
    {
    }

    public FailedToSerializeException(Exception? Inner = null) : base("FailedToSerializeException", Inner)
    {
    }
}