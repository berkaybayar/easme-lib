using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }
        public static bool IsValidDate(this string date)
        {

            return DateTime.TryParse(date, out DateTime result);
        }
        public static string ConvertToReadable(this DateTime datetime)
        {
            if (datetime < DateTime.Now)
            {
                var timeSpan = DateTime.Now - datetime;
                var seconds = timeSpan.TotalSeconds;
                var minutes = timeSpan.TotalMinutes;
                var hours = timeSpan.TotalHours;
                var days = timeSpan.TotalDays;
                var months = days / 30;
                var years = days / 365;


                if (seconds < 60)
                {
                    return $"{(int)seconds} seconds ago";
                }
                else if (minutes < 60)
                {
                    return $"{(int)minutes} minutes ago";
                }
                else if (hours < 24)
                {
                    if ((int)minutes > 0)
                        return $"{(int)hours} hours and {(int)minutes} minutes ago";

                    return $"{(int)hours} hours ago";
                }
                else if (days < 30)
                {
                    if ((int)hours > 0)
                        return $"{(int)days} days and {(int)hours} hours ago";
                    return $"{(int)days} days ";
                }
                else if (months < 12)
                {
                    return $"{(int)months} months ago";
                }
                else
                {
                    return $"{(int)years} years ago";
                }
            }
            else
            {
                var timeSpan = datetime - DateTime.Now;
                var seconds = timeSpan.TotalSeconds;
                var minutes = timeSpan.TotalMinutes;
                var hours = timeSpan.TotalHours;
                var days = timeSpan.TotalDays;
                var months = days / 30;
                var years = days / 365;
                if (seconds < 60)
                {
                    return $"in {(int)seconds} seconds";
                }
                else if (minutes < 60)
                {
                    return $"in {(int)minutes} minutes";
                }
                else if (hours < 24)
                {
                    if ((int)minutes > 0)
                        return $"in {(int)hours} hours and {(int)minutes} minutes";

                    return $"in {(int)hours} hours";
                }
                else if (days < 30)
                {
                    if ((int)hours > 0)
                        return $"in {(int)days} days and {(int)hours} hours";
                    return $"in {(int)days} days";
                }
                else if (months < 12)
                {
                    return $"in {(int)months} months";
                }
                else
                {
                    return $"in {(int)years} years";
                }
            }


        }
    }
}
