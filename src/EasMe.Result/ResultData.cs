namespace EasMe.Result
{
    /// <summary>
    /// A readonly struct Result with Data of T type to be used in Domain Driven Design mainly.
    /// <br/>
    /// In order to avoid using <see cref="Exception"/>'s and the performance downside from it.
    /// </summary>
    public readonly struct ResultData<T> : IResultData<T>
    {
        public ResultData(T? data, ResultSeverity severity, int rv, object errCode,params string[] errors)
        {
            Rv = rv;
            ErrorCode = errCode.ToString() ?? "None";
            Severity = severity;
            Data = data;
            Errors = errors;
            if (IsSuccess)
            {
                Severity = ResultSeverity.Info;
            }
        }

    
        public int Rv { get; init; } = ushort.MaxValue;
        public ResultSeverity Severity { get; init; }
        

        public bool IsSuccess => Rv == 0 && Data is not null;
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; init; } = "None";
        public string[] Errors { get; init; }
        public T? Data { get; init; }

        public static ResultData<T> Success(T data)
        {
            return new ResultData<T>(data, ResultSeverity.Info, 0, "Success");
        }
        public static ResultData<T> Success(T data,string operationName)
        {
            return new ResultData<T>(data, ResultSeverity.Info, 0, operationName);
        }
        public static ResultData<T> Error(ushort rv, object errorCode,params string[] errors)
        {
            return new ResultData<T>(default, ResultSeverity.Error, rv, errorCode, errors);
        }
        public static ResultData<T> Warn(ushort rv, object errorCode, params string[] errors)
        {
            return new ResultData<T>(default, ResultSeverity.Warn, rv, errorCode, errors);
        }
        public static ResultData<T> Fatal(ushort rv, object errorCode, params string[] errors)
        {
            return new ResultData<T>(default, ResultSeverity.Fatal, rv, errorCode, errors);
        }



        public static implicit operator T?(ResultData<T> res)
        {
            return res.Data;
        }

        public static implicit operator ResultData<T>(T? value)
        {
            return value is null 
                ? ResultData<T>.Error(100, $"{nameof(T)}.NullReference") 
                : ResultData<T>.Success(value);
        }

        /// <summary>
        /// Implicit operator for <see cref="Result"/> to <see cref="ResultData{T}"/>
        /// <br/>
        /// If <see cref="Result"/> status is success conversion t
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static implicit operator ResultData<T>(Result result) 
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException(
                    "Implicit conversion from Result to ResultData<T> is not possible if ResultState is success");
            }
            return new ResultData<T>((T?)default,result.Severity,result.Rv,result.ErrorCode);
        }

        public ResultData<T> WithoutRv()
        {
            ushort rv = 0;
            if (IsFailure) rv = ushort.MaxValue;
            return new ResultData<T>(Data,Severity, rv, ErrorCode);
        }
        public Result ToResult()
        {
            return new Result(Severity, Rv, ErrorCode);
        }
        public Result ToResult(ushort value)
        {
            return new Result(Severity, Convert.ToInt32(Rv * value), ErrorCode);
        }

        public ResultData<T> MultiplyRv(ushort value)
        {
            return new ResultData<T>(Data,Severity, Convert.ToInt32(Rv * value), ErrorCode);
        }

    }
}
