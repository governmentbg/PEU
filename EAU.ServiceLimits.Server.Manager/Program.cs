using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using System;
using System.IO;

namespace EAU.ServiceLimits.Server.Manager
{
    public partial class Program
    {
        static partial void Run(string[] args);

        public static void Main(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            var environmentName = GetEnvironmentName(args);

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configurationBuilder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

            configurationBuilder.AddEnvironmentVariables(prefix: "DOTNET_");
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

        private static string GetEnvironmentName(string[] args)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            configurationBuilder.AddEnvironmentVariables(prefix: "DOTNET_");
            if (args != null)
            {
                configurationBuilder.AddCommandLine(args);
            }

            var configuration = configurationBuilder.Build();

            var ret = configuration[HostDefaults.EnvironmentKey] ?? Environments.Production;

            (configuration as IDisposable)?.Dispose();

            return ret;
        }
    }
}
