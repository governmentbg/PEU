using EAU.Web.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EAU.Web
{
    public class StaticFilesOptions
    {
        public Dictionary<string, Dictionary<string, string>> ResponseHeaders { get; set; }
    }

    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Настройва автентикацията да ползва EPZEUCRPrincipal.
        /// </summary>
        public static IApplicationBuilder UseAuthenticationWithEAUPrincipal(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

        public static IApplicationBuilder UseSerilogUserIdentityContext(this IApplicationBuilder app)
        {
            app.UseMiddleware<SerilogUserIdentityContextMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseSpaStaticFiles(this IApplicationBuilder app)
        {
            return app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/index.html", System.StringComparison.OrdinalIgnoreCase),
                appBuilder => appBuilder.UseStaticFilesWithCache());
        }

        public static IApplicationBuilder UseStaticFilesWithCache(this IApplicationBuilder builder)
        {
            var configuration = builder.ApplicationServices.GetRequiredService<IConfiguration>();

            var staticFilesConfig = new StaticFilesOptions();

            configuration.GetSection("StaticFiles")?.Bind(staticFilesConfig);

            var responseHeadersList = staticFilesConfig?.ResponseHeaders?.Where(kvp => { return kvp.Value.Count > 0; }).OrderByDescending(f => f.Key)?.ToList();

            if (responseHeadersList != null && responseHeadersList.Count > 0)
            {
                builder.UseStaticFiles(new StaticFileOptions()
                {
                    OnPrepareResponse = ctx =>
                    {
                        foreach (var item in responseHeadersList)
                        {
                            if (ctx.Context.Request.Path.Value.StartsWith(item.Key, StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (var header in item.Value)
                                    ctx.Context.Response.Headers.Append(header.Key, header.Value);

                                break;
                            }
                        }

                    }
                });
            }
            else
                builder.UseStaticFiles();

            return builder;
        }
        public static IApplicationBuilder UseSpa(this IApplicationBuilder app, bool skipLanguageRedirect = false)
        {
            return app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/api", System.StringComparison.OrdinalIgnoreCase),
                appBuilder => appBuilder.UseMiddleware<EAUSpaMiddleware>(skipLanguageRedirect));
        }

        /// <summary>
        /// Add CertificateForwarding middleware if ASPNETCORE_FORWARDEDCERTIFICATE_ENABLED = true.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCertificateForwardingIfEnabled(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();

            if (string.Equals("true", configuration["ForwardedCertificate_Enabled"], StringComparison.OrdinalIgnoreCase))
            {
                app.UseCertificateForwarding();
            }

            return app;
        }

        /// <summary>
        /// Изпълнява терминален middleware при наличие на неприхваната грешка в приложението.
        /// Показва стилизирана статична html страница.
        /// </summary>
        /// <param name="app">Текущият IApplicationBuilder</param>
        public static void RunTerminalErrorMiddleware(this IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var responseFactory = context.RequestServices.GetRequiredService<IApplicationErrorResponseFactory>();
                
                var errorHtml = await responseFactory.CreateStaticResponseForErrorAsync(context);
                
                await context.Response.WriteAsync(errorHtml);
            });
        }
    }
}
