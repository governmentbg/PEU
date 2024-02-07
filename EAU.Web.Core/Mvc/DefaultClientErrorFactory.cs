using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace EAU.Web.Mvc
{
    /// <summary>
    /// Имплементация на IClientErrorFactory. Използва се от aspnetcore инфраструктурата при възникване http client errors (400+ status code).
    /// </summary>
    public class DefaultClientErrorFactory : IClientErrorFactory
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;
        private readonly IApplicationErrorResponseFactory _errorResponseFactory;

        public DefaultClientErrorFactory(ProblemDetailsFactory problemDetailsFactory, IApplicationErrorResponseFactory errorResponseFactory)
        {
            _problemDetailsFactory = problemDetailsFactory ?? throw new ArgumentNullException(nameof(problemDetailsFactory));
            _errorResponseFactory = errorResponseFactory ?? throw new ArgumentNullException(nameof(errorResponseFactory));
        }

        public IActionResult GetClientError(ActionContext actionContext, IClientErrorActionResult clientError)
        {
            bool isAjaxCall = actionContext.HttpContext.Request.Headers != null && actionContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxCall)
            {
                var problemDetails = _problemDetailsFactory.CreateProblemDetails(actionContext.HttpContext, clientError.StatusCode);

                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status,
                    ContentTypes =
                    {
                        "application/problem+json",
                        "application/problem+xml",
                    },
                };
            }
            else
            {
                var content = _errorResponseFactory.CreateStaticResponseForErrorAsync(actionContext.HttpContext, clientError.StatusCode)
                    .GetAwaiter().GetResult();

                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = content
                };
            }
        }
    }
}

