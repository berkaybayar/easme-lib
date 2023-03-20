namespace EasMe.Result;

public interface IResultData<T> : IResult
{
    T? Data { get; init; }
}