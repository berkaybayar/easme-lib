using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasMe.Result;

/// <summary>
///   A readonly struct Result with Data of T type to be used in Domain Driven Design mainly.
///   <br />
///   In order to avoid using <see cref="Exception" />'s and the performance downside from it.
/// </summary>
public sealed class ResultData<T>
{
  internal CleanException? ExceptionInfo;

  internal ResultData() { }

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

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public T? Data { get; init; }

  public CleanException? GetException() {
    return ExceptionInfo;
  }


  #region OPERATORS

  public static implicit operator ResultData<T>(Result res) {
    return new ResultData<T> {
      ErrorCode = res.ErrorCode,
      Level = res.Level,
      IsSuccess = res.IsSuccess,
      ExceptionInfo = res.GetException(),
      Data = default,
      Params = res.Params,
      HttpStatusCode = res.HttpStatusCode
    };
  }

  public static implicit operator Result(ResultData<T> res) {
    return new Result {
      ErrorCode = res.ErrorCode,
      Level = res.Level,
      IsSuccess = res.IsSuccess,
      ExceptionInfo = res.ExceptionInfo,
      Params = res.Params,
      HttpStatusCode = res.HttpStatusCode
    };
  }

  public static implicit operator T?(ResultData<T> res) {
    return res.Data;
  }

  public static implicit operator ResultData<T>(T? value) {
    return value is null
             ? new ResultData<T> {
               ErrorCode = "NullValue",
               Level = ResultLevel.Error,
               IsSuccess = false,
               ExceptionInfo = null,
               Data = value
             }
             : new ResultData<T> {
               ErrorCode = "Success",
               Level = ResultLevel.Info,
               Data = value,
               IsSuccess = true,
               HttpStatusCode = 200,
               ExceptionInfo = null
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
    return new Result {
      ErrorCode = ErrorCode,
      Level = Level,
      IsSuccess = IsSuccess,
      Params = Params,
      HttpStatusCode = HttpStatusCode,
      ExceptionInfo = ExceptionInfo
    };
  }

  /// <summary>
  ///   Creates new instance of ResultData type with new data type.
  /// </summary>
  /// <param name="action"></param>
  /// <typeparam name="T2"></typeparam>
  /// <returns></returns>
  public ResultData<T2> Map<T2>(Func<T?, T2> action) {
    var res = action(Data);
    return new ResultData<T2> {
      ErrorCode = ErrorCode,
      Level = Level,
      Data = res,
      IsSuccess = IsSuccess,
      ExceptionInfo = ExceptionInfo,
      Params = Params,
      HttpStatusCode = HttpStatusCode
    };
  }

  /// <summary>
  ///   Converts <see cref="Result" /> to <see cref="IActionResult" />. If result is failure, returns
  ///   <see cref="ObjectResult" /> with <paramref name="failStatusCode" />. If result is success, returns
  ///   <see cref="OkObjectResult" />.
  /// </summary>
  /// <param name="failStatusCode"></param>
  /// <returns></returns>
  public ActionResult ToActionResult(int failStatusCode) {
    return IsSuccess
             ? new OkObjectResult(this)
             : new ObjectResult(this) { StatusCode = failStatusCode };
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
}