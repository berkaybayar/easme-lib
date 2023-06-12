namespace EasMe.Exceptions;

public class FailedToApplyException : Exception {
    public FailedToApplyException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToApplyException(string message) : base(message) {
    }

    public FailedToApplyException(Exception? Inner = null) : base("FailedToApplyException", Inner) {
    }
}