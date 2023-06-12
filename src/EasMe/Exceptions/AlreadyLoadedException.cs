namespace EasMe.Exceptions;

public class AlreadyLoadedException : Exception {
    public AlreadyLoadedException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public AlreadyLoadedException(string message) : base(message) {
    }

    public AlreadyLoadedException(Exception? Inner = null) : base("AlreadyLoadedException", Inner) {
    }
}