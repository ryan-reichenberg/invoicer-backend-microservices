using System;

namespace AuthenticationService.Extensions
{
    public static class DateExtensions
    {
        public static long ToTimestamp(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
    }
}