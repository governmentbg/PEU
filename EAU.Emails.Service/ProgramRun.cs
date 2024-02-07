using EAU.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace EAU.Emails.Service
{
    public partial class Program
    {
        static partial void Run(string[] args)
        {
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", typeof(Microsoft.Data.SqlClient.SqlClientFactory));

            EAUStartupBootstrapper.Run();

            Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider((context, options) =>
                {
                    if (context.HostingEnvironment.IsDevelopmentLocal() 
                        || context.HostingEnvironment.IsDevelopment())
                    {
                        options.ValidateScopes = true;
                        options.ValidateOnBuild = true;
                    }
                })
                .ConfigureHostConfiguration(configBuilder =>
                {
                    var environmentName = GetEnvironmentName(args);

                    configBuilder.SetBasePath(Environment.CurrentDirectory);
                    configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    configBuilder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
                    configBuilder.AddEnvironmentVariables("DOTNET_");

                    configBuilder.Build();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddEAUAppParametersDb();
                    services.ConfigureEAUEmailOptions(context.Configuration);
                    services.AddEAUDbContextProviderWithDefaultConnection(context.Configuration, EAUPrincipal.AnonymousLocalUserID);
                    services.AddDbCacheInvalidationDispatcher((options, sp) =>
                    {
                        options.ConnectionString = context.Configuration.GetDBCacheDependencyConnectionString().ConnectionString;
                    });

                    services.AddEmailServices();
                    services.AddEmailProcessingServices();
                    services.AddHostedService<AppParametersConfigurationService>();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddEAUConfigurationFromDb();
                })
                .UseSerilog()
                .Build().Run();
        }
    }
}
