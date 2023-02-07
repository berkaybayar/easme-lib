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
        private Result(ResultSeverity severity, ushort rv, object errCode)
        {
            Rv = rv;
            ErrorCode = errCode.ToString() ?? "None";
            Severity = severity;
        }

        public ushort Rv { get; init; } = ushort.MaxValue;
        public bool IsSuccess => Rv == 0;
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; init; } = "None";

        public ResultSeverity Severity { get; init; }

        public static Result Success()
        {
            return new Result(ResultSeverity.Info, 0, "Success");
        }
        public static Result Success(string operationName)
        {
            return new Result(ResultSeverity.Info,0,operationName);
        }
        public static Result Error(ushort rv, object errorCode)
        {
            return new Result(ResultSeverity.Error, rv, errorCode);
        }
        public static Result Warn(ushort rv, object errorCode)
        {
            return new Result(ResultSeverity.Warn, rv, errorCode);
        }
        public static Result Fatal(ushort rv, object errorCode)
        {
            return new Result(ResultSeverity.Fatal, rv, errorCode);
        }

        
    }
}
