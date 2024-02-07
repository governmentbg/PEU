using EAU.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace EAU.ServiceLimits.Server.Manager
{
    public partial class Program
    {
        static partial void Run(string[] args)
        {
            EAUStartupBootstrapper.Run();

            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", typeof(Microsoft.Data.SqlClient.SqlClientFactory));

            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureAppConfiguration((builder) =>
            {
                builder.AddEAUConfigurationFromDb();
            }).UseSerilog()
            .UseDefaultServiceProvider((context, options) =>
            {
                if (
                    context.HostingEnvironment.IsDevelopmentLocal() ||
                    context.HostingEnvironment.IsDevelopment())
                {
                    options.ValidateScopes = true;
                    options.ValidateOnBuild = true;
                }
            })
            .ConfigureServices((context, services) =>
            {
                services.AddEAUAppParametersDb();
                services.AddEAUSearchUsers();

                services.AddEAUDbContextProviderWithDefaultConnection(context.Configuration, EAUPrincipal.SystemLocalUserID);

                services.AddServerManager();

                services.AddDbCacheInvalidationDispatcher((options, sp) =>
                {
                    options.ConnectionString = context.Configuration.GetDBCacheDependencyConnectionString().ConnectionString;
                });

                services.AddHostedService<ServiceLimitsManagerService>();
            }).RunConsoleAsync().Wait();
        }
    }
}
