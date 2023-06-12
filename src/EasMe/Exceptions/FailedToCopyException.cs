namespace EasMe.Exceptions;

public class FailedToCopyException : Exception {
    public FailedToCopyException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToCopyException(string message) : base(message) {
    }

    public FailedToCopyException(Exception? Inner = null) : base("FailedToCopyException", Inner) {
    }
}