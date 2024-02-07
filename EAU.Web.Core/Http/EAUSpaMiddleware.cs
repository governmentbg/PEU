using EAU.Nomenclatures;
using EAU.Web.FileUploadProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EAU.Web.Http
{
    public class EAUSpaMiddleware
    {
        private readonly string _defaultPage = "index.html";
        private readonly RequestDelegate _next;
        private readonly bool _skipLanguageRedirect;
        private readonly IMemoryCache _memoryCache;
        private readonly ILanguages _languages;
        private readonly IOptionsMonitor<FileUploadProtectionOptions> _fileUploadProtectionOptionsAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _eauConfiguration;


        public EAUSpaMiddleware(
            RequestDelegate next, 
            bool skypLanguageRedirect,
            IMemoryCache memoryCache,
            ILanguages languages,
            IOptionsMonitor<FileUploadProtectionOptions> FileUploadProtectionOptionsAccessor,
            IWebHostEnvironment webHostEnvironment, 
            IConfiguration configuration)
        {
            _next = next;
            _skipLanguageRedirect = skypLanguageRedirect;
            _memoryCache = memoryCache;

            _languages = languages;
            _fileUploadProtectionOptionsAccessor = FileUploadProtectionOptionsAccessor;
            _webHostEnvironment = webHostEnvironment;

            _eauConfiguration = configuration.GetEAUSection();
        }

        public Task Invoke(HttpContext context)
        {
            string lang = context.GetLanguage();

            if (!_skipLanguageRedirect)
            {
                RequestLocalizationExtentions.TryExtractLanguageFromUrl(context.Request.Path, _languages, out string extrectedLang);

                if ((extrectedLang == null && lang != "bg")
                    ||
                    (extrectedLang != null && (extrectedLang != lang || extrectedLang == "bg"))
                    )
                {
                    //Трябва да редиректнем към същото url, но съдържащ lang ако е различен от bg.
                    string newUrlBasedOnLang = string.Format("{0}://{1}{2}{3}{4}{5}"
                        , context.Request.Scheme
                        , context.Request.Host.Host
                        , context.Request.Host.Port.HasValue ? string.Format(":{0}", context.Request.Host.Port.Value.ToString()) : ""
                        , string.Format("{0}/", context.Request.PathBase.Value.TrimEnd('/'))
                        , lang == "bg" ? "" : string.Format("{0}/", lang)
                        , lang == "bg" && context.Request.Path.Value.StartsWith("/bg") ? context.Request.Path.Value.Substring(3).TrimStart('/') : context.Request.Path.Value.TrimStart('/'));

                    context.Response.Redirect(newUrlBasedOnLang);
                    return Task.CompletedTask;
                }
            }

            string htmlContent = GetOrCreateHtml(context, lang);

            context.Response.StatusCode = 200;

            context.Response.ContentType = "text/html";

            return context.Response.WriteAsync(htmlContent, Encoding.UTF8);
        }

        private string GetOrCreateHtml(HttpContext context, string lang)
        {
            var htmlContent = _memoryCache.GetOrCreate(string.Format("default_{0}", lang), (entry) =>
            {
                var applicationPath = context.Request.PathBase + "/";

                string ver = _eauConfiguration.GetValue<string>("GL_VERSION");
                int? maxRequestLengthInKB = _eauConfiguration.GetValue<int?>("GL_DOCUMENT_MAX_FILE_SIZE");
                int? pageSize = _eauConfiguration.GetValue<int?>("GL_ITEMS_PER_PAGE");
                double? docSaveInterval = _eauConfiguration.GetValue<TimeSpan?>("GL_APPLICATION_DRAFTS_AUTO_SAVE_INTERVAL")?.TotalMilliseconds;
                double? userInactivityTimeout = _eauConfiguration.GetValue<TimeSpan?>("GL_EAU_USR_SESSION_INACTIVITY_INTERVAL")?.TotalMilliseconds;
                int? allowTestSign = _eauConfiguration.GetValue<int?>("SIGN_ALLOW_TEST_SIGN");
                string[] possibleKATObligationsFishSeries = _eauConfiguration.GetValue<string>("POSSIBLE_KAT_OBLIGATIONS_FISH_SERIES")?.Split(",");
                string idsrvURL = _eauConfiguration.GetValue<string>("GL_IDSRV_URL");

                var applicationConfig = new
                {
                    baseUrlName = applicationPath,
                    clientLanguage = lang,
                    version = ver,
                    docSaveIntervalInMs = docSaveInterval,
                    userInactivityTimeout = userInactivityTimeout,
                    acceptedFileExt = _fileUploadProtectionOptionsAccessor.CurrentValue.AllowedFileExtensions,
                    maxRequestLengthInKB = maxRequestLengthInKB,
                    defaultPageSize = pageSize,
                    allowTestSign = allowTestSign.HasValue && allowTestSign.Value == 1,
                    commonCookieDomain = context.Request.Host.Host,
                    possibleKATObligationsFishSeries = possibleKATObligationsFishSeries,
                    webHelpUrl = _eauConfiguration.GetValue<string>("GL_WEB_HELP_URL"),
                    webHelpConfig = _eauConfiguration.GetValue<string>("GL_WEB_HELP_CONFIG"),
                    idsrvURL = idsrvURL
                };

                string strApplicationConfigJson = EAUJsonSerializer.Serialize(applicationConfig);

                StringBuilder defaultPage = new StringBuilder(GetDefaultPage());

                defaultPage = defaultPage.Replace("__lang__", lang);
                defaultPage = defaultPage.Replace("__BASE_URL__", applicationPath);
                defaultPage = defaultPage.Replace("__APP_CONFIG__", strApplicationConfigJson);

                entry.AddExpirationToken(_eauConfiguration.GetReloadToken());

                return defaultPage.ToString();
            });

            return htmlContent;
        }

        private string GetDefaultPage()
        {
            string defaultPage = null;

            using (StreamReader r = new StreamReader(_webHostEnvironment.WebRootFileProvider.GetFileInfo(_defaultPage).CreateReadStream()))
            {
                defaultPage = r.ReadToEnd();
            }

            return defaultPage;
        }
    }
}
