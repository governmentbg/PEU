using EAU.Common;
using EAU.Net.Http.Authentication;
using EAU.Security;
using EAU.Web.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Utilities.IO.Pem;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpContextEAUUserAccessor(this IServiceCollection services)
        {
            services.TryAddSingleton<EAUHttpContextUserAccessor>();
            services.TryAddSingleton<IEAUUserAccessor>((sp) => { return sp.GetRequiredService<EAUHttpContextUserAccessor>(); });
            services.TryAddSingleton<IClaimsPrincipalAccessor>((sp) => { return sp.GetRequiredService<EAUHttpContextUserAccessor>(); });
            services.TryAddSingleton<IEAUUserImpersonation>((sp) => { return sp.GetRequiredService<EAUHttpContextUserAccessor>(); });

            return services;
        }

        public static IServiceCollection AddEAUPrincipalTransformation(this IServiceCollection services)
        {
            // това е AddSingleton, а не TryAddSingleton, защото има default-на имплементация на IClaimsTransformation и трябва да се замести!
            services.AddSingleton(typeof(IClaimsTransformation), typeof(EAUPrincipalTranformation));

            return services;
        }

        /// <summary>
        /// Добавя автернтификация с cookie и OpenIdConnect
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configureCookieOptions"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCookieAuthenticationWithOpenIdConnectChallenge(this IServiceCollection services, IConfiguration configuration,
            Action<CookieAuthenticationOptions> configureCookieOptions = null)
        {
            // изчистваме inbound claim type map, която е по подразбиране, 
            // защото иначе клеймовете ще се създават с claimtype , който е дефиниран от microsoft 
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var ret = services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.HttpOnly = true;
                    //Конфигурираме продължителността на сесията като към потребителската сесия добавим допълнително време.
                    options.ExpireTimeSpan = TimeSpan.FromMilliseconds(configuration.GetEAUSection().Get<GlobalOptions>().GL_EAU_USR_SESSION_INACTIVITY_INTERVAL.TotalMilliseconds + 20000);
                    options.SlidingExpiration = true;
                    configureCookieOptions?.Invoke(options);
                    configuration.GetEAUSection().GetSection("CookieAuthentication").Bind(options);
                })
                .AddOpenIdConnectChallenge(OpenIdConnectDefaults.AuthenticationScheme, configuration, openIdConnectSetup => {
                    configuration.GetEAUSection().GetSection("OpenIdConnectAuthentication").Bind(openIdConnectSetup);
                });

            services.AddEAUPrincipalTransformation();

            return ret;
        }

        /// <summary>
        /// Конфигурира подадените опции. Делегата се вика за всяко отделно име !
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, Action<string, TOptions> action) where TOptions : class
        {
            services.AddSingleton<IConfigureOptions<TOptions>>((sp) => { return new CommonOptionsConfiguration<TOptions>(action); });
            services.AddSingleton<IConfigureNamedOptions<TOptions>>((sp) => { return new CommonOptionsConfiguration<TOptions>(action); });

            return services;
        }

        /// <summary>
        /// Конфигурира ForwardingMiddleware - а ако е разрешен през конфигурацията от секция ForwardedHeaders
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureForwardedHeadersIfEnabled(this IServiceCollection services, IConfiguration configuration)
        {
            if (string.Equals("true", configuration["ForwardedHeaders_Enabled"], StringComparison.OrdinalIgnoreCase))
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    var forwarderHeadersConfig = configuration.GetSection("ForwardedHeaders");

                    forwarderHeadersConfig.Bind(options);

                    var knownNetworks = forwarderHeadersConfig.GetSection("KnownNetworks").Get<string[]>();

                    if (knownNetworks != null)
                    {
                        options.KnownNetworks.Clear();

                        foreach (var networkCIDR in knownNetworks)
                        {
                            var network = IPNetwork.Parse(networkCIDR);

                            options.KnownNetworks.Add(new AspNetCore.HttpOverrides.IPNetwork(network.Network, network.Cidr));
                        }
                    }

                    var knownProxies = forwarderHeadersConfig.GetSection("KnownProxies").Get<string[]>();

                    if (knownProxies != null)
                    {
                        options.KnownProxies.Clear();

                        foreach (var proxy in knownProxies)
                            options.KnownProxies.Add(IPAddress.Parse(proxy));
                    }
                });
            }

            return services;
        }

        /// <summary>
        /// Add CertificateForwarding if ASPNETCORE_FORWARDEDCERTIFICATE_ENABLED = true
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCertificateForwardingIfEnabled(this IServiceCollection services, IConfiguration configuration)
        {
            if (string.Equals("true", configuration["ForwardedCertificate_Enabled"], StringComparison.OrdinalIgnoreCase))
            {
                services.AddCertificateForwarding(options =>
                {
                    options.CertificateHeader = "X-ClientCert";
                    options.HeaderConverter = (string headerValue) =>
                    {
                        using (var textReader = new StringReader(Uri.UnescapeDataString(headerValue)))
                        {
                            PemReader pem = new PemReader(textReader);
                            var pemObject = pem.ReadPemObject();
                            return new X509Certificate2(pemObject.Content);
                        }
                    };
                });
            }

            return services;
        }
    }
}
