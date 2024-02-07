using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Mvc.Filters
{
    /// <summary>
    /// Филтър атрибут за работа с api грешка.
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly IStringLocalizer _localizer;
        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger, IStringLocalizer localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            ApiErrorHandlingManager.ExecuteRestErrorHandling(context, _logger, _localizer);
        }
    }
}
