namespace EasMe.Exceptions;

public class FailedToDeserializeException : Exception
{
    public FailedToDeserializeException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public FailedToDeserializeException(string message) : base(message)
    {
    }

    public FailedToDeserializeException(Exception? Inner = null) : base("FailedToDeserializeException", Inner)
    {
    }
}