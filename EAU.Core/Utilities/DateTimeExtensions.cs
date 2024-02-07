namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Използва се, за да се върне датата в определен формат като таг в отговор на заявка от браузър. В зависимост от
        /// тази стойност браузърът решава дали да ползва кеш-а.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatForETag(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        public static DateTime TrimDateTimeToDate(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static DateTime RoundToEndOfDay(this DateTime dateTime)
        {
            return dateTime.TrimDateTimeToDate().AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}

