using EasMe.Models;
using Newtonsoft.Json;

namespace EasMe.Result;

/// <summary>
///     A readonly struct Result to be used in Domain Driven Design mainly.
///     <br />
///     In order to avoid using <see cref="Exception" />'s and the performance downside from it.
/// </summary>
public readonly struct Result {
    #region CONSTRUCTORS

    internal Result(bool isSuccess, ResultSeverity severity, object errCode, List<string> errors, Exception ex) {
        ErrorCode = errCode.ToString() ?? "None";
        Severity = severity;
        IsSuccess = isSuccess;
        ExceptionInfo = new CleanException(ex);
        Errors = errors;
    }

    internal Result(bool isSuccess, ResultSeverity severity, object errCode, List<string> errors,
        CleanException? ex = null) {
        ErrorCode = errCode.ToString() ?? "None";
        Severity = severity;
        IsSuccess = isSuccess;
        ExceptionInfo = ex;
        Errors = errors;
    }

    internal Result(bool isSuccess, ResultSeverity severity, object errCode) {
        ErrorCode = errCode.ToString() ?? "None";
        Severity = severity;
        IsSuccess = isSuccess;
        ExceptionInfo = null;
    }

    internal Result(Exception exception, ResultSeverity severity, List<string> errors) {
        ErrorCode = "ExceptionOccurred";
        Severity = severity;
        IsSuccess = false;
        ExceptionInfo = new CleanException(exception);
        Errors = errors;
    }

    internal Result(Exception exception, ResultSeverity severity = ResultSeverity.Fatal) {
        ErrorCode = "ExceptionOccurred";
        Severity = severity;
        IsSuccess = false;
        ExceptionInfo = new CleanException(exception);
    }

    internal Result(Exception exception, string errorCode, ResultSeverity severity = ResultSeverity.Fatal) {
        ErrorCode = errorCode;
        Severity = severity;
        IsSuccess = false;
        ExceptionInfo = new CleanException(exception);
    }

    #endregion

    #region PROPERTIES

    /// <summary>
    ///     Indicates success status of <see cref="Result" />.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    ///     Indicates fail status of <see cref="Result" />.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    public string ErrorCode { get; init; } = "UnsetError";
    public List<string> Errors { get; init; } = new();
    public ResultSeverity Severity { get; init; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public CleanException? ExceptionInfo { get; init; }

    #endregion

    #region OPERATORS

    public static implicit operator Result(bool value) {
        return !value ? Error("UnsetError") : Success();
    }

    public static implicit operator bool(Result value) {
        return value.IsSuccess;
    }

    #endregion

    #region METHOD CONVERTERS

    public ResultData<T> ToResultData<T>(T? data = default) {
        return new ResultData<T>(data, Severity, ErrorCode, Errors);
    }

    #endregion

    #region METHODS

    public Result AddError(string error) {
        Errors.Add(error);
        return this;
    }

    public Result WithNewErrorCode(string errorCode) {
        return new Result(IsSuccess, Severity, errorCode, Errors, ExceptionInfo);
    }

    public Result WithNewSeverity(ResultSeverity severity) {
        return new Result(IsSuccess, severity, ErrorCode, Errors, ExceptionInfo);
    }

    public Result WithNewErrors(List<string> errors) {
        return new Result(IsSuccess, Severity, ErrorCode, errors, ExceptionInfo);
    }

    public Result WithNewErrors(params string[] errors) {
        return new Result(IsSuccess, Severity, ErrorCode, errors.ToList(), ExceptionInfo);
    }

    #endregion

    #region CREATOR METHODS

    public static Result Create(bool isSuccess, ResultSeverity severity, object errCode, List<string> errors) {
        return new Result(isSuccess, severity, errCode, errors);
    }

    #endregion

    #region Success

    public static Result Success(string errorCode = "") {
        return new Result(true, ResultSeverity.Info, string.IsNullOrEmpty(errorCode) ? "Success" : errorCode);
    }

    public static ResultData<T> SuccessData<T>(T data, string? errorCode = "") {
        return new ResultData<T>(data,
            ResultSeverity.Info,
            string.IsNullOrEmpty(errorCode)
                ? "Success"
                : errorCode);
    }

    public static ResultData<T> SuccessData<T>(T data, string? errorCode, List<string> errors) {
        return new ResultData<T>(data,
            ResultSeverity.Info,
            string.IsNullOrEmpty(errorCode)
                ? "Success"
                : errorCode,
            errors);
    }

    public static Result Success(string errorCode, List<string> errors) {
        return new Result(true, ResultSeverity.Info, string.IsNullOrEmpty(errorCode) ? "Success" : errorCode, errors);
    }

    public static Result Success(List<string> errors) {
        return new Result(true, ResultSeverity.Info, "Success", errors);
    }

    #endregion

    #region Exception

    public static Result Exception(Exception exception, ResultSeverity severity = ResultSeverity.Fatal) {
        return new Result(exception, severity);
    }

    public static Result Exception(Exception exception, ResultSeverity severity, List<string> errors) {
        return new Result(exception, severity, errors);
    }

    public static Result Exception(Exception exception, ResultSeverity severity, string error1) {
        var errors = new List<string> { error1 };
        return new Result(exception, severity, errors);
    }

    public static Result Exception(Exception exception, ResultSeverity severity, string error1, string error2) {
        var errors = new List<string> { error1, error2 };
        return new Result(exception, severity, errors);
    }

    public static Result Exception(Exception exception, ResultSeverity severity, string error1, string error2,
        string error3) {
        var errors = new List<string> { error1, error2, error3 };
        return new Result(exception, severity, errors);
    }

    public static Result Exception(Exception exception, List<string> errors) {
        return new Result(exception, ResultSeverity.Fatal, errors);
    }

    public static Result Exception(Exception exception, string error1) {
        var errors = new List<string> { error1 };
        return new Result(exception, ResultSeverity.Fatal, errors);
    }

    public static Result Exception(Exception exception, string error1, string error2) {
        var errors = new List<string> { error1, error2 };
        return new Result(exception, ResultSeverity.Fatal, errors);
    }

    public static Result Exception(Exception exception, string error1, string error2, string error3) {
        var errors = new List<string> { error1, error2, error3 };
        return new Result(exception, ResultSeverity.Fatal, errors);
    }

    #endregion

    #region Warn

    public static Result Warn(object errorCode) {
        return new Result(false, ResultSeverity.Warn, errorCode);
    }

    public static Result Warn(object errorCode, List<string> errors) {
        return new Result(false, ResultSeverity.Warn, errorCode, errors);
    }

    public static Result Warn(object errorCode, string error1) {
        var errors = new List<string> { error1 };
        return new Result(false, ResultSeverity.Warn, errorCode, errors);
    }

    public static Result Warn(object errorCode, string error1, string error2) {
        var errors = new List<string> { error1, error2 };
        return new Result(false, ResultSeverity.Warn, errorCode, errors);
    }

    public static Result Warn(object errorCode, string error1, string error2, string error3) {
        var errors = new List<string> { error1, error2, error3 };
        return new Result(false, ResultSeverity.Warn, errorCode, errors);
    }

    #endregion

    #region Fatal

    public static Result Fatal(object errorCode) {
        return new Result(false, ResultSeverity.Fatal, errorCode);
    }

    public static Result Fatal(object errorCode, List<string> errors) {
        return new Result(false, ResultSeverity.Fatal, errorCode, errors);
    }

    public static Result Fatal(object errorCode, string error1) {
        var errors = new List<string> { error1 };
        return new Result(false, ResultSeverity.Fatal, errorCode, errors);
    }

    public static Result Fatal(object errorCode, string error1, string error2) {
        var errors = new List<string> { error1, error2 };
        return new Result(false, ResultSeverity.Fatal, errorCode, errors);
    }

    public static Result Fatal(object errorCode, string error1, string error2, string error3) {
        var errors = new List<string> { error1, error2, error3 };
        return new Result(false, ResultSeverity.Fatal, errorCode, errors);
    }

    #endregion

    #region Error

    public static Result Error(object errorCode) {
        return new Result(false, ResultSeverity.Error, errorCode);
    }

    public static Result Error(object errorCode, List<string> errors) {
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    public static Result Error(object errorCode, string error1) {
        var errors = new List<string> { error1 };
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    public static Result Error(object errorCode, string error1, string error2) {
        var errors = new List<string> { error1, error2 };
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    public static Result Error(object errorCode, string error1, string error2, string error3) {
        var errors = new List<string> { error1, error2, error3 };
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    #endregion

    #region Pre-defined

    public static Result Unauthorized() {
        return new Result(false, ResultSeverity.Error, "Unauthorized");
    }

    public static Result Forbidden() {
        return new Result(false, ResultSeverity.Error, "Forbidden");
    }

    public static Result ValidationError(List<string> errors) {
        return new Result(false, ResultSeverity.Error, EasMe.Result.ErrorCode.ValidationError, errors);
    }

    #endregion

    #region Multiple-Error

    public static Result MultipleErrors(object errorCode, List<string> errors) {
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    public static Result MultipleErrors(object errorCode, string error1) {
        var errors = new List<string> { error1 };
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    public static Result MultipleErrors(object errorCode, string error1, string error2) {
        var errors = new List<string> { error1, error2 };
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    public static Result MultipleErrors(object errorCode, string error1, string error2, string error3) {
        var errors = new List<string> { error1, error2, error3 };
        return new Result(false, ResultSeverity.Error, errorCode, errors);
    }

    public static Result MultipleErrors(List<string> errors) {
        return new Result(false, ResultSeverity.Error, "MultipleErrors", errors);
    }

    public static Result MultipleErrors(string error1) {
        var errors = new List<string> { error1 };
        return new Result(false, ResultSeverity.Error, "MultipleErrors", errors);
    }

    public static Result MultipleErrors(string error1, string error2) {
        var errors = new List<string> { error1, error2 };
        return new Result(false, ResultSeverity.Error, "MultipleErrors", errors);
    }

    public static Result MultipleErrors(string error1, string error2, string error3) {
        var errors = new List<string> { error1, error2, error3 };
        return new Result(false, ResultSeverity.Error, "MultipleErrors", errors);
    }

    #endregion
}