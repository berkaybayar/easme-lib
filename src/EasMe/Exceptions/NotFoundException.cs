namespace EasMe.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(Exception? Inner = null) : base("NotFoundException", Inner)
    {
    }
}