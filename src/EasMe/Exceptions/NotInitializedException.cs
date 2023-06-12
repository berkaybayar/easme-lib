namespace EasMe.Exceptions;

public class NotInitializedException : Exception {
    public NotInitializedException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public NotInitializedException(string message) : base(message) {
    }

    public NotInitializedException(Exception? Inner = null) : base("NotInitializedException", Inner) {
    }
}