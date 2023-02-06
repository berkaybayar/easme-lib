using EasMe.DDD.Abstract;
using EasMe.Enums;

namespace EasMe.Models
{
    public readonly struct Result : IResult
    {
        private Result(ResultSeverity severity, ushort rv, object errCode, params string[] parameters)
        {
            Rv = rv;
            ErrorCode = errCode.ToString() ?? "None";
            Severity = severity;
            Parameters = parameters;
        }

        public readonly ushort Rv { get; init; } = ushort.MaxValue;
        public readonly bool IsSuccess { get => Rv == 0; }
        public readonly string ErrorCode { get; init; } = "None";
        public readonly string[] Parameters { get; init; }

        public readonly ResultSeverity Severity { get; init; }

        public static Result Success(params string[] parameters)
        {
            return new Result(ResultSeverity.INFO, 0, "Success", parameters);
        }
        public static Result Success()
        {
            return new Result();
        }
        public static Result Error(ushort rv, object errorCode, params string[] parameters)
        {
            return new Result(ResultSeverity.ERROR, rv, errorCode, parameters);
        }
        public static Result Warn(ushort rv, object errorCode, params string[] parameters)
        {
            return new Result(ResultSeverity.WARN, rv, errorCode, parameters);
        }
        public static Result Fatal(ushort rv, object errorCode, params string[] parameters)
        {
            return new Result(ResultSeverity.FATAL, rv, errorCode, parameters);
        }

        public static Result DbInternal(ushort rv, params string[] parameters)
        {
            return new Result(ResultSeverity.FATAL, rv, Enums.ErrorCode.DbInternal.ToString(), parameters);
        }
    }
}
