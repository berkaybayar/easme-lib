using EasMe.Models;
using Newtonsoft.Json;

namespace EasMe.Result;

/// <summary>
///     A readonly struct Result with Data of T type to be used in Domain Driven Design mainly.
///     <br />
///     In order to avoid using <see cref="Exception" />'s and the performance downside from it.
/// </summary>
public readonly struct ResultData<T>
{
    #region CONSTRUCTORS

    internal ResultData(T? data, ResultSeverity severity, object errCode)
    {
        ErrorCode = errCode.ToString() ?? "None";
        Severity = severity;
        Data = data;
        IsSuccess = data != null;
        Exception = null;
    }

    internal ResultData(T? data, ResultSeverity severity, object errCode, List<string> errors)
    {
        ErrorCode = errCode.ToString() ?? "None";
        Severity = severity;
        Data = data;
        IsSuccess = data != null;
        Exception = null;
        Errors = errors;
    }
    internal ResultData(ResultSeverity severity, object errCode, List<string> errors)
    {
        ErrorCode = errCode.ToString() ?? "None";
        Severity = severity;
        IsSuccess = false;
        Exception = null;
        Errors = errors;
    }
    internal ResultData(Exception exception, ResultSeverity severity, List<string> errors)
    {
        ErrorCode = "ExceptionOccured";
        Severity = severity;
        Data = default;
        Errors = errors;
        IsSuccess = false;
        Exception = new CleanException(exception);
    }

    internal ResultData(Exception exception, ResultSeverity severity)
    {
        ErrorCode = "ExceptionOccured";
        Severity = severity;
        Data = default;
        IsSuccess = false;
        Exception = new CleanException(exception);
    }

    #endregion

    #region PROPERTIES

    public ResultSeverity Severity { get; init; }
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public string ErrorCode { get; init; } = "UnsetError";
    public List<string> Errors { get; init; } = new();

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T? Data { get; init; } = (T?)default;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public CleanException? Exception { get; init; }

    #endregion

    #region OPERATORS

    public static implicit operator T?(ResultData<T> res)
    {
        return res.Data;
    }

    public static implicit operator ResultData<T>(T? value)
    {
        return value is null
            ? Result.Error($"{nameof(T)}.NullReference")
            : Result.SuccessData(value);
    }

    public static implicit operator bool(ResultData<T> value)
    {
        return value.IsSuccess;
    }

    /// <summary>
    ///     Implicit operator for <see cref="Result" /> to <see cref="ResultData{T}" />
    ///     <br />
    ///     If <see cref="Result" /> status is success conversion t
    /// </summary>
    /// <param name="result"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static implicit operator ResultData<T>(Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException(
                "Implicit conversion from Result to ResultData<T> is not possible if ResultState is success");
        return new ResultData<T>(result.Severity, result.ErrorCode,result.Errors);
    }

    #endregion

    #region MethodConverters

    public Result ToResult()
    {
        return new Result(IsSuccess, Severity, ErrorCode);
    }

    public ResultData<T> ToNew(T? data)
    {
        return new ResultData<T>(data, Severity, Errors, Errors);
    }

    #endregion
}