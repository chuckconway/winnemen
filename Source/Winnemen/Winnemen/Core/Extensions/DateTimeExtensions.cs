using System;

namespace Winnemen.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime EndOfTheDay(this DateTime dateTime)
        {
            return dateTime.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime? EndOfTheDay(this DateTime? dateTime)
        {
            return dateTime?.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}
