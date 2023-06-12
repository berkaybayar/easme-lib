namespace EasMe.Exceptions;

public class ApiSendFailedToGetException : Exception {
    public ApiSendFailedToGetException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public ApiSendFailedToGetException(string message) : base(message) {
    }

    public ApiSendFailedToGetException(Exception? Inner = null) : base("ApiSendFailedToGetException", Inner) {
    }
}