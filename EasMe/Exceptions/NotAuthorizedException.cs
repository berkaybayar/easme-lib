namespace EasMe.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotAuthorizedException(string message) : base(message)
        {

        }
        public NotAuthorizedException(Exception? Inner = null) : base("NotAuthorizedException", Inner)
        {

        }

    }
}
