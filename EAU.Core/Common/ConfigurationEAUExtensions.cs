using EAU.Common;
using EAU.Common.Cache;
using EAU.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationEAUExtensions
    {      

        public static IOptions<TOptions> GetEAUOptions<TOptions>(this IServiceProvider serviceProvider) where TOptions : class, new()
        {
            return serviceProvider.GetRequiredService<IOptions<TOptions>>();
        }
               
        /// <summary>
        /// Добавяне на конфигурация за ПЕАУ.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddEAUConfiguration(
            this IConfigurationBuilder builder, IEnumerable<AppParameter> appParameters)
        {
            builder.Sources.Insert(0, new EAUConfigurationSource(appParameters));
            return builder;
        }

        public static IConfigurationRoot RegisterAppParametersSourceInEAUConfiguration(this IConfigurationRoot configurationRoot, IAppParameters appParameters)
        {
            var provider = configurationRoot.Providers.Single((item) => item is EAUConfigurationProvider) as EAUConfigurationProvider;

            provider.RegisterAppParametersSource(appParameters);

            return configurationRoot;
        }
    }
}
