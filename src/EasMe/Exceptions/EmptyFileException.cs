namespace EasMe.Exceptions;

public class EmptyFileException : Exception
{
    public EmptyFileException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public EmptyFileException(string message) : base(message)
    {
    }

    public EmptyFileException(Exception? Inner = null) : base("EmptyFileException", Inner)
    {
    }
}