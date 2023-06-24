namespace EasMe.Result;

public static class ExtensionMethods {
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
    ) {
        return result.CombineAll(errorCode);
    }

    /// <summary>
    ///     Combines multiple <see cref="Result" />'s into one with new <see cref="Result.ErrorCode" /> and adds
    ///     <see cref="Result.Errors" /> array from each <see cref="Result" /> in list to combined <see cref="Result" />'s
    ///     <see cref="Result.Errors" /> array however it will not include <see cref="Result.ErrorCode" />'s in
    ///     <see cref="Result.Errors" /> array
    ///     <br /><br />
    ///     <see cref="Result" />'s that has <see cref="Result.IsSuccess" /> status true will not be added to combined
    ///     <see cref="Result.Errors" /> array
    ///     <br /><br />
    ///     <see cref="Result.Exception" />.Message's will be merged into error array
    /// </summary>
    /// <param name="result"></param>
    /// <param name="errorCode"></param>
    /// <param name="severity"></param>
    /// <returns>
    ///     The new combined <see cref="Result" /> with new <see cref="Result.ErrorCode" /> and <see cref="Result.Errors" />
    ///     array without sub <see cref="Result.ErrorCode" />'s
    /// </returns>
    public static Result CombineErrorArrays(
        this IEnumerable<Result> result,
        string errorCode,
        ResultSeverity severity = ResultSeverity.Warn
    ) {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        //var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode).ToArray();
        var errorArrayOfErrors = list.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToList();
        var validationErrors = list.Where(x => x.IsFailure).SelectMany(x => x.ValidationErrors).ToList();
        return Result.Create(isAllSuccess, severity, errorCode, errorArrayOfErrors, validationErrors);
    }

    /// <summary>
    ///     Combines multiple <see cref="Result" />'s into one with new <see cref="Result.ErrorCode" /> and adds
    ///     <see cref="Result.ErrorCode" />'s in list to combined <see cref="Result" />'s <see cref="Result.Errors" /> array
    ///     also adds <see cref="Result.Errors" /> array from each <see cref="Result" /> in list to combined
    ///     <see cref="Result" />'s <see cref="Result.Errors" /> array
    ///     <br /><br />
    ///     <see cref="Result" />'s that has <see cref="Result.IsSuccess" /> status true will not be added to combined
    ///     <see cref="Result.Errors" /> array
    ///     <br /><br />
    ///     <see cref="Result.Exception" />.Message's will be merged into error array
    /// </summary>
    /// <param name="result"></param>
    /// <param name="errorCode"></param>
    /// <param name="severity"></param>
    /// <returns>
    ///     The new combined <see cref="Result" /> with new <see cref="Result.ErrorCode" /> and <see cref="Result.Errors" />
    ///     array
    /// </returns>
    public static Result CombineAll(
        this IEnumerable<Result> result,
        string errorCode,
        ResultSeverity severity = ResultSeverity.Warn
    ) {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode).ToList();
        var errorArrayOfErrors = list.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToList();
        errorArray = errorArray.Concat(errorArrayOfErrors).ToList();
        var validationErrors = list.Where(x => x.IsFailure).SelectMany(x => x.ValidationErrors).ToList();
        return Result.Create(isAllSuccess, severity, errorCode, errorArray , validationErrors);
    }

    /// <summary>
    ///     Combines multiple <see cref="Result" />'s into one with new <see cref="Result.ErrorCode" /> and adds
    ///     <see cref="Result.ErrorCode" />'s in list to combined <see cref="Result" />'s <see cref="Result.Errors" /> array
    ///     <br /><br />
    ///     <see cref="Result" />'s that has <see cref="Result.IsSuccess" /> status true will not be added to combined
    ///     <see cref="Result.Errors" /> array
    ///     <br /><br />
    ///     <see cref="Result.Exception" />.Message's will be merged into error array
    /// </summary>
    /// <param name="result"></param>
    /// <param name="errorCode"></param>
    /// <param name="severity"></param>
    /// <returns>
    ///     The new combined <see cref="Result" /> with new <see cref="Result.ErrorCode" /> and <see cref="Result.Errors" />
    ///     array
    /// </returns>
    public static Result CombineErrorCodes(
        this IEnumerable<Result> result,
        string errorCode,
        ResultSeverity severity = ResultSeverity.Warn
    ) {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode).ToList();
        var exceptions = list.Where(x => x.IsFailure && x.ExceptionInfo != null).Select(x => x.ExceptionInfo!.Message)
            .ToList();
        errorArray = errorArray.Concat(exceptions).ToList();
        var validationErrors = list.Where(x => x.IsFailure).SelectMany(x => x.ValidationErrors).ToList();
        return Result.Create(isAllSuccess, severity, errorCode, errorArray , validationErrors);
        //var errorArrayOfErrors = list.Where(x => x.IsFailure).SelectMany(x => x.Errors).ToArray();
        //errorArray = errorArray.Concat(errorArrayOfErrors).ToArray();
        // return Result.Create(isAllSuccess, severity, errorCode, errorArray , validationErrors);
    }
}