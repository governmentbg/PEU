using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace EAU.Web.IdentityServer.Security
{
    public interface ICookieManager
    {
        void EnsureIsLoggedCookie();
        void RemoveIsLoggedCookie();
    }

    public class DefaultCookieManager : ICookieManager
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly string CommonCookieDomain;

        public DefaultCookieManager(IHttpContextAccessor httpContextAccessor, IOptions<AuthenticationOptions> options)
        {
            HttpContextAccessor = httpContextAccessor;

            var configuration = httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>().GetEAUSection();

            CommonCookieDomain = configuration.GetValue<string>("GL_COMMON_COOKIE_DOMAIN");
        }

        public void EnsureIsLoggedCookie()
        {
            if (!HttpContextAccessor.HttpContext.Request.Cookies.TryGetValue(AuthenticationOptions.EAUIsLoggedCookieName, out string isLoggedValue) ||
                !bool.TryParse(isLoggedValue, out bool isLogged) ||
                !isLogged)
            {
                HttpContextAccessor.HttpContext.Response.Cookies.Append(AuthenticationOptions.EAUIsLoggedCookieName, bool.TrueString, GetCreateCookieOptions());
            }
        }

        public void RemoveIsLoggedCookie()
        {
            if (HttpContextAccessor.HttpContext.Request.Cookies.TryGetValue(AuthenticationOptions.EAUIsLoggedCookieName, out string isLoggedValue))
            {
                var options = GetCreateCookieOptions();
                options.Expires = DateTimeOffset.FromUnixTimeSeconds(0);

                HttpContextAccessor.HttpContext.Response.Cookies.Append(AuthenticationOptions.EAUIsLoggedCookieName, bool.FalseString, options);
            }
        }

        private CookieOptions GetCreateCookieOptions() =>
            new CookieOptions
            {
                Domain = CommonCookieDomain,
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.None
            };
    }
}
