namespace EasMe.Exceptions;

public class FailedToSearchException : Exception {
    public FailedToSearchException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToSearchException(string message) : base(message) {
    }

    public FailedToSearchException(Exception? Inner = null) : base("FailedToSearchException", Inner) {
    }
}