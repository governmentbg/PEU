using EAU.Security;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        public const string EAUUserSessionCookieName = "EAUSessionID";

        /// <summary>
        /// Взима или генерира уникален идентификатор на потребителска сесия
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static Guid GetUserSessionID(this HttpContext httpContext)
        {
            string sessionID = null;
            Guid sessionIDGuid;
            object sessionIDObj;

            if (httpContext.Items.TryGetValue(EAUUserSessionCookieName, out sessionIDObj))
                return (Guid)sessionIDObj;

            if (!httpContext.Request.Cookies.TryGetValue(EAUUserSessionCookieName, out sessionID) ||
                !Guid.TryParse(sessionID, out sessionIDGuid))
            {
                sessionIDGuid = Guid.NewGuid();

                var configuration = httpContext.RequestServices.GetRequiredService<IConfiguration>().GetEAUSection();

                httpContext.Response.Cookies.Append(EAUUserSessionCookieName, sessionIDGuid.ToString(),
                    new CookieOptions
                    {
                        HttpOnly = false,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Domain = configuration.GetValue<string>("GL_COMMON_COOKIE_DOMAIN")
                    });
            }

            httpContext.Items.Add(EAUUserSessionCookieName, sessionIDGuid);

            return sessionIDGuid;
        }

        /// <summary>
        /// Връща EPZEUPrincipal
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static EAUPrincipal EAUUser(this HttpContext httpContext)
        {
            return httpContext.User as EAUPrincipal;
        }

        public static string GetLanguage(this HttpContext httpContext)
        {
            return httpContext.Features.Get<IRequestCultureFeature>().RequestCulture.UICulture.Name.Substring(0, 2);
        }
    }
}

