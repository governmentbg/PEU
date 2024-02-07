using System.Collections.Generic;

namespace EAU.Common
{
    public static class EnumerableExtensions
    {
        public static string ConcatItems<T>(this IEnumerable<T> items, string separator = ",")
        {
            if (items == null) return null;

            return string.Join(separator, items);
        }
    }
}
