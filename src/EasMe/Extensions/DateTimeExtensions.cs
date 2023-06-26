namespace EasMe.Extensions;

public static class DateTimeExtensions
{
    public static bool IsBetween(this DateTime date, DateTime start, DateTime end) {
        return date >= start && date <= end;
    }

    public static bool IsPassed(this DateTime date) {
        return date <= DateTime.Now;
    }

    public static bool IsInFuture(this DateTime date) {
        return date >= DateTime.Now;
    }

    public static bool IsValidDateTime(this string date) {
        return DateTime.TryParse(date, out var _);
    }

    /// <summary>
    ///     Converts a nullable datetime to datetime. If null, returns DateTime.UnixEpoch
    /// </summary>
    /// <param name="datetime"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this DateTime? datetime) {
        if (datetime is null) return DateTime.UnixEpoch;
        return (DateTime)datetime;
    }

    public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp) {
        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    public static DateTime TicksToDateTime(long ticks) {
        return new DateTime(ticks);
    }

    public static long ToUnixTime(this DateTime date) {
        return ((DateTimeOffset)date).ToUnixTimeSeconds();
    }

    public static bool IsDayOlder(this DateTime date, int day) {
        return date.AddDays(day) < DateTime.Now;
    }

    public static bool IsMinutesOlder(this DateTime date, int mins) {
        return date.AddMinutes(mins) < DateTime.Now;
    }

    public static bool IsSecondOlder(this DateTime date, int seconds) {
        return date.AddSeconds(seconds) < DateTime.Now;
    }

    public static bool IsHoursOlder(this DateTime date, int hours) {
        return date.AddHours(hours) < DateTime.Now;
    }

    public static bool IsMonthOlder(this DateTime date, int months) {
        return date.AddMonths(months) < DateTime.Now;
    }

    public static bool IsYearOlder(this DateTime date, int years) {
        return date.AddYears(years) < DateTime.Now;
    }

    public static string ToReadableDateString(this DateTime dateTime) {
        var isPassed = dateTime.IsPassed();
        var timeSpan = isPassed ? DateTime.Now - dateTime : dateTime - DateTime.Now;
        var text = isPassed ? "ago" : "from now";
        if (timeSpan.TotalSeconds < 60) {
            var seconds = (int)timeSpan.TotalSeconds;
            return FormatTimeSpan(seconds, "second", text);
        }

        if (timeSpan.TotalMinutes < 60) {
            var minutes = (int)timeSpan.TotalMinutes;
            return FormatTimeSpan(minutes, "minute", text);
        }

        if (timeSpan.TotalHours < 24) {
            var hours = (int)timeSpan.TotalHours;
            var leftMinutes = (int)timeSpan.TotalMinutes % 60;
            return FormatTimeSpan(hours, "hour", leftMinutes, "minute", text);
        }

        if (timeSpan.TotalDays < 30) {
            var days = (int)timeSpan.TotalDays;
            var leftHours = (int)timeSpan.TotalHours % 24;
            return FormatTimeSpan(days, "day", leftHours, "hour", text);
        }

        if (timeSpan.TotalDays < 365) {
            var months = (int)(timeSpan.TotalDays / 30);
            var leftDays = (int)timeSpan.TotalDays % 30;
            return FormatTimeSpan(months, "month", leftDays, "day", text);
        }

        var years = (int)(timeSpan.TotalDays / 365);
        var leftMonths = (int)(timeSpan.TotalDays / 30) % 12;
        return FormatTimeSpan(years, "year", leftMonths, "month", text);
    }

    private static string FormatTimeSpan(int value1, string unit1, int value2, string unit2, string text) {
        var result = $"{value1} {unit1}";
        if (value1 != 1)
            result += "s";
        if (value2 > 0) {
            result += $" and {value2} {unit2}";
            if (value2 != 1)
                result += "s";
        }

        return $"{result} {text}";
    }

    private static string FormatTimeSpan(int value, string unit, string text) {
        var result = $"{value} {unit}";
        if (value != 1)
            result += "s";

        return $"{result} {text}";
    }
}