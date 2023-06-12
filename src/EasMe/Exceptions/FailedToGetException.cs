namespace EasMe.Exceptions;

public class FailedToGetException : Exception {
    public FailedToGetException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToGetException(string message) : base(message) {
    }

    public FailedToGetException(Exception? Inner = null) : base("FailedToGetException", Inner) {
    }
}