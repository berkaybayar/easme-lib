using EasMe.Enums;
using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace EasMe.Models.ResultModels
{
    public readonly struct Result
    {
        private Result(ResultSeverity severity, ushort rv, string errCode, Exception? exception,params string[] parameters)
        {
            Rv = rv;
            ErrorString = errCode;
            Severity = severity;
            Parameters = parameters;
            ExceptionData = exception;
        }

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public readonly ushort Rv { get; init; } = ushort.MaxValue;
        public readonly bool IsSuccess { get => Rv == 0; }
        public readonly string ErrorString { get; init; }  = "None";
        public readonly string[] Parameters { get; init; }

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public readonly Exception? ExceptionData { get; init; }
        public readonly ResultSeverity Severity { get; init; }

        public static Result Success(params string[] parameters)
        {
            return new Result(ResultSeverity.INFO,0,"Success",null,parameters);
        }
        public static Result Success()
        {
            return new Result();
        }
        public static Result Error(ushort rv, ErrorCode err,params string[] parameters)
        {
            return new Result(ResultSeverity.ERROR,rv,err.ToString(),null,parameters);
        }
        public static Result Warn(ushort rv, ErrorCode err, params string[] parameters)
        {
            return new Result(ResultSeverity.WARN, rv, err.ToString(), null, parameters);
        }
        public static Result Fatal(ushort rv, ErrorCode err, params string[] parameters)
        {
            return new Result(ResultSeverity.FATAL, rv, err.ToString(), null, parameters);
        }
        public static Result Exception(ushort rv, Exception exception)
        {
            return new Result(ResultSeverity.EXCEPTION, rv, "EXCEPTION", exception);
        }
        public static Result DbInternal(ushort rv,params string[] parameters)
        {
            return new Result(ResultSeverity.FATAL, rv, ErrorCode.DbInternal.ToString(), null,parameters);
        }
    }
}
