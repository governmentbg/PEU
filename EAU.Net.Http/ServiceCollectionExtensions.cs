using EAU.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Конфигурира EPZEUHttpClientsOptions секцията от EPZEU
        /// </summary>
        public static IServiceCollection ConfigureEAUHttpClientOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EAUHttpClientsOptions>(config.GetSection("EAU"));

            return services;
        }

        /// <summary>
        /// Конфигурира автентикацията на клиентите в EPZEU.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureEAUHttpAuthenticationClientsOptions(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Configure<HttpAuthenticationClients>(config.GetSection("EAU").GetSection("httpAuthenticationClients"));
        }

        /// <summary>
        /// Регистрира httpclient с име httpClientName, конфигурира го с configureClient и закача Authentication handler в pipeline-a му.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="httpClientName"></param>
        /// <param name="configureClient"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientWithAuthenticationHanlder(
            this IServiceCollection services, string httpClientName, Action<IServiceProvider, HttpClient> configureClient)
        {
            services
               .AddHttpClient(httpClientName)
               .ConfigureHttpClient(configureClient)
               .AddEAUAuthenticationHandler();

            return services;
        }
    }
}
