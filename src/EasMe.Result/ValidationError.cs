namespace EasMe.Result;

public class ValidationError
{
    public string Message { get; set; }
    public string? Property { get; set; }
    public string? ErrorCode { get; set; }
    
}