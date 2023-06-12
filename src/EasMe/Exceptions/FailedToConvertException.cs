namespace EasMe.Exceptions;

public class FailedToConvertException : Exception {
    public FailedToConvertException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public FailedToConvertException(string message) : base(message) {
    }

    public FailedToConvertException(Exception? Inner = null) : base("FailedToConvertException", Inner) {
    }
}