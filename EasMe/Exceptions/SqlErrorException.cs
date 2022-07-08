namespace EasMe.Exceptions
{
    public class SqlErrorException : Exception
    {
        public SqlErrorException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public SqlErrorException(string message) : base(message)
        {

        }
        public SqlErrorException(Exception? Inner = null) : base("SqlErrorException", Inner)
        {

        }
    }
}
