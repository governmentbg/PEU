using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EAU.Web.IdentityServer.Security
{
    public class SecurityHeadersAttribute : ActionFilterAttribute
    {
        public bool AllowSandboxModals { get; set; }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ViewResult)
            {
                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
                {
                    context.HttpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
                if (!context.HttpContext.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    context.HttpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                }

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy

                var sandboxString = AllowSandboxModals ? "sandbox allow-forms allow-same-origin allow-scripts allow-modals allow-popups" : "sandbox allow-forms allow-same-origin allow-scripts allow-popups";
                var csp = $"default-src 'self'; object-src 'none'; frame-ancestors 'none'; {sandboxString}; base-uri 'self';";

                // once for standards compliant browsers
                SetCSPHeaderIfNotPresent(context, "Content-Security-Policy", csp);

                // and once again for IE
                SetCSPHeaderIfNotPresent(context, "X-Content-Security-Policy", csp);

                // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
                var referrer_policy = "no-referrer";
                if (!context.HttpContext.Response.Headers.ContainsKey("Referrer-Policy"))
                {
                    context.HttpContext.Response.Headers.Add("Referrer-Policy", referrer_policy);
                }
            }
        }

        private void SetCSPHeaderIfNotPresent(ResultExecutingContext context, string headerName, string value)
        {
            if (!context.HttpContext.Response.Headers.ContainsKey(headerName))
            {
                context.HttpContext.Response.Headers.Add(headerName, value);
            }
            else
            {
                if (AllowSandboxModals)
                {
                    var headerValue = context.HttpContext.Response.Headers[headerName][0];

                    if (headerValue.Contains("sandbox"))
                    {
                        context.HttpContext.Response.Headers[headerName] = value;
                    }
                }
            }
        }
    }
}
