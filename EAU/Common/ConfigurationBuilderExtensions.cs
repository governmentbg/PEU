using EAU.Common.Models;
using EAU.Common.Repositories;
using EAU.Security;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddEAUConfigurationFromDb(this IConfigurationBuilder builder)
        {
            var configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddEAUDbContextProviderWithDefaultConnection(configuration, EAUPrincipal.AnonymousLocalUserID);
            services.AddEAUAppParametersDb();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var entity = serviceProvider.GetRequiredService<IAppParameterRepository>();

                builder.AddEAUConfiguration(entity.SearchInfoAsync(new AppParameterSearchCriteria(), CancellationToken.None).GetAwaiter().GetResult().Data);
            }
            return builder;
        }
    }
}