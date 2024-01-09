using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasMe.Result;

/// <summary>
///   A readonly struct Result to be used in Domain Driven Design mainly.
///   <br />
///   In order to avoid using <see cref="Exception" />'s and the performance downside from it.
/// </summary>
public sealed class Result
{
  internal CleanException? ExceptionInfo;

  internal Result() { }

  [JsonIgnore]
  [System.Text.Json.Serialization.JsonIgnore]
  public int HttpStatusCode { get; init; }

  /// <summary>
  ///   Indicates success status of <see cref="Result" />.
  /// </summary>
  public bool IsSuccess { get; init; }

  /// <summary>
  ///   Indicates fail status of <see cref="Result" />.
  /// </summary>
  public bool IsFailure => !IsSuccess;

  /// <summary>
  ///   Error code that indicates error type. This is used for localization.
  ///   It is not recommended to use this for full messages.
  /// </summary>
  public string ErrorCode { get; init; } = "None";

  /// <summary>
  ///   Localization parameter list for localization.
  /// </summary>
  public Param[] Params { get; init; } = Array.Empty<Param>();

  public ResultLevel Level { get; init; } = ResultLevel.Info;

  public CleanException? GetException() {
    return ExceptionInfo;
  }

  #region CREATE_METHODS:VALIDATION

