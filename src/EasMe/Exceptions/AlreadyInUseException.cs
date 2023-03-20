namespace EasMe.Exceptions;

public class AlreadyInUseException : Exception
{
    public AlreadyInUseException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public AlreadyInUseException(string message) : base(message)
    {
    }

    public AlreadyInUseException(Exception? Inner = null) : base("AlreadyInUseException", Inner)
    {
    }
}