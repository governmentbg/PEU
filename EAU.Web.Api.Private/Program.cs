using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using System;
using System.IO;

namespace EAU.Web.Api.Private
{
    public partial class Program
    {
        static partial void Run(string[] args);

        public static void Main(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            var hostParameters = GetHostingParameters(args);

            if (!string.IsNullOrEmpty(hostParameters.ContentRoot))
                configurationBuilder.SetBasePath(hostParameters.ContentRoot);

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddJsonFile($"appsettings.{hostParameters.EnvironmentName}.json", optional: true, reloadOnChange: true);

            configurationBuilder.AddEnvironmentVariables(prefix: "ASPNETCORE_");
            if (args != null)
            {
                configurationBuilder.AddCommandLine(args);
            }

            var configuration = configurationBuilder.Build();

            if (configuration.GetValue<bool>("Serilog:EnableSelfLogToConsole"))
                SelfLog.Enable(TextWriter.Synchronized(Console.Error));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithProperty("ServiceType", System.Reflection.Assembly.GetEntryAssembly().GetName().Name)
            .CreateLogger();

            try
            {
                Run(args);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static (string EnvironmentName, string ContentRoot) GetHostingParameters(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configurationBuilder.AddEnvironmentVariables(prefix: "ASPNETCORE_");
            if (args != null)
            {
                configurationBuilder.AddCommandLine(args);
            }

            var configuration = configurationBuilder.Build();

            (string EnvironmentName, string ContentRoot) ret =
                (configuration[HostDefaults.EnvironmentKey] ?? Environments.Production,
                configuration[WebHostDefaults.ContentRootKey]);

            (configuration as IDisposable)?.Dispose();

            return ret;
        }
    }
}
