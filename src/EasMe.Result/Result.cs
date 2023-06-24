using System.Net;
using EasMe.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasMe.Result;

/// <summary>
///     A readonly struct Result to be used in Domain Driven Design mainly.
///     <br />
///     In order to avoid using <see cref="Exception" />'s and the performance downside from it.
/// </summary>
public readonly struct Result
{

    public Result() { }

    /// <summary>
    ///     Indicates success status of <see cref="Result" />.
    /// </summary>
    public bool IsSuccess { get; init; }  = false;

    /// <summary>
    ///     Indicates fail status of <see cref="Result" />.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    public string ErrorCode { get; init; }  = "None";
    public List<string> Errors { get; init; } = new();
    public List<ValidationError> ValidationErrors { get; init; } = new();
    public ResultSeverity Severity { get; init; }  = ResultSeverity.None;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public CleanException? ExceptionInfo { get; init; } = null;

#region OPERATORS

    public static implicit operator Result(bool value) {
        return !value ? Error("UnsetError") : Success("Success");
    }

    public static implicit operator bool(Result value) {
        return value.IsSuccess;
    }
    

    public static implicit operator ActionResult(Result result) {
        return result.ToActionResult();
    }

#endregion

#region CONVERTERS

    public ResultData<T> ToResultData<T>(T? data = default) {
        return new ResultData<T>() {
            ErrorCode = this.ErrorCode,
            Severity = this.Severity,
            Data = data,
            IsSuccess = this.IsSuccess,
            ExceptionInfo = this.ExceptionInfo,
        };
    }

    /// <summary>
    /// Converts <see cref="Result"/> to <see cref="IActionResult"/>. If result is failure, returns <see cref="BadRequestObjectResult"/>. If result is success, returns <see cref="OkObjectResult"/>.
    /// </summary>
    /// <returns></returns>
    public ActionResult ToActionResult() {
        return IsSuccess ? new OkObjectResult(this) : new BadRequestObjectResult(this);
    }

    /// <summary>
    ///   Converts <see cref="Result" /> to <see cref="IActionResult" />. If result is failure, returns <see cref="ObjectResult" /> with <paramref name="failStatusCode" />. If result is success, returns <see cref="OkObjectResult" />.
    /// </summary>
    /// <param name="failStatusCode"></param>
    /// <returns></returns>
    public ActionResult ToActionResult(int failStatusCode) {
        return IsSuccess ? new OkObjectResult(this) : new ObjectResult(this) { StatusCode = failStatusCode };
    }

#endregion

#region CREATE METHODS
    public static Result Create(bool isSuccess, ResultSeverity severity, string errCode, List<string> errors, List<ValidationError> validationErrors) {
        return new Result() {
            IsSuccess = isSuccess,
            Severity = severity,
            ErrorCode = errCode,
            Errors = errors,
            ExceptionInfo = null,
            ValidationErrors = validationErrors
        };

    }
    
    public static Result Success(string errorCode) {
        return new Result() {  
            ErrorCode = errorCode,
            IsSuccess = true,
            Severity = ResultSeverity.Info,
            ExceptionInfo = null,
            
        };
    }

    public static Result Success(string errorCode, List<string> errors) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors,
            IsSuccess = true,
            Severity = ResultSeverity.Info
        };
    }
    public static Result Success(List<string> errors) {
        return new Result() {
            ErrorCode = "Success",
            Errors = errors,
            IsSuccess = true,
            Severity = ResultSeverity.Info
        };
    }

    public static Result Exception(Exception exception) {
        return new Result() {
            ErrorCode = "Exception",
            IsSuccess = false,
            Severity = ResultSeverity.Exception
        };
    }
    public static Result Exception(Exception exception, List<string> errors) {
        return new Result() {
            ErrorCode = "Exception",
            Errors = errors,
            IsSuccess = false,
            Severity = ResultSeverity.Exception
        };
    }
    public static Result Exception(Exception exception, params string[] errors) {
        return new Result() {
            ErrorCode = "Exception",
            Errors = errors.ToList(),
            IsSuccess = false,
            Severity = ResultSeverity.Exception
        };
    }
    

    public static Result Warn(string errorCode) {
        return new Result() {
            ErrorCode = errorCode,
            IsSuccess = false,
            Severity = ResultSeverity.Warn,
        };
    }

    public static Result Warn<T>(T errorEnum) where T : Enum{
        return new Result() {
            ErrorCode = errorEnum.ToString(),
            IsSuccess = false,
            Severity = ResultSeverity.Warn,
        };
    }
    public static Result Warn(string errorCode, List<string> errors) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors,
            IsSuccess = false,
            Severity = ResultSeverity.Warn
        };
    }
    public static Result Warn(string errorCode, params string[] errors) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors.ToList(),
            IsSuccess = false,
            Severity = ResultSeverity.Warn
        };
    }
    public static Result Fatal(string errorCode) {
        return new Result() {
            ErrorCode = errorCode,
            IsSuccess = false,
            Severity = ResultSeverity.Fatal,
        };
    }
    public static Result Fatal<T>(T errorEnum) where T : Enum{
        return new Result() {
            ErrorCode = errorEnum.ToString(),
            IsSuccess = false,
            Severity = ResultSeverity.Fatal,
        };
    }

    public static Result Fatal(string errorCode, List<string> errors) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors,
            IsSuccess = false,
            Severity = ResultSeverity.Fatal
        };
    }
    public static Result Fatal(string errorCode, params string[] errors) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors.ToList(),
            IsSuccess = false,
            Severity = ResultSeverity.Fatal
        };
    }


   
    public static Result Error(string errorCode, List<string> errors) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors,
            IsSuccess = false,
            Severity = ResultSeverity.Error
        };
    }

    public static Result Error(string errorCode) {
        return new Result() {
            ErrorCode = errorCode,
            IsSuccess = false,
            Severity = ResultSeverity.Error,
        };
    }

    public static Result Error(string errorCode, params string[] errors) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors.ToList(),
            IsSuccess = false,
            Severity = ResultSeverity.Error
        };
    }
    public static Result Error<T>(T errorEnum) where T : Enum{
        return new Result() {
            ErrorCode = errorEnum.ToString(),
            IsSuccess = false,
            Severity = ResultSeverity.Error,
        };
    }
    public static Result NotFound() {
        return new Result() {
            ErrorCode = "NotFound",
        };
    }

    public static Result Unauthorized() {
        return new Result() {
            ErrorCode = "Unauthorized",
        };
    }

    public static Result Forbidden() {
        return new Result() {
            ErrorCode = "Forbidden",
        };
    }

    public static Result ValidationError(List<ValidationError> validationErrors) {
        return new Result() {
            ValidationErrors = validationErrors,
            Severity = ResultSeverity.Validation,
            IsSuccess = false,
            ErrorCode = "ValidationError",
        };
    }

    public static Result MultipleErrors(List<string> errors, string errorCode = "MultipleErrors", ResultSeverity severity = ResultSeverity.Error) {
        return new Result() {
            ErrorCode = errorCode,
            Errors = errors,
            IsSuccess = false,
            Severity = severity,
        };
    }
#endregion
}