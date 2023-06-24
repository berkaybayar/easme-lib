using EasMe.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasMe.Result;

/// <summary>
///     A readonly struct Result with Data of T type to be used in Domain Driven Design mainly.
///     <br />
///     In order to avoid using <see cref="ExceptionInfo" />'s and the performance downside from it.
/// </summary>
public readonly struct ResultData<T>  {
    

    public ResultData() {}

    public ResultSeverity Severity { get; init; } = ResultSeverity.None;
    public bool IsSuccess { get; init; } = false;
    public bool IsFailure => !IsSuccess;
    public string ErrorCode { get; init; } = "UnsetError";
    public List<string> Errors { get; init; } = new();
    public List<ValidationError> ValidationErrors { get; init; } = new();

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T? Data { get; init; } = default;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public CleanException? ExceptionInfo { get; init; } = null;


    #region OPERATORS

    public static implicit operator T?(ResultData<T> res) {
        return res.Data;
    }

    public static implicit operator ResultData<T>(T? value) {
        return value is null
            ? new ResultData<T>() {
                ErrorCode = "UnsetError",
                ExceptionInfo = null,
                Severity = ResultSeverity.Error,
                IsSuccess = false,
            }
            : new ResultData<T>() {
                ErrorCode = "Success",
                Severity = ResultSeverity.Info,
                ExceptionInfo = null,
                Data = value,
                IsSuccess = true,
            };
    }

    public static implicit operator bool(ResultData<T> value) {
        return value.IsSuccess;
    }

    public static implicit operator ActionResult(ResultData<T> result) {
        return result.ToActionResult();
    }
    #endregion
    
    
    #region MethodConverters

    public Result ToResult() {
        return new Result() {  
            ErrorCode = this.ErrorCode,
            Severity = Severity,
            ExceptionInfo = ExceptionInfo,
            IsSuccess = IsSuccess,
            Errors = Errors,
            ValidationErrors = ValidationErrors
        };
    }

    public ResultData<T2> Map<T2>(Func<T,T2> action) {
        var res = action(Data);
        return new ResultData<T2>() {
            ErrorCode = ErrorCode,
            Severity = Severity,
            Data = res,
            IsSuccess = IsSuccess,
            ExceptionInfo = ExceptionInfo,
        };
        
    }

    /// <summary>
    ///   Converts <see cref="Result" /> to <see cref="IActionResult" />. If result is failure, returns <see cref="ObjectResult" /> with <paramref name="failStatusCode" />. If result is success, returns <see cref="OkObjectResult" />.
    /// </summary>
    /// <param name="failStatusCode"></param>
    /// <returns></returns>
    public ActionResult ToActionResult(int failStatusCode) {
        return IsSuccess ? new OkObjectResult(this) : new ObjectResult(this) { StatusCode = failStatusCode };
    }
    
    /// <summary>
    /// Converts <see cref="Result"/> to <see cref="IActionResult"/>. If result is failure, returns <see cref="BadRequestObjectResult"/>. If result is success, returns <see cref="OkObjectResult"/>.
    /// </summary>
    /// <returns></returns>
    public ActionResult ToActionResult() {
        return IsSuccess ? new OkObjectResult(this) : new BadRequestObjectResult(this);
    }

    #endregion
}