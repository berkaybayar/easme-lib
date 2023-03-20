namespace EasMe.Exceptions;

public class ExpiredException : Exception
{
    public ExpiredException(string message, Exception? Inner = null) : base(message, Inner)
    {
    }

    public ExpiredException(string message) : base(message)
    {
    }

    public ExpiredException(Exception? Inner = null) : base("ExpiredException", Inner)
    {
    }
}