using Microsoft.Extensions.Configuration;
using System;
using EAU.Signing;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using EAU.Signing.Evrotrust;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionSigningModuleHttpClientExtension
    {
        /// <summary>
        /// Регистрира HttpClient за BSecureDssl.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddSignModuleHttpClient(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddHttpClient(SignModuleConstants.BSecureDsslHttpClientName)
                .ConfigureHttpClient((sp, client) =>
                {
                    client.BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>().GetSection("EAU").GetValue<string>("SIGN_BSECURE_DSSL_API"));
                    client.Timeout = TimeSpan.FromMinutes(5);
                })
                .AddEAUTimeoutHandler(299);

            serviceCollection.AddHttpClient(SignModuleConstants.BTrustRemoteHttpClientName)
                .ConfigurePrimaryHttpMessageHandler((sp) =>
                {
                    var handler = new HttpClientHandler();

                    X509Store store = null;
                    try
                    {
                        store = new X509Store(StoreLocation.LocalMachine);
                        store.Open(OpenFlags.ReadOnly);

                        var certThumbPrint = sp.GetRequiredService<IConfiguration>().GetSection("EAU").GetValue<string>("SIGN_BTRUST_REMOTE_CLIENT_CERT");
                        var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, certThumbPrint, true);

                        if (certCollection.Count != 1)
                            throw new Exception("Unable to locate the correct SSL certificate for Btrust.");

                        handler.ClientCertificates.Add(certCollection[0]);
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        if (store != null)
                            store.Close();
                    }

                    return handler;
                })
                .ConfigureHttpClient((sp, client) =>
                {
                    client.BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>().GetSection("EAU").GetValue<string>("SIGN_BTRUST_REMOTE_CLIENT_API"));
                })
                .AddEAUTimeoutHandler();

            serviceCollection.AddHttpClient(SignModuleConstants.EvrotrustRemoteHttpClientName).ConfigureHttpClient((sp, client) => {
                client.BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>().GetSection("EAU").GetValue<string>("SIGN_EVROTRUST_CLIENT_API"));
            }).AddEAUTimeoutHandler();

            return serviceCollection;
        }

        /// <summary>
        /// Helper за регистриране на EAU.Signing.BSecureDSSL.Client към HttpClient с име {httpClientName}.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="httpClientName"></param>
        /// <returns></returns>
        public static IServiceCollection AddBSecureDsslServiceClient(this IServiceCollection serviceCollection, string httpClientName = SignModuleConstants.BSecureDsslHttpClientName)
        {
            serviceCollection.AddHttpClient<EAU.Signing.BSecureDSSL.IBSecureDsslClient, EAU.Signing.BSecureDSSL.BSecureDsslClient>(httpClientName);

            return serviceCollection;
        }

        public static IServiceCollection AddBTrustRemoteServiceClient(this IServiceCollection serviceCollection, string httpClientName = SignModuleConstants.BTrustRemoteHttpClientName)
        {
            serviceCollection.AddHttpClient<EAU.Signing.BtrustRemoteClient.IBtrustRemoteClient, EAU.Signing.BtrustRemoteClient.BtrustRemoteClient>(httpClientName);

            return serviceCollection;
        }

        public static IServiceCollection AddEvrotrustRemoteServiceClient(this IServiceCollection serviceCollection, string httpClientName = SignModuleConstants.EvrotrustRemoteHttpClientName)
        {
            serviceCollection.AddHttpClient<IEvrotrustClient, EvrotrustClient>(httpClientName);

            return serviceCollection;
        }
    }
}
