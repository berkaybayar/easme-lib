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
    /// <param name="includeErrorArray"></param>
    /// <returns></returns>
    public static Result Combine(this IEnumerable<Result> result, string errorCode,
        bool includeErrorArrayOfErrors = true)
    {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode);
        if (!includeErrorArrayOfErrors)
            return Result.Create(isAllSuccess, ResultSeverity.Warn, errorCode, errorArray.ToArray());
        var errorArrayOfErrors = list.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToArray();
        errorArray = errorArray.Concat(errorArrayOfErrors);
        return Result.Create(isAllSuccess, ResultSeverity.Warn, errorCode, errorArray.ToArray());
    }
}