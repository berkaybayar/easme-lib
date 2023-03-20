namespace EasMe.Exceptions;

public class EmailIncorrectException : Exception
{
    public EmailIncorrectException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public EmailIncorrectException(string message) : base(message)
    {
    }

    public EmailIncorrectException(Exception? Inner = null) : base("EmailIncorrectException", Inner)
    {
    }
}