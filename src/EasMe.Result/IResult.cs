using System.Runtime.CompilerServices;

namespace EasMe.Result
{

    public interface IResult
    {
        /// <summary>
        /// Indicates the exact location of the result coming from.
        /// <br/>
        /// <br/>
        /// It is to compensate <see cref="Exception.StackTrace"/>. Every <see cref="IResult"/> returned in a single method each return must have different <see cref="Rv"/>.
        /// <br/>
        /// <br/>
        /// In cases where using <see cref="IResult"/> returning methods that returns <see cref="IResult"/> from another method,
        /// it is recommended to multiply the incoming <see cref="Rv"/> value before returning exact result.
        /// Use <see cref="Result.MultiplyRv"/> or <see cref="ResultData{T}.MultiplyRv"/> method
        /// <br/>
        /// <br/>
        /// It is recommended to multiply the <see cref="Rv"/> from another method by 100 or multiples of 100
        /// </summary>
        int Rv { get; init; } 

        /// <summary>
        /// Indicates success status of <see cref="IResult"/>. 
        /// </summary>
        bool IsSuccess => Rv == 0;

        /// <summary>
        /// Indicates fail status of <see cref="IResult"/>. 
        /// </summary>
        bool IsFailure => !IsSuccess;

        string ErrorCode { get; init; }

        string[] Errors { get; init; }

        ResultSeverity Severity { get; init; }
        
    }
}
