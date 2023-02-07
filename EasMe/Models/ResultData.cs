using EasMe.Abstract;
using EasMe.Enums;

namespace EasMe.Models
{
    public readonly struct ResultData<T> : IResultData<T>
    {
        private ResultData(T? data, ResultSeverity severity, ushort rv, object errCode, params string[] parameters)
        {
            Rv = rv;
            ErrorCode = errCode.ToString() ?? "None";
            Severity = severity;
            Parameters = parameters;
            Data = data;
        }

        public readonly ushort Rv { get; init; } = ushort.MaxValue;
        public readonly ResultSeverity Severity { get; init; }
        public readonly bool IsSuccess
        {
            get => Rv == 0 && Data is not null;
        }
        public readonly string ErrorCode { get; init; } = "None";
        public readonly string[] Parameters { get; init; }

        public readonly T? Data { get; init; }

        public static ResultData<T> Success(T data, params string[] parameters)
        {
            return new ResultData<T>(data, ResultSeverity.INFO, 0, "Success", parameters);
        }
        public static ResultData<T> Success(T data)
        {
            return new ResultData<T>(data, ResultSeverity.INFO, 0, "Success");
        }
        public static ResultData<T> Error(ushort rv, object err, params string[] parameters)
        {
            return new ResultData<T>(default, ResultSeverity.ERROR, rv, err, parameters);
        }
        public static ResultData<T> Warn(ushort rv, object err, params string[] parameters)
        {
            return new ResultData<T>(default, ResultSeverity.WARN, rv, err, parameters);
        }
        public static ResultData<T> Fatal(ushort rv, object err, params string[] parameters)
        {
            return new ResultData<T>(default, ResultSeverity.FATAL, rv, err, parameters);
        }

        public static ResultData<T> DbInternal(ushort rv, params string[] parameters)
        {
            return new ResultData<T>(default, ResultSeverity.FATAL, rv, Enums.ErrorCode.DbInternal.ToString(), parameters);
        }

        public static implicit operator ResultData<T>(T value)
        {
            if (value is null) return ResultData<T>.Error(100, Enums.ErrorCode.NullReference, nameof(T));
            return ResultData<T>.Success(value);
        }
    }
}
