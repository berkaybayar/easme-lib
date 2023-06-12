namespace EasMe.Exceptions;

public class FailedToValidateException : Exception {
    public FailedToValidateException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToValidateException(string message) : base(message) {
    }

    public FailedToValidateException(Exception? Inner = null) : base("FailedToValidateException", Inner) {
    }
}