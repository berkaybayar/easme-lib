namespace EasMe.Exceptions;

public class ApiSendFailedToPatchException : Exception {
    public ApiSendFailedToPatchException(string message, Exception? Inner = null) : base(message, Inner) {
    }

    public ApiSendFailedToPatchException(string message) : base(message) {
    }

    public ApiSendFailedToPatchException(Exception? Inner = null) : base("ApiSendFailedToPatchException", Inner) {
    }
}