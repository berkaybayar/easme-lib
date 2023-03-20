namespace EasMe.Exceptions;

public class EmailSendFailedException : Exception
{
    public EmailSendFailedException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public EmailSendFailedException(string message) : base(message)
    {
    }

    public EmailSendFailedException(Exception? Inner = null) : base("EmailSendFailedException", Inner)
    {
    }
}