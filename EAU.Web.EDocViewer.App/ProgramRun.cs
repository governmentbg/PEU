﻿using EAU.Payments;
using EAU.Services;
using EAU.Signing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace EAU.Web.EDocViewer.App
{
    public partial class Program
    {
        static partial void Run(string[] args)
        {
            System.Data.Common.DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", typeof(Microsoft.Data.SqlClient.SqlClientFactory));
            //TODO: ADD DB Provider
            //System.Data.Common.DbProviderFactories.RegisterFactory("Npgsql", typeof(Npgsql.NpgsqlFactory));

            EAUStartupBootstrapper.Run();
            EAUPaymentsStartupBootstrapper.Run();
            EAUServicesStartupBootstrapper.Run();
            SigningModuleDapperBootstrapper.Run();
            EAUEDocViewerStartupBootstrapper.Run();
            //EPZEUWebCoreStartupBootstrapper.Run();

            CreateWebHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog()
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
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddEAUConfigurationFromDb();
                });
        }
    }
}
