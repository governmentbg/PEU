using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Mvc
{
    /// <summary>
    /// Клас предоставящ функционалност за справяне с api грешки.
    /// </summary>
    public class ApiErrorHandlingManager
    {
        /// <summary>
        /// Справяне на грешки при изпълняване на rest заявка.
        /// </summary>
        /// <param name="exceptionContext"></param>
        /// <param name="logger"></param>
        /// <param name="labels"></param>
        public static void ExecuteRestErrorHandling(ExceptionContext exceptionContext, ILogger logger, IStringLocalizer localizer)
        {
            var exception = exceptionContext.Exception;

            //log exception in file
            logger.LogException(exception);

            exceptionContext.Result = new ErrorActionResult(exception, localizer);
        }
    }
}
