using EasMe.Models;

namespace EasMe.Result;

public readonly struct MergedResult
{
    public MergedResult(bool isSuccess,
                          ResultSeverity severity,
                          List<string> errorCodes, 
                          List<string> errors, 
                          List<ValidationError> validationErrors, 
                          List<CleanException> exceptions) {
        IsSuccess = isSuccess;
        ErrorCodes = errorCodes;
        Errors = errors;
        ValidationErrors = validationErrors;
        Exceptions = exceptions;
        Severity = severity;
    }
    public List<CleanException> Exceptions { get; init; } = new();
    /// <summary>
    ///     Indicates success status of <see cref="Result" />.
    /// </summary>
    public bool IsSuccess { get; init; } = false;

    /// <summary>
    ///     Indicates fail status of <see cref="Result" />.
    /// </summary>
    public bool IsFailure => !IsSuccess;
    
    public List<string> ErrorCodes { get; init; }= new();
    public List<string> Errors { get; init; } = new();
    public List<ValidationError> ValidationErrors { get; init; } = new();
    public ResultSeverity Severity { get; init; } = ResultSeverity.None;

}