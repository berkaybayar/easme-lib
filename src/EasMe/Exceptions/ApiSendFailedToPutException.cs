namespace EasMe.Exceptions;

public class ApiSendFailedToPutException : Exception {
    public ApiSendFailedToPutException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public ApiSendFailedToPutException(string message) : base(message) {
    }

    public ApiSendFailedToPutException(Exception? Inner = null) : base("ApiSendFailedToPutException", Inner) {
    }
}