namespace EasMe.Result;

/// <summary>
///     A readonly struct Result to be used in Domain Driven Design mainly.
///     <br />
///     In order to avoid using <see cref="Exception" />'s and the performance downside from it.
/// </summary>
public readonly struct ResultT<T> {
    internal ResultT(T? data, Result result) {
        Data = data;
        Result = result;
    }

    public T? Data { get; init; }
    public Result Result { get; init; }
}