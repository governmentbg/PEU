using EAU.Common.Cache;
using EAU.Security;
using EAU.Users;
using EAU.Web.IdentityServer.Common;
using EAU.Web.IdentityServer.Extensions;
using EAU.Web.IdentityServer.Security;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EAU.Web.IdentityServer
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment HostingEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCertificateForwardingIfEnabled(Configuration);

            services.ConfigureForwardedHeadersIfEnabled(Configuration);

            services.AddEAUDbContextProviderWithDefaultConnection(Configuration, EAUPrincipal.AnonymousLocalUserID);

            services.AddMvc();

            services.Configure<IISOptions>(iis =>
            {
                iis.AutomaticAuthentication = false;
            });

            services.AddEAUDataProtection(Configuration.GetEAUSection().GetValue<string>("GL_ASPNETCORE_DP_KEY_CERT_THUMBPRINT"), HostingEnvironment);

            services
                .ConfigureIdentityServer(HostingEnvironment, Configuration)
                .ConfigureEAUGlobalOptions(Configuration)
                .ConfigureEAUHttpClientOptions(Configuration)
                .ConfigureEAUUsersOptions(Configuration)
                .ConfigureEAUHttpAuthenticationClientsOptions(Configuration);

            services.AddEAUPrincipalTransformation();

            if (Configuration.GetSection("IdentityServer").GetValue<bool>("EnableEAuth"))
            {
                services.ConfigureSamlAuthenticationServices(Configuration);
            }

            var authBuilder = services
                .AddAuthentication(IdentityServer4.IdentityServerConstants.DefaultCookieAuthenticationScheme)
                .AddNRAAuthentication(Configuration);

            if (Configuration.GetSection("IdentityServer").GetValue<bool>("EnableEAuth"))
            {
                authBuilder.AddSamlAutenticationForEAuth(Configuration);

                services.AddDistributedSqlServerCache(options =>
                {
                    //dotnet sql-cache create "Data Source=vm-mvr-eau-db.dev.local;Initial Catalog=EAU_DEV2;Persist Security Info=True;User ID=eau_saml_cache_user;Password=eau_saml_cache_user;" aspnetcore idsrv_saml_cache
                    var connString = Configuration.GetSamlCacheConnectionString().ConnectionString;
                    var distCacheConfig = Configuration.GetSection("DistributedSqlServerCache");
                    options.ConnectionString = connString;
                    options.SchemaName = distCacheConfig.GetValue<string>("SchemaName");
                    options.TableName = distCacheConfig.GetValue<string>("TableName");
                });
            }

            services.AddSingleton<IDataProtectorServiceProvider, DataProtectorServiceProvider>();
            services.AddSingleton<IDataSerializer<AuthenticationProperties>, PropertiesSerializer>();
            services.AddSingleton<ISecureDataFormat<AuthenticationProperties>, SecureDataFormat<AuthenticationProperties>>(sp =>
            {
                return new SecureDataFormat<AuthenticationProperties>(
                    sp.GetRequiredService<IDataSerializer<AuthenticationProperties>>(),
                    sp.GetRequiredService<IDataProtectorServiceProvider>().GetDataProtector());
            });

            services
                .AddEAUUsers()
                .AddEAUUsersLogin()
                .AddEAUAudit()
                .AddEmailServices()
                .AddHttpContextEAUUserAccessor()
                .AddSingleton<IProfileService, EAUProfileService>()
                .AddHttpContextAccessor()
                .AddSingleton<ICookieManager, DefaultCookieManager>()
                .AddLDAPUsers();

            services.AddEAUNomenclatures();
            services.AddEAUNomenclaturesDBCaching();
            services.AddEAUAppParametersDb();
            services.AddServiceLimiterService();

            services.AddEAUWebStringLocalizer();
            services.AddEAUClaimsHelper();

            services.AddDbCacheInvalidationDispatcher((options, sp) =>
            {
                options.ConnectionString = Configuration.GetDBCacheDependencyConnectionString().ConnectionString;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
            });
            /*слагаме го на последно място, защото ако има някаква грешка при инициализиране на услигите прави опит да стартира хоста. обаче не са добавени нужните услуги.*/
            services.AddHostedService<ApplicationAsyncConfigurationService>();
        }

        public void Configure(IApplicationBuilder app, IAppParameters appParameters)
        {
            app.UseCertificateForwardingIfEnabled();

            app.UseExceptionHandler("/home/apperror");

            /*Регистрира параметрите на ПЕАУ за инвалидиране при промяна*/
            (Configuration as IConfigurationRoot).RegisterAppParametersSourceInEAUConfiguration(appParameters);

            app.UseStaticFilesWithCache();

            app.UseIdentityServerRequestLocalization();

            app.UseRouting();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
