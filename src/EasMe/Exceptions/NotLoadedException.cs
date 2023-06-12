namespace EasMe.Exceptions;

public class NotLoadedException : Exception {
    public NotLoadedException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public NotLoadedException(string message) : base(message) {
    }

    public NotLoadedException(Exception? Inner = null) : base("NotLoadedException", Inner) {
    }
}