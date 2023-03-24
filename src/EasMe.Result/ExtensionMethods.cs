namespace EasMe.Result;

public static class ExtensionMethods
{
    /// <summary>
    ///     Merges multiple Results into one.
    ///     Result will be Success if all results are Success
    ///     This will not include Error Array inside results only ErrorCodes will be included in ErrorArray
    /// </summary>
    /// <param name="result"></param>
    /// <param name="errorCode"></param>
    /// <param name="severity"></param>
    /// <returns></returns>
    [Obsolete]
    public static Result Combine(
        this IEnumerable<Result> result,
        string errorCode
    )
    {
        return result.CombineAll(errorCode);
    }
    public static Result CombineErrorArrays(
        this IEnumerable<Result> result, 
        string errorCode,
        ResultSeverity severity = ResultSeverity.Warn
        )
    {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        //var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode).ToArray();
        var errorArrayOfErrors = list.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToList();
        return Result.Create(isAllSuccess, severity, errorCode, errorArrayOfErrors);
    }
    public static Result CombineAll(
        this IEnumerable<Result> result,
        string errorCode,
        ResultSeverity severity = ResultSeverity.Warn
    )
    {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode).ToList();
        var errorArrayOfErrors = list.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToList();
        errorArray = errorArray.Concat(errorArrayOfErrors).ToList();
        return Result.Create(isAllSuccess, severity, errorCode, errorArray);
    }
    public static Result CombineErrorCodes(
        this IEnumerable<Result> result,
        string errorCode,
        ResultSeverity severity = ResultSeverity.Warn
    )
    {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode).ToList();
        //var errorArrayOfErrors = list.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToArray();
        //errorArray = errorArray.Concat(errorArrayOfErrors).ToArray();
        return Result.Create(isAllSuccess, severity, errorCode, errorArray);
    }

}