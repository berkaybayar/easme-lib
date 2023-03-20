namespace EasMe.Exceptions;

public class NotValidException : Exception
{
    public NotValidException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public NotValidException(string message) : base(message)
    {
    }

    public NotValidException(Exception? Inner = null) : base("NotValidException", Inner)
    {
    }
}