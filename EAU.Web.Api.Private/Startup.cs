using AutoMapper;
using EAU.BDS.Documents.Models.Mapping;
using EAU.COD.Documents.Models.Mapping;
using EAU.Common.Cache;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Models.Mapping;
using EAU.KAT.Documents.Models.Mapping;
using EAU.KOS.Documents.Models.Mapping;
using EAU.Migr.Documents.Models.Mapping;
using EAU.Net.Http;
using EAU.Security;
using EAU.Utilities;
using EAU.Web.Api.Models.DocumentProcesses.Mapping;
using EAU.Web.Api.Private.Code;
using EAU.Web.Models;
using EAU.Web.Mvc.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Rebus.ServiceProvider;
using System;
using System.Text.Json;

namespace EAU.Web.Api.Private
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            //TODO: Да се обмисли след като се направи автентикацията тук кой потребител трябва да бъде
            services.AddEAUDbContextProviderWithDefaultConnection(Configuration, EAUPrincipal.SystemLocalUserID);

            services.AddMemoryCache();
            services.AddHttpContextEAUUserAccessor();
            services.AddHttpContextAccessor();
            services.AddEAUNomenclatures();
            services.AddEAUNomenclaturesDBCaching();
            services.AddEAUAppParametersDb();
            services.AddEAUPrivateApiHttpClient();

            services.AddEmailServices();

            #region Sign Module

            services.AddSignModuleHttpClient();
            services.AddBSecureDsslServiceClient();
            services.AddBTrustRemoteServiceClient();
            services.AddEvrotrustRemoteServiceClient();
            services.AddSigningModuleServices(Configuration);

            #endregion

            services.AddNotaryServicesClient();
            services.AddNRBLDServicesClient();
            services.AddCHODServicesClient();
            services.AddEAUDataProtection(Configuration.GetEAUSection().GetValue<string>("GL_ASPNETCORE_DP_KEY_CERT_THUMBPRINT"), HostingEnvironment);
            services.AddNomenclatureServicesClient();
            services.AddEntityDataServicesClient();
            services.AddKOSServicesClient();

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

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.AddEAUUsers();
            services.AddEAUAudit();
            services.AddEAUWebStringLocalizer();
            services.AddDocumentProcessesServices();

            services.ConfigureEAUGlobalOptions(Configuration);
            services.ConfigureFileUploadProtectionOptions(Configuration);

            services.AddDbCacheInvalidationDispatcher((options, sp) =>
            {
                options.ConnectionString = Configuration.GetDBCacheDependencyConnectionString().ConnectionString;
            });

            //Конфигурира максималния размер на мултипарт заявка (качване на файлове) на 5МB.
            services.Configure<FormOptions>(options =>
            {
                //TODO: Rad From appSettings
                options.MultipartBodyLengthLimit = 100000 * 1024;
            });

            #region Rebus

            services.AddEAUPortalQueue(Configuration, HostingEnvironment);

            #endregion 

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
            services.AddWAISIntegrationClients();

            services.AddControllers(options => {
                options.Filters.Add(typeof(ApiExceptionFilterAttribute));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiError), Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiError), Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError));

                options.InputFormatters.Add(new RawTextContentFormatter());
            })
            .AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.Converters.Add(new SystemTextIsoTimeSpanConverter());
                //configure.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                configure.JsonSerializerOptions.Converters.Add(new EAUJsonEnumConverterFactory(JsonNamingPolicy.CamelCase));
                configure.JsonSerializerOptions.IgnoreNullValues = true;
                configure.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            })
            .ConfigureApplicationPartManager(sa => sa.FeatureProviders.Add(new DefaultApiControllerFeatureProvider("^EAU.Web.Api.Private.Controllers.*")));

            services.AddSingleton<IIndentityServiceTokenRequestClient, IndentityServicerTokenRequestClient>();

            services.AddHttpClient(EAU.Net.Http.HttpClientNames.IdentityTokenApi, (sp, client) =>
            {
                var baseUrl = Configuration.GetSection("EAU").GetValue<string>("GL_IDSRV_URL");
                var tokenEndpointUri = new Uri($"{(baseUrl.TrimEnd('/') + "/")}connect/token");

                client.BaseAddress = tokenEndpointUri;

            }).ConfigureEAUHttpClientWithOptions();

            services.ConfigureEAUHttpClientOptions(Configuration);
            services.ConfigureEAUHttpAuthenticationClientsOptions(Configuration);

            services.AddEAUSwaggerGen(Configuration, HostingEnvironment, "v1");

            #region Authentication

            services.AddAuthentication("token")
                .AddJwtBearer("token", options =>
                {
                    var idsrvUrl = Configuration.GetSection("EAU").GetValue<string>("GL_IDSRV_URL");

                    options.Authority = idsrvUrl;
                    options.Audience = "eau.api.private"; //TODO: да се помисли от къде може да се взима.
                });

            services.AddAuthorization(config => {

                config.AddPolicy("BackOfficeIntegrationApiPolicy", policyConfig => {
                    policyConfig.RequireScope("api.eau.integration.backoffice");
                });
            });

            #endregion

            /*слагаме го на последно място, защото ако има някаква грешка при инициализиране на услигите прави опит да стартира хоста. обаче не са добавени нужните услуги.*/
            services.AddHostedService<ApplicationAsyncConfigurationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAppParameters appParameters, IStringLocalizer stringLocalizer)
        {
            ValidatorOptions.Global.LanguageManager = new LanguageManagerBridge(stringLocalizer);
            ValidatorOptions.Global.MessageFormatterFactory = () => new EAUMessageFormatter();

            /*Регистрира параметрите на ЕАУ за инвалидиране при промяна*/
            (Configuration as IConfigurationRoot).RegisterAppParametersSourceInEAUConfiguration(appParameters);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.UseRebus();

            app.UseHttpsRedirection();

            app.UseRouting();

            // Admin would be only in Bulgarian. There will be no UI interface to change language.
            var supportedCultures = new[] { "bg" };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseAuthenticationWithEAUPrincipal();

            app.UseSerilogUserIdentityContext();

            app.UseEndpoints(endpoints =>
            {
                // Add a new endpoint that uses the SwaggerMiddleware
                endpoints.MapSwagger();

                endpoints.MapControllers();
            });
        }
    }
}
