namespace EasMe.Exceptions;

public class FailedToInitializeException : Exception {
    public FailedToInitializeException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToInitializeException(string message) : base(message) {
    }

    public FailedToInitializeException(Exception? Inner = null) : base("FailedToInitializeException", Inner) {
    }
}