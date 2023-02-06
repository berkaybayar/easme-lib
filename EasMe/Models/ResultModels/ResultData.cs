using EasMe.Enums;
using EasMe.Extensions;
using System.Text.Json.Serialization;

namespace EasMe.Models.ResultModels
{
	public readonly struct ResultData<T>
	{
        private ResultData(T? data,ResultSeverity severity, ushort rv, string errCode, Exception? exception, params string[] parameters)
        {
            Rv = rv;
            ErrorString = errCode;
            Severity = severity;
            Parameters = parameters;
            ExceptionData = exception;
            Data = data;
        }

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public readonly ushort Rv { get; init; } = ushort.MaxValue;
        public readonly ResultSeverity Severity { get; init; }
        public readonly bool IsSuccess { get => Rv == 0; }
        public readonly string ErrorString { get; init; } = "None";
        public readonly string[] Parameters { get; init; }

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public readonly Exception? ExceptionData { get; init; }
        public readonly T? Data { get; init; }

        public static ResultData<T> Success(T data,params string[] parameters)
        {
            return new ResultData<T>(data,ResultSeverity.INFO, 0, "Success", null, parameters);
        }
        public static ResultData<T> Success(T data)
        {
            return new ResultData<T>(data, ResultSeverity.INFO, 0, "Success", null);
        }
        public static ResultData<T> Error(ushort rv, ErrorCode err, params string[] parameters)
        {
            return new ResultData<T>(default, ResultSeverity.ERROR, rv, err.ToString(), null, parameters);
        }
        public static ResultData<T> Warn(ushort rv, ErrorCode err, params string[] parameters)
        {
            return new ResultData<T>(default, ResultSeverity.WARN, rv, err.ToString(), null, parameters);
        }
        public static ResultData<T> Fatal(ushort rv, ErrorCode err, params string[] parameters)
        {
            return new ResultData<T>(default, ResultSeverity.FATAL, rv, err.ToString(), null, parameters);
        }
        public static ResultData<T> Exception(ushort rv, Exception exception)
        {
            return new ResultData<T>(default, ResultSeverity.EXCEPTION, rv, "EXCEPTION", exception);
        }
        public static ResultData<T> DbInternal(ushort rv, params string[] parameters)
        {
            return new ResultData<T>(default,ResultSeverity.FATAL, rv, ErrorCode.DbInternal.ToString(), null, parameters);
        }

        public static implicit operator ResultData<T>(T value)
        {
            if (value is null) return ResultData<T>.Error(100, ErrorCode.NullReference,nameof(T));
            return ResultData<T>.Success(value);
        }
    }
}
