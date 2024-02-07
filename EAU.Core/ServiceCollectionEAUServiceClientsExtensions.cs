using EAU.Common;
using EAU.DocumentTemplates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionEAUServiceClientsExtensions
    {
        /// <summary>
        /// Регистрира и конфигурира в Dependency контейнера обекти от EAU.Core
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddEAUPrivateApiHttpClient(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient(EAUHttpClientNames.EAUPrivateApi)
                .ConfigureHttpClient((sp, client) =>
                {
                    string baseUrl = sp.GetRequiredService<IConfiguration>().GetSection("EAU").GetValue<string>("GL_EAU_PRIVATE_API");
                    client.BaseAddress = new Uri(baseUrl);
                })
                .AddEAUAuthenticationHandler();

            serviceCollection.AddHttpClient<IDocumentTemplatesServiceClient, DocumentTemplatesServiceClient>(EAUHttpClientNames.EAUPrivateApi);

            return serviceCollection;
        }

        public static IServiceCollection AddScopedWithHttpClient<TClient, TImplementation>(this IServiceCollection services, string name)
            where TClient : class
            where TImplementation : class, TClient
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            services.AddScoped<TClient>(s =>
            {
                var httpClientFactory = s.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(name);

                var typedClientFactory = s.GetRequiredService<ITypedHttpClientFactory<TImplementation>>();
                return typedClientFactory.CreateClient(httpClient);
            });

            return services;
        }
    }
}
