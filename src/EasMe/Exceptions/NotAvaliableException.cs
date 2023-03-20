namespace EasMe.Exceptions;

public class NotAvaliableException : Exception
{
    public NotAvaliableException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public NotAvaliableException(string message) : base(message)
    {
    }

    public NotAvaliableException(Exception? Inner = null) : base("NotAvaliableException", Inner)
    {
    }
}