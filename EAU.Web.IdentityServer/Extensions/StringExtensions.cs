
using System;
using System.Linq;

namespace EAU.Web.IdentityServer.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeThumbprint(this string thumbprint)
            => thumbprint?.Replace(" ", "").Replace("-", "").Replace("_", "");

        public static string GetTimeTextPresentation(this TimeSpan time)
        {
            string days = null, hours = null, minutes = null, seconds = null;

            if (time.Days > 0) days = $"{time.Days} дни";
            if (time.Hours > 0) hours = $"{time.Hours} часа";
            if (time.Minutes > 0) minutes = $"{time.Minutes} минути";
            if (time.Seconds > 0) seconds = $"{time.Seconds} секунди";

            return string.Join(", ", new string[] { days, hours, minutes, seconds }.Where(s => s != null));
        }

        public static string AddQueryString(this string url, string query)
        {
            if (!url.Contains("?"))
            {
                url += "?";
            }
            else if (!url.EndsWith("&"))
            {
                url += "&";
            }

            return url + query;
        }

        public static string AddQueryString(this string url, string name, string value)
            => url.AddQueryString(name + "=" + System.Text.Encodings.Web.UrlEncoder.Default.Encode(value));
    }
}
