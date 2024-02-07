using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Models
{
    /// <summary>
    /// Кодове за грешка
    /// </summary>
    public enum EAUErrorCodes
    {
        /// <summary>
        /// Възникна грешка на сървъра.
        /// </summary>
        ServerError = 1,

        /// <summary>
        /// Валидационна грешка.
        /// </summary>
        ValidationError = 2,

        /// <summary>
        /// Нямате достъп до този модул
        /// </summary>
        NoAccess = 3,
    }

    /// <summary>
    /// Модел на грешка.
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Код.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Съобщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Описва къде в кода е възникнала грешката.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Вътрешни грешки.
        /// </summary>
        public List<ApiError> InnerErrors { get; set; }
    }
}
