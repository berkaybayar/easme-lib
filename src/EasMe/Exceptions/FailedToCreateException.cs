namespace EasMe.Exceptions;

public class FailedToCreateException : Exception {
    public FailedToCreateException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToCreateException(string message) : base(message) {
    }

    public FailedToCreateException(Exception? Inner = null) : base("FailedToCreateException", Inner) {
    }
}