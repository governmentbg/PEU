using AutoMapper;
using EAU.BDS.Documents.Models.Mapping;
using EAU.COD.Documents.Models.Mapping;
using EAU.Common;
using EAU.Common.Cache;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Models.Mapping;
using EAU.KAT.Documents.Models.Mapping;
using EAU.KOS.Documents.Models.Mapping;
using EAU.Migr.Documents.Models.Mapping;
using EAU.Security;
using EAU.Utilities;
using EAU.Web.Api;
using EAU.Web.Api.Models.DocumentProcesses.Mapping;
using EAU.Web.Models;
using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Rebus.ServiceProvider;
using System;
using System.Text.Json;

namespace EAU.Web.EDocViewer.App
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddEAUDbContextProviderWithDefaultConnection(Configuration, EAUPrincipal.AnonymousLocalUserID);

            services.AddMemoryCache();
            services.AddHttpContextEAUUserAccessor();
            services.AddHttpContextAccessor();
            services.AddEAUNomenclaturesDBCaching();
            services.AddEAUNomenclatures();
            services.AddEAUAppParametersDb();
            //services.AddEAUServicesDb();
            services.AddEAUPrivateApiHttpClient();

            #region Sign Module

            services.AddSignModuleHttpClient();
            services.AddBSecureDsslServiceClient();
            services.AddBTrustRemoteServiceClient();
            services.AddEvrotrustRemoteServiceClient();
            services.AddSigningModuleServices(Configuration);

            #endregion

            services.AddEAUAudit();
            services.AddEmailServices();
            services.AddEAUWebStringLocalizer();
            //services.AddDocumentServiceProvider();
            services.AddDocumentProcessesServices();

            services.ConfigureEAUGlobalOptions(Configuration);

            services.AddNRBLDServicesClient();
            services.AddCHODServicesClient();
            services.AddANDServicesClient();
            services.AddKOSServicesClient();
            services.AddNomenclatureServicesClient();
            services.AddEntityDataServicesClient();

            services.AddHttpClientWithAuthenticationHanlder(WAIS.Integration.EPortal.HttpClientNames.WAISIntegrationEAUApi,
                (sp, client) =>
                {
                    client.BaseAddress = new Uri(Configuration.GetSection("EAU").GetValue<string>("GL_EAU_WAIS_INTEGRATION_API"));
                });
            services.AddHttpClientWithAuthenticationHanlder(WAIS.Integration.EPortal.HttpClientNames.WAISDocumentViewer,
                (sp, client) =>
                {
                    client.BaseAddress = new Uri(Configuration.GetSection("EAU").GetValue<string>("GL_EAU_WAIS_INTEGRATION_API"));
                });
            services.AddHttpClientWithAuthenticationHanlder(WAIS.Integration.EPortal.HttpClientNames.WAISNomenclaturesApi,
                (sp, client) =>
                {
                    client.BaseAddress = new Uri(Configuration.GetSection("EAU").GetValue<string>("GL_EAU_WAIS_INTEGRATION_API"));
                });
            services.AddWAISIntegrationClients();
            services.AddWAISNomenclaturesClient();

            services.AddNotaryServicesClient();
            services
                .AddHttpClient("wais.integration.notary.api")
                .ConfigureHttpClient((sp, client) =>
                {
                    client.BaseAddress = new Uri(sp.GetRequiredService<IOptionsMonitor<GlobalOptions>>().CurrentValue.GL_WAIS_INTEGRATION_NOTARY_API);
                });

            services.AddEAUDataProtection(Configuration.GetEAUSection().GetValue<string>("GL_ASPNETCORE_DP_KEY_CERT_THUMBPRINT"), HostingEnvironment);

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DocumentProcessesAutoMapperProfile());
                mc.AddProfile(new EAUDocumentsAutoMapperProfile());
                mc.AddProfile(new EAUKATDocumentsAutoMapperProfile());
                mc.AddProfile(new EAUBDSDocumentsAutoMapperProfile());
                mc.AddProfile(new EAUMigrDocumentsAutoMapperProfile());
                mc.AddProfile(new EAUCODDocumentsAutoMapperProfile());
                mc.AddProfile(new EAUKOSDocumentsAutoMapperProfile());
                mc.AddProfile(new EAUPBZNDocumentsAutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddCommonDocumentServices();
            services.AddKATDocumentServices();
            services.AddBDSDocumentServices();
            services.AddMigrDocumentServices();
            services.AddKOSDocumentServices();
            services.AddCODDocumentServices();
            services.AddPBZNDocumentServices();

            #region Documents Domain Validation

            services.AddValidation();

            #endregion

            services.AddDbCacheInvalidationDispatcher((options, sp) =>
            {
                options.ConnectionString = Configuration.GetDBCacheDependencyConnectionString().ConnectionString;
            });


            #region Rebus

            services.AddEAUPortalQueue(Configuration, HostingEnvironment);

            #endregion 

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Nomenclatures", new CacheProfile() { Duration = 60 });
                options.CacheProfiles.Add("NoCache", new CacheProfile() { Location = ResponseCacheLocation.None, NoStore = true });

                options.Filters.Add(typeof(ApiExceptionFilterAttribute));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiError), Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiError), Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError));
                options.Filters.Add(new ResponseCacheAttribute() { CacheProfileName = "NoCache" });
            })
           .AddJsonOptions(configure =>
           {
               configure.JsonSerializerOptions.Converters.Add(new SystemTextIsoTimeSpanConverter());
               configure.JsonSerializerOptions.Converters.Add(new EAUJsonEnumConverterFactory(JsonNamingPolicy.CamelCase));
               configure.JsonSerializerOptions.IgnoreNullValues = true;
               configure.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
               configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
           })
           //.ConfigureBaseApiControllerFeatureProvider();
           //TODO:FIX
           .ConfigureApplicationPartManager(sa => sa.FeatureProviders.Add(new DefaultApiControllerFeatureProvider("^EAU.Web.EDocViewer.App.Controllers.*", "^EAU.Web.Api.Controllers.*")));

            services.AddEAUUsers();

            services.AddSingleton<EAU.Net.Http.IIndentityServiceTokenRequestClient, EAU.Net.Http.IndentityServicerTokenRequestClient>();

            services.AddHttpClient(EAU.Net.Http.HttpClientNames.IdentityTokenApi, (sp, client) =>
            {
                var baseUrl = Configuration.GetSection("EAU").GetValue<string>("GL_IDSRV_URL");
                var tokenEndpointUri = new Uri($"{(baseUrl.TrimEnd('/') + "/")}connect/token");

                client.BaseAddress = tokenEndpointUri;

            }).ConfigureEAUHttpClientWithOptions();

            services.ConfigureEAUHttpClientOptions(Configuration);
            services.ConfigureEAUHttpAuthenticationClientsOptions(Configuration);

            services.AddSingleton<IClientErrorFactory, DefaultClientErrorFactory>();
            services.AddSingleton<IApplicationErrorResponseFactory, ApplicationErrorResponseFactory>();

            // add authentication + deny anonymous user authorization
            services.AddCookieAuthenticationWithOpenIdConnectChallenge(Configuration);
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            

            /*слагаме го на последно място, защото ако има някаква грешка при инициализиране на услигите прави опит да стартира хоста. обаче не са добавени нужните услуги.*/
            services.AddHostedService<ApplicationAsyncConfigurationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAppParameters appParameters, IStringLocalizer stringLocalizer)
        {
            ValidatorOptions.Global.LanguageManager = new LanguageManagerBridge(stringLocalizer);
            ValidatorOptions.Global.MessageFormatterFactory = () => new EAUMessageFormatter();

            (Configuration as IConfigurationRoot).RegisterAppParametersSourceInEAUConfiguration(appParameters);

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler(errApp =>
                {
                    errApp.RunTerminalErrorMiddleware();
                });
            }

            app.ApplicationServices.UseRebus();

            app.UseSpaStaticFiles();

            app.UseRouting();

            // EDocViewer would be only in Bulgarian. There will be no UI interface to change language.
            var supportedCultures = new[] { "bg" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseAuthenticationWithEAUPrincipal();

            app.UseSerilogUserIdentityContext();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "API Default",
                    pattern: "api/{controller}/{action}/{id}");
            });

            app.UseSpa(skipLanguageRedirect: true);
        }
    }
}
