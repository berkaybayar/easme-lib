namespace EasMe.Exceptions;

public class FailedToSendException : Exception {
    public FailedToSendException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToSendException(string message) : base(message) {
    }

    public FailedToSendException(Exception? Inner = null) : base("FailedToSendException", Inner) {
    }
}