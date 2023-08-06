using System.Text.Json.Serialization;
using EasMe.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasMe.Result;

/// <summary>
///     A readonly struct Result to be used in Domain Driven Design mainly.
///     <br />
///     In order to avoid using <see cref="Exception" />'s and the performance downside from it.
/// </summary>
public sealed class Result
{
   internal Result()
   {
   }

   [Newtonsoft.Json.JsonIgnore]
   [JsonIgnore]
   public int HttpStatusCode { get; init; }
   /// <summary>
   ///     Indicates success status of <see cref="Result" />.
   /// </summary>
   public bool IsSuccess { get; init; } = false;

   /// <summary>
   ///     Indicates fail status of <see cref="Result" />.
   /// </summary>
   public bool IsFailure => !IsSuccess;

   /// <summary>
   /// Error code that indicates error type. This is used for localization.
   /// It is not recommended to use this for full messages.
   /// </summary>
   public string ErrorCode { get; init; } = "None";

   /// <summary>
   /// Localization parameter list for localization. 
   /// </summary>
   public Param[] Params { get; init; } = Array.Empty<Param>();
   public ResultLevel Level { get; init; } = ResultLevel.Info;

   protected internal CleanException? _exceptionInfo = null;

   public CleanException? GetException() => _exceptionInfo;



   #region OPERATORS
   public static implicit operator bool(Result value) => value.IsSuccess;
   public static implicit operator ActionResult(Result result) => result.ToActionResult();

   #endregion

   #region CONVERTERS

   public ResultData<T> ToResultData<T>(T? data = default) =>
      new()
      {
         ErrorCode = ErrorCode,
         Level = Level,
         Data = data,
         IsSuccess = IsSuccess,
         _exceptionInfo = _exceptionInfo
      };

   /// <summary>
   ///     Converts <see cref="Result" /> to <see cref="IActionResult" />. If result is failure, returns
   ///     <see cref="BadRequestObjectResult" />. If result is success, returns <see cref="OkObjectResult" />.
   /// </summary>
   /// <returns></returns>
   public  ActionResult ToActionResult()
   {
      var objResult = new ObjectResult(this)
      {
         StatusCode = HttpStatusCode
      };
      return objResult;
      // return IsSuccess ? new OkObjectResult(this) : objResult;
   }

   #endregion


   #region CREATE_METHODS:SUCCESS

   public static ResultData<T> SuccessData<T>(T data) =>
      new()
      {
         Data = data,
         IsSuccess = true,
         Level = ResultLevel.Success,
         ErrorCode = "None",
         _exceptionInfo = null
      };
   public static Result Success(string errorCode) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = true,
         Level = ResultLevel.Success,
         _exceptionInfo = null,
         HttpStatusCode = 200
      };

   public static Result Success(string errorCode, Param[] @params) =>
      new()
      {
         ErrorCode = errorCode,
         Params = @params,
         IsSuccess = true,
         Level = ResultLevel.Info,
         HttpStatusCode = 200
      };

   #endregion

   #region CREATE_METHODS:EXCEPTION

   public static Result Exception(Exception exception, string errorCode = "Exception") =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Exception,
         _exceptionInfo = new CleanException(exception),
         HttpStatusCode = 500,
      };
   public static Result Exception(Exception exception, string errorCode, Param[] @params) =>
      new()
      {
         ErrorCode = errorCode,
         Params = @params,
         IsSuccess = false,
         Level = ResultLevel.Exception,
         _exceptionInfo = new CleanException(exception),
         HttpStatusCode = 500,
      };

   #endregion

   #region CREATE_METHODS:WARN

   public static Result Warn(string errorCode) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Warn,
         _exceptionInfo = null,
         HttpStatusCode = 400,
      };
   public static Result Warn(string errorCode, Param[] @params) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Warn,
         _exceptionInfo = null,
         HttpStatusCode = 400,
         Params = @params
      };

   #endregion

   #region CREATE_METHODS:FATAL

   public static Result Fatal(Exception exception, string errorCode) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Fatal,
         HttpStatusCode = 500,
         _exceptionInfo = new CleanException(exception),
      };
   public static Result Fatal(Exception exception, string errorCode, Param[] @params) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Fatal,
         HttpStatusCode = 500,
         _exceptionInfo = new CleanException(exception),
         Params = @params
      };
   public static Result Fatal(string errorCode) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Fatal,
         HttpStatusCode = 500,
         _exceptionInfo = null
      };

   public static Result Fatal(string errorCode, Param[] @params) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Fatal,
         HttpStatusCode = 500,
         _exceptionInfo = null,
         Params = @params
      };

   #endregion

   #region CREATE_METHODS:ERROR

   public static Result Error(string errorCode, Param[] @params) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Error,
         HttpStatusCode = 400,
         Params = @params,
         _exceptionInfo = null
      };
   public static Result Error(string errorCode) =>
      new()
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = ResultLevel.Error,
         HttpStatusCode = 400,
         _exceptionInfo = null
      };

   #endregion

   #region CREATE_METHODS:BUILT_IN

   /// <summary>
   /// Creates <see cref="Result"/> object with <see cref="ResultLevel.Error"/> level and <see cref="HttpStatusCode"/> 404.
   /// </summary>
   /// <returns></returns>
   public static Result NotFound() =>
      new()
      {
         ErrorCode = "NotFound",
         _exceptionInfo = null,
         HttpStatusCode = 404,
         Level = ResultLevel.Error,
         IsSuccess = false,
      };

   /// <summary>
   /// Creates <see cref="Result"/> object with <see cref="ResultLevel.Error"/> level and <see cref="HttpStatusCode"/> 401.
   /// </summary>
   /// <returns></returns>
   public static Result Unauthorized() =>
      new()
      {
         ErrorCode = "Unauthorized",
         _exceptionInfo = null,
         Level = ResultLevel.Error,
         IsSuccess = false,
         HttpStatusCode = 401
      };
   /// <summary>
   /// Creates <see cref="Result"/> object with <see cref="ResultLevel.Error"/> level and <see cref="HttpStatusCode"/> 403.      
   /// </summary>
   /// <returns></returns>
   public static Result Forbidden() =>
      new()
      {
         ErrorCode = "Forbidden",
         _exceptionInfo = null,
         HttpStatusCode = 403,
         Level = ResultLevel.Error,
         IsSuccess = false,
      };

   #endregion

   #region CREATE_METHODS:VALIDATION

   /// <summary>
   /// Initializes <see cref="Result"/> object with multiple errors at once.
   ///
   /// Multiple errors stored in Param array and user-friendly data can be received through <see cref="GetParamValues"/> method or you can loop through Param array.
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
      int httpStatusCode = 400)
   {
      var locParams = @params.Select(x => new Param("error", x)).ToArray();
      return new Result
      {
         ErrorCode = errorCode,
         IsSuccess = false,
         Level = level,
         _exceptionInfo = null,
         HttpStatusCode = httpStatusCode,
         Params = locParams
      };
   }

   #endregion

   #region OTHERS
   /// <summary>
   /// Gets param values from <see cref="Params"/> property.
   /// </summary>
   /// <returns></returns>
   public List<object> GetParamValues()
   {
      return Params?.Select(x => x.Value).ToList() ?? new List<object>();
   }
   
   /// <summary>
   /// Gets severity ui class text for UI.
   /// </summary>
   /// <returns></returns>
   /// <exception cref="ArgumentOutOfRangeException"></exception>
   public string GetLevelForUI()
   {
      return Level switch
      {
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