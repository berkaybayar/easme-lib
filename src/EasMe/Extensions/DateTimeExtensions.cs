namespace EasMe.Extensions;

public static class DateTimeExtensions {
    public static bool IsBetween(this DateTime date, DateTime start, DateTime end) {
        return date >= start && date <= end;
    }

    public static bool IsInPass(this DateTime date) {
        return date <= DateTime.Now;
    }

    public static bool IsInFuture(this DateTime date) {
        return date >= DateTime.Now;
    }

    public static bool IsValidDate(this string date) {
        return DateTime.TryParse(date, out var _);
    }

    public static DateTime ToDateTime(this DateTime? datetime) {
        if (datetime is null) return DateTime.UnixEpoch;
        return (DateTime)datetime;
    }

    public static string ToReadableDateString(this DateTime datetime) {
        if (datetime < DateTime.Now) {
            var timeSpan = DateTime.Now - datetime;
            var seconds = timeSpan.TotalSeconds;
            var minutes = timeSpan.TotalMinutes;
            var hours = timeSpan.TotalHours;
            var days = timeSpan.TotalDays;
            var months = days / 30;
            var years = days / 365;


            if (seconds < 60) return $"{(int)seconds} seconds ago";

            if (minutes < 60) return $"{(int)minutes} minutes ago";

            if (hours < 24) {
                var leftMins = (int)minutes % 60;
                if (leftMins > 0)
                    return $"{(int)hours} hours and {leftMins} minutes ago";

                return $"{(int)hours} hours ago";
            }

            if (days < 30) {
                var leftHours = (int)hours % 24;
                if (leftHours > 0)
                    return $"{(int)days} days and {leftHours} hours ago";
                return $"{(int)days} days ";
            }

            if (months < 12)
                return $"{(int)months} months ago";
            return $"{(int)years} years ago";
        }
        else {
            var timeSpan = datetime - DateTime.Now;
            var seconds = timeSpan.TotalSeconds;
            var minutes = timeSpan.TotalMinutes;
            var hours = timeSpan.TotalHours;
            var days = timeSpan.TotalDays;
            var months = days / 30;
            var years = days / 365;
            if (seconds < 60) return $"in {(int)seconds} seconds";

            if (minutes < 60) return $"in {(int)minutes} minutes";

            if (hours < 24) {
                var leftMins = (int)minutes % 60;
                if (leftMins > 0)
                    return $"in {(int)hours} hours and {leftMins} minutes";

                return $"in {(int)hours} hours";
            }

            if (days < 30) {
                var leftHours = (int)hours % 24;
                if (leftHours > 0)
                    return $"in {(int)days} days and {leftHours} hours";
                return $"in {(int)days} days";
            }

            if (months < 12)
                return $"in {(int)months} months";
            return $"in {(int)years} years";
        }
    }

    public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp) {
        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    public static long ToUnixTime(this DateTime date) {
        return ((DateTimeOffset)date).ToUnixTimeSeconds();
        //return (long)(date - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
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
}