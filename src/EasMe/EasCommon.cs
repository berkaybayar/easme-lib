namespace EasMe;

public static class EasCommon
{
  public static string GetReadableDateTimeCompareToNow(DateTime date,
                                           TimeZoneInfo? nowTimeZoneInfo,
                                           out bool isAgo
  ) {
    nowTimeZoneInfo ??= TimeZoneInfo.Utc;
    var convertedNow = TimeZoneInfo.ConvertTime(DateTime.Now, nowTimeZoneInfo);
    isAgo = date < convertedNow;
    var diff = date - convertedNow;
    var days = diff.Days;
    var hours = diff.Hours;
    var minutes = diff.Minutes;
    var seconds = diff.Seconds;
    var list = new List<string>();
    if (seconds != 0) {
      if (seconds < 0) seconds = -seconds;
      list.Add($"{seconds} seconds");
    }

    if (minutes != 0) {
      if (minutes < 0) minutes = -minutes;
      list.Add($"{minutes} minutes");
    }

    if (hours != 0) {
      if (hours < 0) hours = -hours;
      list.Add($"{hours} hours");
    }

    if (days != 0) {
      if (days < 0) days = -days;
      list.Add($"{days} days");
    }

    if (list.Count == 0) return "Now";
    list.Reverse();
    var str = string.Join(" and ", list);
    return str;
  }
}