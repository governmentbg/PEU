using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EAU.Web
{
    /// <summary>
    /// Интерфейс за взимане на отговори при грешка.
    /// </summary>
    public interface IApplicationErrorResponseFactory
    {
        /// <summary>
        /// Връща статично съдържание спрямо код за грешка.
        /// </summary>
        Task<string> CreateStaticResponseForErrorAsync(HttpContext httpContext, int? statusCode = null);
    }

    public class ApplicationErrorResponseFactory : IApplicationErrorResponseFactory
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IStringLocalizer _localizer;

        public ApplicationErrorResponseFactory(IMemoryCache memoryCache, IStringLocalizer stringLocalizer)
        {
            _memoryCache = memoryCache;
            _localizer = stringLocalizer;
        }

        public Task<string> CreateStaticResponseForErrorAsync(HttpContext httpContext, int? statusCode = null)
        {
            string lang = httpContext.GetLanguage();
            string messageCode = GetMessageCodeForStatusCode(statusCode);

            return _memoryCache.GetOrCreateAsync($"error_page_{messageCode}_{lang}", (entry) =>
            {
                string returnUrl = httpContext.Request.PathBase + "/";
                string message = _localizer[messageCode];
                string page;

                var assembly = typeof(AppBuilderExtensions).GetTypeInfo().Assembly;

                using (var r = new StreamReader(assembly.GetManifestResourceStream("EAU.Web.StaticPages.application_error.html")))
                {
                    page = r.ReadToEnd();
                }

                StringBuilder defaultPage = new StringBuilder(page)
                    .Replace("__GL_BACK_HOME_PAGE_URL__", returnUrl)
                    .Replace("__GL_SYSTEM_UNAVAILABLE_E__", message)
                    .Replace("__GL_BACK_HOME_PAGE_I__", _localizer["GL_BACK_HOME_PAGE_I"])
                    .Replace("__GL_REPUBLIC_OF_BULGARIA_L__", _localizer["GL_REPUBLIC_OF_BULGARIA_L"])
                    .Replace("__GL_MVR_L__", _localizer["GL_MVR_L"])
                    .Replace("__GL_PEAU_L__", _localizer["GL_PEAU_L"])
                    .Replace("__GL_OPERATION_PROGRAM_GOOD_MANAGEMENT_EU_L__", _localizer["GL_OPERATION_PROGRAM_GOOD_MANAGEMENT_EU_L"]);

                return Task.FromResult(defaultPage.ToString());
            });
        }

        private static string GetMessageCodeForStatusCode(int? statusCode)
        {
            if (statusCode == (int?)System.Net.HttpStatusCode.TooManyRequests)
                return "GL_TOO_MANY_REQUESTS_E";

            return "GL_SYSTEM_UNAVAILABLE_E";
        }
    }
}
