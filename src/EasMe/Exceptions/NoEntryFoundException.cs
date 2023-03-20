namespace EasMe.Exceptions;

public class NoEntryFoundException : Exception
{
    public NoEntryFoundException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public NoEntryFoundException(string message) : base(message)
    {
    }

    public NoEntryFoundException(Exception? Inner = null) : base("NoEntryFoundException", Inner)
    {
    }
}