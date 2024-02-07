using AutoMapper;
using EAU.Common.Cache;
using EAU.Security;
using EAU.Utilities;
using EAU.Web.Admin.App.Models.Mapping;
using EAU.Web.Api;
using EAU.Web.Models;
using EAU.Web.Mvc;
using EAU.Web.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EAU.Web.Admin.App
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureForwardedHeadersIfEnabled(Configuration);

            services.AddEAUDbContextProviderWithDefaultConnection(Configuration, EAUPrincipal.AnonymousLocalUserID);
            services.AddDbCacheInvalidationDispatcher((options, sp) =>
            {
                options.ConnectionString = Configuration.GetDBCacheDependencyConnectionString().ConnectionString;
            });

            services.AddMemoryCache();

            services.AddHttpContextAccessor();
            services.AddEAUNomenclaturesDBCaching();
            services.AddEAUNomenclatures();            
            services.AddEAUAppParametersDb();
            services.AddEAUFunctionalitiesDb();
            services.AddEAUAudit();
            services.AddEAUWebStringLocalizer();
            services.AddCMS();
            services.AddEmailServices();
            services.AddEAUPaymentsRegDataServices();

            services.AddEAUUsers();
            services.AddLDAPUsers();
            services.AddServiceLimits();

            services.AddEAUDataProtection(Configuration.GetEAUSection().GetValue<string>("GL_ASPNETCORE_DP_KEY_CERT_THUMBPRINT"), HostingEnvironment);

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                options.CacheProfiles.Add("Nomenclatures", new CacheProfile() { Location = ResponseCacheLocation.None, NoStore = true });
                options.CacheProfiles.Add("NoCache", new CacheProfile() { Location = ResponseCacheLocation.None, NoStore = true });

                options.Filters.Add(typeof(ApiExceptionFilterAttribute));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiError), Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ApiError), Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError));
                options.Filters.Add(new ResponseCacheAttribute() { CacheProfileName = "NoCache" });
            })
            .AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.Converters.Add(new SystemTextIsoTimeSpanConverter());
                //TODO_ADD JsonEnumConverter
                //configure.JsonSerializerOptions.Converters.Add(new EPZEUJsonEnumConverterFactory(JsonNamingPolicy.CamelCase));
                configure.JsonSerializerOptions.IgnoreNullValues = true;
                configure.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            })
            //.ConfigureBaseApiControllerFeatureProvider()
            //TODO:FIX
            .ConfigureApplicationPartManager(sa => sa.FeatureProviders.Add(new DefaultApiControllerFeatureProvider("^EAU.Web.Admin.App.Controllers.*",
            "^EAU.Web.Api.Controllers.LocalizationController$", "^EAU.Web.Api.Controllers.FunctionalitiesController$", "^EAU.Web.Api.Controllers.NomenclaturesCommonCacheController$")));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EAUAdminAutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>().ActionContext;
                return new UrlHelper(actionContext);
            });

            services.ConfigureFileUploadProtectionOptions(Configuration);

            services.AddHttpContextEAUUserAccessor();
            services.AddCookieAuthenticationWithOpenIdConnectChallenge(Configuration);

            services.AddSingleton<IClientErrorFactory, DefaultClientErrorFactory>();
            services.AddSingleton<IApplicationErrorResponseFactory, ApplicationErrorResponseFactory>();

            services.AddEAUSwaggerGen(Configuration, HostingEnvironment, "v1");

            /*слагаме го на последно място, защото ако има някаква грешка при инициализиране на услигите прави опит да стартира хоста. обаче не са добавени нужните услуги.*/
            services.AddHostedService<ApplicationAsyncConfigurationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAppParameters appParameters)
        {
            /*Регистрира параметрите на ПЕАУ за инвалидиране при промяна*/
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

            app.UseSpaStaticFiles();

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
                endpoints.MapSwagger();

                endpoints.MapControllerRoute(
                    name: "API Default",
                    pattern: "api/{controller}/{action}/{id}");
            });

            app.UseSpa(skipLanguageRedirect: true);
        }
    }
}