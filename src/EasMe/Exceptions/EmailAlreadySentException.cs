namespace EasMe.Exceptions;

public class EmailAlreadySentException : Exception {
    public EmailAlreadySentException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public EmailAlreadySentException(string message) : base(message) {
    }

    public EmailAlreadySentException(Exception? Inner = null) : base("EmailAlreadySentException", Inner) {
    }
}