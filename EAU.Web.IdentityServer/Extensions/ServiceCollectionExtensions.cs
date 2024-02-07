using EAU.Web.IdentityServer;
using EAU.Web.IdentityServer.Common;
using EAU.Web.IdentityServer.Extensions;
using EAU.Web.IdentityServer.Security;
using EAU.Web.IdentityServer.Validation;
using IdentityServer4.Extensions;
using IdentityServer4.Validation;
using IdModel = IdentityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography.X509Certificates;
using IdentityServer4.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Конфигуриране на DependencyInjection на Identity сървъра.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureIdentityServer(this IServiceCollection serviceCollection, IWebHostEnvironment environment, IConfiguration configuration)
        {
            var eauSection = configuration.GetEAUSection();

            string connectionStringCfg = eauSection.GetValue<string>("IDSRV_CONFIG_DB_CONNECTION_STRING");
            var identityServerConfiguration = configuration.GetSection("IdentityServer");

            var idsrvBuilder = serviceCollection.AddIdentityServer(options =>
            {
                identityServerConfiguration.Bind(options);

                options.IssuerUri = eauSection.GetValue<string>("GL_IDSRV_URL").ToLower();
            })
            .AddConfigurationStore(options =>
            {
                configuration.GetSection("ConfigurationStoreOptions").Bind(options);
                options.ConfigureDbContext = b => b.UseSqlServer(connectionStringCfg);
            })
            .AddOperationalStore(options => {

                configuration.GetSection("OperationalStoreOptions").Bind(options);
                options.ConfigureDbContext = b => b.UseSqlServer(connectionStringCfg);
            })
            .AddExtensionGrantValidator<DelegationGrantValidator>()
            .AddExtensionGrantValidator<WeakDelegationGrantValidator>();

            serviceCollection.Configure<IdentityServer4.Configuration.AuthenticationOptions>(identityServerConfiguration);
            serviceCollection.Configure<EAU.Web.IdentityServer.AuthenticationOptions>(identityServerConfiguration);
            serviceCollection.Configure<UserManagementOptions>(identityServerConfiguration.GetSection("UserManagement"));
            serviceCollection.Configure<IdentityServerTLSOptions>(eauSection);

            // някои от методите за вход се определят от комбинацията параметър в EP_ + локалната конфигурация на самия идентити сървър;
            //serviceCollection.Configure((Action<EAU.Web.IdentityServer.AuthenticationOptions>)(options =>
            //{
            //    options.EnableKEPAuth = options.EnableKEPAuth && eauSection.GetValue<int>("EP_CERT_AUTH_ENABLED") == 1;
            //}));

            var thumbPrint = eauSection.GetValue<string>("IDSRV_SIGN_CERT_THUMBPRINT").NormalizeThumbprint();

            if (!string.IsNullOrEmpty(thumbPrint))
                idsrvBuilder.AddSigningCredential(thumbPrint, StoreLocation.LocalMachine, NameType.Thumbprint);
            else
                idsrvBuilder.AddDeveloperSigningCredential(true, identityServerConfiguration.GetValue<string>("DeveloperSingingCredentialFileName"));

            serviceCollection.AddSingleton<IRedirectUriValidator, RedirectUriValidator>();

            // clients + resources caching
            idsrvBuilder.AddConfigurationStoreCache();

            return serviceCollection;
        }

        /// <summary>
        /// Конфигуриране на SamlAuthentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSamlAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EAuthIntegrationOptions>(configuration.GetSection("EAuth"));

            services.AddSaml(samlOptions =>
            {
                configuration.GetSection("SAML").Bind(samlOptions);

                var localCfg = samlOptions.Configurations[0].LocalServiceProviderConfiguration;

                var baseName = configuration.GetEAUSection().GetValue<string>("GL_IDSRV_URL").TrimEnd('/');
                var localCertTbprint = configuration.GetEAUSection().GetValue<string>("GL_IDSRV_EAUTH_CERT_THUMBPRINT");
                var egovCertTbprint = configuration.GetEAUSection().GetValue<string>("GL_EGOV_EAUTH_CERT_THUMBPRINT");

                // ДАЕУ очакват като Issuer в заявката за вход, трябва да бъде metadata адреса ни.
                localCfg.Name = baseName + "/saml/metadata";
                localCfg.AssertionConsumerServiceUrl = baseName + EAuthConstants.AssertionConsumerServiceUrl;
                localCfg.SingleLogoutServiceUrl = baseName + EAuthConstants.SingleLogoutServiceUrl;
                localCfg.ArtifactResolutionServiceUrl = baseName + EAuthConstants.ArtifactResolutionServiceUrl;

                var localCerts = new List<ComponentSpace.Saml2.Configuration.Certificate>
                    {
                        new ComponentSpace.Saml2.Configuration.Certificate
                        {
                            Thumbprint = localCertTbprint
                        }
                    };
                localCfg.LocalCertificates = localCerts;

                var extIdpCfg = samlOptions.Configurations[0].PartnerIdentityProviderConfigurations[0];

                var eauthBaseName = configuration.GetEAUSection().GetValue<string>("GL_EAUTH_BASE_URL").TrimEnd('/');
                extIdpCfg.Name = eauthBaseName;
                extIdpCfg.SingleSignOnServiceUrl = eauthBaseName + "/SingleSignOnService";
                extIdpCfg.SingleLogoutServiceUrl = eauthBaseName + "/SingleLogoutService";
                extIdpCfg.ArtifactResolutionServiceUrl = eauthBaseName + "/ArtifactResolutionService";

                if (extIdpCfg.PartnerCertificates == null)
                    extIdpCfg.PartnerCertificates = new ComponentSpace.Saml2.Configuration.Certificate[] {
                            new ComponentSpace.Saml2.Configuration.Certificate
                            {
                                Thumbprint = egovCertTbprint
                            }
                        };
            });

            services.AddSingleton(sp =>
            {
                return new ComponentSpace.Saml2.Events.SamlServiceProviderEvents
                {
                    OnAuthnRequestCreated = (ctx, req) =>
                    {
                        var eauthOptions = ctx.RequestServices.GetRequiredService<Microsoft.Extensions.Options.IOptions<EAuthIntegrationOptions>>();

                        var xmlDocument = new System.Xml.XmlDocument();
                        var ns = "urn:bg:egov:eauth:2.0:saml:ext";

                        var element = xmlDocument.CreateElement("egovbga", "RequestedService", ns);

                        var c1 = xmlDocument.CreateElement("egovbga", "Service", ns);
                        c1.InnerText = eauthOptions.Value.RequestedServiceOID;
                        element.AppendChild(c1);

                        var c2 = xmlDocument.CreateElement("egovbga", "Provider", ns);
                        c2.InnerText = eauthOptions.Value.RequestedProviderOID;
                        element.AppendChild(c2);

                        var c3 = xmlDocument.CreateElement("egovbga", "LevelOfAssurance", ns);
                        c3.InnerText = eauthOptions.Value.RequestedLevelOfAssurance;
                        element.AppendChild(c3);

                        xmlDocument.AppendChild(element);

                        req.Extensions = new ComponentSpace.Saml2.Protocols.Extensions
                        {
                            Items = new System.Collections.Generic.List<System.Xml.XmlElement>
                                {
                                    xmlDocument.DocumentElement
                                }
                        };

                        req.Issuer.SpProvidedID = eauthOptions.Value.RequestedProviderOID;

                        return req;
                    }
                };
            });

            return services;
        }             

        public static IServiceCollection AddEAUClaimsHelper(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IEAUClaimsHelper, EAUClaimsHelper>();
            return serviceCollection;
        }
    }
}
