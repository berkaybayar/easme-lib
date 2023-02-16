using EasMe.Abstract;
using EasMe.Enums;

namespace EasMe.Models
{
    /// <summary>
    /// A readonly struct Result to be used in Domain Driven Design mainly.
    /// <br/>
    /// In order to avoid using <see cref="Exception"/>'s and the performance downside from it.
    /// </summary>
    public readonly struct Result : IResult
    {
        public Result(ResultSeverity severity, ushort rv, object errCode,params string[] errors)
        {
            Rv = rv;
            ErrorCode = errCode.ToString() ?? "None";
            Severity = severity;
            Errors = errors;
        }

        public ushort Rv { get; init; } = ushort.MaxValue;
        public bool IsSuccess => Rv == 0;
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; init; } = "None";
        public string[] Errors { get; init; }
        public ResultSeverity Severity { get; init; }

        public Result WithoutRv()
        {
            return this;
            //TODO: Check same as resultdata
            return new Result(Severity, ushort.MaxValue, ErrorCode);
        }


        public static Result Success()
        {
            return new Result(ResultSeverity.Info, 0, "Success");
        }
        public static Result Success(string operationName)
        {
            return new Result(ResultSeverity.Info,0,operationName);
        }
        public static Result Error(ushort rv, object errorCode, params string[] errors)
        {
            return new Result(ResultSeverity.Error, rv, errorCode, errors);
        }
        public static Result Warn(ushort rv, object errorCode, params string[] errors)
        {
            return new Result(ResultSeverity.Warn, rv, errorCode, errors);
        }
        public static Result Fatal(ushort rv, object errorCode, params string[] errors)
        {
            return new Result(ResultSeverity.Fatal, rv, errorCode, errors);
        }

        public static implicit operator Result(bool value)
        {
            if (!value)
            {
                return Result.Error(ushort.MaxValue, "UnsetError");
            }

            return Result.Success();

        }

        public ResultData<T> ToResultData<T>(T? data = default)
        {
            return new ResultData<T>(data,Severity,Rv,ErrorCode,Errors);
        }
    }
}