  /// <summary>
  ///   Initializes <see cref="Result" /> object with multiple errors at once.
  ///   Multiple errors stored in Param array and user-friendly data can be received through <see cref="GetParamValues" />
  ///   method or you can loop through Param array.
  /// </summary>
  /// <param name="params"></param>
  /// <param name="errorCode"></param>
  /// <param name="level"></param>
  /// <param name="httpStatusCode"></param>
  /// <returns></returns>
  public static Result MultipleErrors(
    string[] @params,
    string errorCode = "MultipleErrors",
    ResultLevel level = ResultLevel.Error,
    int httpStatusCode = 400) {
    var locParams = @params.Select(x => new Param("error", x)).ToArray();
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = level,
      ExceptionInfo = null,
      HttpStatusCode = httpStatusCode,
      Params = locParams
    };
  }

  #endregion


  #region OPERATORS

  public static implicit operator bool(Result value) {
    return value.IsSuccess;
  }

  public static implicit operator ActionResult(Result result) {
    return result.ToActionResult();
  }

  #endregion

  #region CONVERTERS

  public ResultData<T> ToResultData<T>(T? data = default) {
    return new ResultData<T> {
      ErrorCode = ErrorCode,
      Level = Level,
      Data = data,
      IsSuccess = IsSuccess,
      ExceptionInfo = ExceptionInfo
    };
  }

  /// <summary>
  ///   Converts <see cref="Result" /> to <see cref="IActionResult" />. If result is failure, returns
  ///   <see cref="BadRequestObjectResult" />. If result is success, returns <see cref="OkObjectResult" />.
  /// </summary>
  /// <returns></returns>
  public ActionResult ToActionResult() {
    var objResult = new ObjectResult(this) {
      StatusCode = HttpStatusCode
    };
    return objResult;
    // return IsSuccess ? new OkObjectResult(this) : objResult;
  }

  #endregion


  #region CREATE_METHODS:SUCCESS

  public static ResultData<T> SuccessData<T>(T data) {
    return new ResultData<T> {
      Data = data,
      IsSuccess = true,
      Level = ResultLevel.Success,
      ErrorCode = "None",
      ExceptionInfo = null
    };
  }

  public static Result Success(string errorCode) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = true,
      Level = ResultLevel.Success,
      ExceptionInfo = null,
      HttpStatusCode = 200
    };
  }

  public static Result Success(string errorCode, Param[] @params) {
    return new Result {
      ErrorCode = errorCode,
      Params = @params,
      IsSuccess = true,
      Level = ResultLevel.Info,
      HttpStatusCode = 200
    };
  }

  #endregion

  #region CREATE_METHODS:EXCEPTION

  public static Result Exception(Exception exception, string errorCode = "Exception") {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Exception,
      ExceptionInfo = new CleanException(exception),
      HttpStatusCode = 500
    };
  }

  public static Result Exception(Exception exception, string errorCode, Param[] @params) {
    return new Result {
      ErrorCode = errorCode,
      Params = @params,
      IsSuccess = false,
      Level = ResultLevel.Exception,
      ExceptionInfo = new CleanException(exception),
      HttpStatusCode = 500
    };
  }

  #endregion

  #region CREATE_METHODS:WARN

  public static Result Warn(string errorCode) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Warn,
      ExceptionInfo = null,
      HttpStatusCode = 400
    };
  }

  public static Result Warn(string errorCode, Param[] @params) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Warn,
      ExceptionInfo = null,
      HttpStatusCode = 400,
      Params = @params
    };
  }

  #endregion

  #region CREATE_METHODS:FATAL

  public static Result Fatal(Exception exception, string errorCode) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Fatal,
      HttpStatusCode = 500,
      ExceptionInfo = new CleanException(exception)
    };
  }

  public static Result Fatal(Exception exception, string errorCode, Param[] @params) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Fatal,
      HttpStatusCode = 500,
      ExceptionInfo = new CleanException(exception),
      Params = @params
    };
  }

  public static Result Fatal(string errorCode) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Fatal,
      HttpStatusCode = 500,
      ExceptionInfo = null
    };
  }

  public static Result Fatal(string errorCode, Param[] @params) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Fatal,
      HttpStatusCode = 500,
      ExceptionInfo = null,
      Params = @params
    };
  }

  #endregion

  #region CREATE_METHODS:ERROR

  public static Result Error(string errorCode, Param[] @params) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Error,
      HttpStatusCode = 400,
      Params = @params,
      ExceptionInfo = null
    };
  }

  public static Result Error(string errorCode) {
    return new Result {
      ErrorCode = errorCode,
      IsSuccess = false,
      Level = ResultLevel.Error,
      HttpStatusCode = 400,
      ExceptionInfo = null
    };
  }

  #endregion

  #region CREATE_METHODS:BUILT_IN

  /// <summary>
  ///   Creates <see cref="Result" /> object with <see cref="ResultLevel.Error" /> level and <see cref="HttpStatusCode" />
  ///   404.
  /// </summary>
  /// <returns></returns>
  public static Result NotFound() {
    return new Result {
      ErrorCode = "NotFound",
      ExceptionInfo = null,
      HttpStatusCode = 404,
      Level = ResultLevel.Error,
      IsSuccess = false
    };
  }

  /// <summary>
  ///   Creates <see cref="Result" /> object with <see cref="ResultLevel.Error" /> level and <see cref="HttpStatusCode" />
  ///   401.
  /// </summary>
  /// <returns></returns>
  public static Result Unauthorized() {
    return new Result {
      ErrorCode = "Unauthorized",
      ExceptionInfo = null,
      Level = ResultLevel.Error,
      IsSuccess = false,
      HttpStatusCode = 401
    };
  }

  /// <summary>
  ///   Creates <see cref="Result" /> object with <see cref="ResultLevel.Error" /> level and <see cref="HttpStatusCode" />
  ///   403.
  /// </summary>
  /// <returns></returns>
  public static Result Forbidden() {
    return new Result {
      ErrorCode = "Forbidden",
      ExceptionInfo = null,
      HttpStatusCode = 403,
      Level = ResultLevel.Error,
      IsSuccess = false
    };
  }

  #endregion

  #region OTHERS

  /// <summary>
  ///   Gets param values from <see cref="Params" /> property.
  /// </summary>
  /// <returns></returns>
  public List<object> GetParamValues() {
    return Params?.Select(x => x.Value).ToList() ?? new List<object>();
  }

  /// <summary>
  ///   Gets severity ui class text for UI.
  /// </summary>
  /// <returns></returns>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public string GetLevelForUI() {
    return Level switch {
      ResultLevel.Info => "info",
      ResultLevel.Warn => "warn",
      ResultLevel.Error => "danger",
      ResultLevel.Fatal => "danger",
      ResultLevel.Exception => "danger",
      _ => throw new ArgumentOutOfRangeException()
    };
  }

  #endregion
}