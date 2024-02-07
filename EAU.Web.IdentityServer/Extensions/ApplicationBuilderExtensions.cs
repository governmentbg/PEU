using EAU.Nomenclatures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///  CNSYS Локализация
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseIdentityServerRequestLocalization(this IApplicationBuilder builder)
        {
            var languages = builder.ApplicationServices.GetService<ILanguages>();
            var labels = builder.ApplicationServices.GetService<ILabelLocalizations>();

            var supportedCultures = languages.Search().Select(l => new CultureInfo(l.Code)).ToArray();
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(languages.GetDefault()?.Code ?? "bg-BG"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            localizationOptions.RequestCultureProviders.Clear();
            localizationOptions.RequestCultureProviders.Add(new CustomRequestCultureProvider(context =>
            {
                // разчитаме ui_locales да е кодирано директно в Query или в някой от елементите в Query.
                ProviderCultureResult result = null;

                if (context.Request.Query.TryGetValue("ui_locales", out Microsoft.Extensions.Primitives.StringValues uiLocaleVal))
                {
                    var uiLocale = uiLocaleVal.ToString();
                    if (!string.IsNullOrEmpty(uiLocale) && languages.Get(uiLocale) != null)
                    {
                        result = new ProviderCultureResult(uiLocale, uiLocale);
                    }
                }
                else
                {
                    foreach (var queryItem in context.Request.Query)
                    {
                        var subQueryItem = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(WebUtility.UrlDecode(queryItem.Value));
                        if (subQueryItem.TryGetValue("ui_locales", out Microsoft.Extensions.Primitives.StringValues subQLocaleVal))
                        {
                            var subQLocale = subQLocaleVal.ToString();
                            if (!string.IsNullOrEmpty(subQLocale) && languages.Get(subQLocale) != null)
                            {
                                result = new ProviderCultureResult(subQLocale, subQLocale);
                                break;
                            }
                        }
                    }
                }

                return Task.FromResult(result);
            }));
            localizationOptions.RequestCultureProviders.Add(new CustomCookieRequestCultureProvider(languages));
            localizationOptions.RequestCultureProviders.Add(new CustomRequestCultureProvider((context) =>
            {
                return Task.FromResult(new ProviderCultureResult("bg-BG", "bg-BG"));
            }));

            builder.UseRequestLocalization(localizationOptions);

            // добавяме този inline middleware за зареждане на етикетите за езици, различни от default-ния
            builder.Use(async (context, next) =>
            {
                string lang = context.GetLanguage();

                /*зареждаме етикетите за съотвятния език*/
                await labels.EnsureLoadedAsync(lang, CancellationToken.None);
                await next.Invoke();
            });

            builder.Use(async (context, next) =>
            {
                string lang = context.GetLanguage();

                if (string.IsNullOrEmpty(context.Request.Cookies["currentLang"]) || context.Request.Cookies["currentLang"].Substring(0, 2) != lang)
                {
                    context.Response.Cookies.Append("currentLang", lang,
                        new CookieOptions()
                        {
                            Path = "/",
                            Expires = new DateTimeOffset(new DateTime(2033, 12, 1)),
                            Domain = context.RequestServices.GetRequiredService<IConfiguration>().GetEAUSection().GetValue<string>("GL_COMMON_COOKIE_DOMAIN")
                        });
                }
                /*зареждаме етикетите за съотвятния език*/
                await labels.EnsureLoadedAsync(lang, CancellationToken.None);
                await next.Invoke();
            });

            return builder;
        }
    }
}
