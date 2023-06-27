namespace EasMe.Result;

public class ValidationError
{
    public ValidationError(string message, string? property = null, string? errorCode = null) {
        Message = message;
        Property = property;
        ErrorCode = errorCode;
    }

    public ValidationError() {
        
    }
    public string Message { get; init; }
    public string? Property { get; init; }
    public string? ErrorCode { get; init; }
}