using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Utilities
{
    /// <summary>
    /// Клас за помощни функции свързани с DateTimeOffse
    /// </summary>
    public static class DateTimeOffsetHelper
    {
        /// <summary>
        /// Операция за конвертиране от DateTimeOffset към DateTime.
        /// </summary>
        /// <param name="offset">DateTimeOffset</param>
        /// <returns>DateTime</returns>
        public static DateTime? ConvertOffsetToDateTime(DateTimeOffset? offset)
        {
            if (offset.HasValue)
            {
                return offset.Value.LocalDateTime;
            }
            else
            {
                return null;
            }
        }
    }
}
