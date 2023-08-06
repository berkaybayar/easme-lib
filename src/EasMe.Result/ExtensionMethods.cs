namespace EasMe.Result;

public static class ExtensionMethods
{

   /// <summary>
   /// Combine error codes
   /// </summary>
   /// <param name="result"></param>
   /// <param name="errorCode"></param>
   /// <param name="level"></param>
   /// <returns></returns>
    public static Result CombineErrorCodes(
        this IEnumerable<Result> result,
        string errorCode,
        ResultLevel level = ResultLevel.Warn
    ) {
        var list = result.ToList();
        var isAllSuccess = list.All(x => x.IsSuccess);
        var errorArray = list.Where(x => x.IsFailure).Select(x => x.ErrorCode).ToArray();
        return Result.MultipleErrors(errorArray, errorCode, level);
    }
}