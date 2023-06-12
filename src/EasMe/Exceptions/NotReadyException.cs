namespace EasMe.Exceptions;

public class NotReadyException : Exception {
    public NotReadyException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public NotReadyException(string message) : base(message) {
    }

    public NotReadyException(Exception? Inner = null) : base("NotReadyException", Inner) {
    }
}