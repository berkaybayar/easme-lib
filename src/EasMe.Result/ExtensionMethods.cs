namespace EasMe.Result;

public static class ExtensionMethods
{
	/// <summary>
	/// Merges multiple Results into one.
	/// Result will be Success if all results are Success
	/// This will not include Error Array inside results only ErrorCodes will be included in ErrorArray
	/// </summary>
	/// <param name="result"></param>
	/// <param name="errorCode"></param>
	/// <param name="rv"></param>
	/// <returns></returns>
	public static Result ToSingleResult(this IEnumerable<Result> result,object errorCode, ushort rvIfNotSuccess = ushort.MaxValue)
	{
		var list = result.ToList();
		var isAllSuccess = list.All(x => x.IsSuccess);
		var rv = isAllSuccess ? 0 : rvIfNotSuccess;
		return Result.Create(ResultSeverity.Warn, rv, errorCode, list.Select(x => x.ErrorCode).ToArray());
	}
}