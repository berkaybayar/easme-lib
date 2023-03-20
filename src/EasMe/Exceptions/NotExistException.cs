namespace EasMe.Exceptions;

public class NotExistException : Exception
{
    public NotExistException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public NotExistException(string message) : base(message)
    {
    }

    public NotExistException(Exception? Inner = null) : base("NotExistException", Inner)
    {
    }
}