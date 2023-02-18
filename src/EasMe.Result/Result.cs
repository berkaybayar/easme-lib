namespace EasMe.Result
{
    /// <summary>
    /// A readonly struct Result to be used in Domain Driven Design mainly.
    /// <br/>
    /// In order to avoid using <see cref="Exception"/>'s and the performance downside from it.
    /// </summary>
    public readonly struct Result : IResult
    {
        public Result(ResultSeverity severity, int rv, object errCode,params string[] errors)
        {
            Rv = rv;
            ErrorCode = errCode.ToString() ?? "None";
            Severity = severity;
            Errors = errors;
            if (IsSuccess)
            {
                Severity = ResultSeverity.Info;
            }
        }

        public int Rv { get; init; } = ushort.MaxValue;
        public bool IsSuccess => Rv == 0;
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; init; }
        public string[] Errors { get; init; }
        public ResultSeverity Severity { get; init; }

        public Result WithoutRv()
        {
            ushort rv = 0;
            if (IsFailure) rv = ushort.MaxValue;
            return new Result(Severity, rv, ErrorCode,Errors);
        }

        public Result MultiplyRv(ushort value)
        {
            return new Result(Severity, Convert.ToInt32(value * Rv), ErrorCode,Errors);
        }

        public ResultData<T> ToResultData<T>(T? data = default)
        {
            return new ResultData<T>(data, Severity, Rv, ErrorCode, Errors);
        }

        public static implicit operator Result(bool value)
        {
            if (!value)
            {
                return Result.Error(ushort.MaxValue, "UnsetError");
            }
            return Result.Success();

        }

        //CREATE METHODS
        public static Result Success()
        {
            return new Result(ResultSeverity.Info, 0, "Success");
        }
        public static Result Success(string operationName)
        {
            return new Result(ResultSeverity.Info,0,operationName);
        }
        public static Result Error(int rv, object errorCode, params string[] errors)
        {
            return new Result(ResultSeverity.Error, rv, errorCode, errors);
        }
        public static Result Warn(int rv, object errorCode, params string[] errors)
        {
            return new Result(ResultSeverity.Warn, rv, errorCode, errors);
        }
        public static Result Fatal(int rv, object errorCode, params string[] errors)
        {
            return new Result(ResultSeverity.Fatal, rv, errorCode, errors);
        }

        public static Result Unauthorized(int rv, params string[] errors)
        {
            return new Result(ResultSeverity.Error, rv, "Unauthorized", errors);
        }
        public static Result Forbidden(int rv, params string[] errors)
        {
            return new Result(ResultSeverity.Error, rv, "Forbidden", errors);
        }
        public static Result ValidationError(int rv, params string[] errors)
        {
            return new Result(ResultSeverity.Error, rv, EasMe.Result.ErrorCode.ValidationError, errors);
        }
    }
}
