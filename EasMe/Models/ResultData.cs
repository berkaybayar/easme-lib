using EasMe.Abstract;
using EasMe.Enums;

namespace EasMe.Models
{
    /// <summary>
    /// A readonly struct Result with Data of T type to be used in Domain Driven Design mainly.
    /// <br/>
    /// In order to avoid using <see cref="Exception"/>'s and the performance downside from it.
    /// </summary>
    public readonly struct ResultData<T> : IResultData<T>
    {
        private ResultData(T? data, ResultSeverity severity, ushort rv, object errCode)
        {
            Rv = rv;
            ErrorCode = errCode.ToString() ?? "None";
            Severity = severity;
            Data = data;
        }

    
        [System.Text.Json.Serialization.JsonIgnore] 
        public ushort Rv { get; init; } = ushort.MaxValue;
        public ResultSeverity Severity { get; init; }
        public bool IsSuccess => Rv == 0 && Data is not null;
        public bool IsFailure => !IsSuccess;
        public string ErrorCode { get; init; } = "None";
        public T? Data { get; init; }

        public static ResultData<T> Success(T data)
        {
            return new ResultData<T>(data, ResultSeverity.Info, 0, "Success");
        }
        public static ResultData<T> Success(T data,string operationName)
        {
            return new ResultData<T>(data, ResultSeverity.Info, 0, operationName);
        }
        public static ResultData<T> Error(ushort rv, object errorCode)
        {
            return new ResultData<T>(default, ResultSeverity.Error, rv, errorCode);
        }
        public static ResultData<T> Warn(ushort rv, object errorCode)
        {
            return new ResultData<T>(default, ResultSeverity.Warn, rv, errorCode);
        }
        public static ResultData<T> Fatal(ushort rv, object errorCode)
        {
            return new ResultData<T>(default, ResultSeverity.Fatal, rv, errorCode);
        }

      
        public static implicit operator ResultData<T>(T? value)
        {
            return value is null 
                ? ResultData<T>.Error(100, $"{nameof(T)}.NullReference") 
                : ResultData<T>.Success(value);
        }

        /// <summary>
        /// %100 it is going to be failure
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static implicit operator ResultData<T>(Result result)
        {
            return result.Severity switch
            {
                ResultSeverity.Warn => Warn(result.Rv, result.ErrorCode),
                ResultSeverity.Error => Error(result.Rv, result.ErrorCode),
                ResultSeverity.Fatal => Fatal(result.Rv, result.ErrorCode),
                ResultSeverity.Info => 
                    throw new Exception("Implicit conversion from Result to ResultData<T> is not possible if Result state is Success"),
                _ => throw  new ArgumentOutOfRangeException(nameof(Result))

            };
        }

        public Result ToResult()
        {
            return Severity switch
            {
                ResultSeverity.Warn => Result.Warn(Rv, ErrorCode),
                ResultSeverity.Error => Result.Error(Rv, ErrorCode),
                ResultSeverity.Fatal => Result.Fatal(Rv, ErrorCode),
                ResultSeverity.Info => Result.Success(),
                _ => throw new ArgumentOutOfRangeException(nameof(ResultData<T>))
            };
        }

        public Result ToResult(byte multiplyRv)
        {
            return Severity switch
            {
                ResultSeverity.Warn => Result.Warn(Convert.ToUInt16(Rv * multiplyRv), ErrorCode),
                ResultSeverity.Error => Result.Error(Convert.ToUInt16(Rv * multiplyRv), ErrorCode),
                ResultSeverity.Fatal => Result.Fatal(Convert.ToUInt16(Rv * multiplyRv), ErrorCode),
                ResultSeverity.Info => Result.Success(),
                _ => throw new ArgumentOutOfRangeException(nameof(ResultData<T>))
            };
        }
    }
}
