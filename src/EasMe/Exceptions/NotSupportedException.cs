namespace EasMe.Exceptions;

public class NotSupportedException : Exception {
    public NotSupportedException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public NotSupportedException(string message) : base(message) {
    }

    public NotSupportedException(Exception? Inner = null) : base("NotSupportedException", Inner) {
    }
}