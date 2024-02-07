using Dapper;
using EAU.Security;
using EAU.Utilities;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EAUDbConfigurationExtensions
    {
        public static IServiceCollection AddEAUDbContextProviderWithDefaultConnection(this IServiceCollection services, IConfiguration configuration, int? defaultUserID = null)
        {
            services.AddDbContextProviderWithDefaultConnection(configuration, (options, sp) =>
            {
                if (options.ConnectionStrings.Count == 0)
                {
                    var connectionStringSettings = configuration.GetEAUDBConnectionString();

                    connectionStringSettings.Name = "default";

                    options.ConnectionStrings.Add("default", connectionStringSettings);

                    options.DefaultConnectionStringName = "default";
                }

                var userAccessor = sp.GetService<IEAUUserAccessor>();
                options.ConnectionInitializer = async (context, token) =>
                {
                    var userClientID = userAccessor?.User?.ClientID;

                    if (!string.IsNullOrEmpty(userClientID) || defaultUserID.HasValue)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("p_ClientID", !string.IsNullOrEmpty(userClientID) ? int.Parse(userClientID) : defaultUserID.Value);

                        await context.SPExecuteAsync("[dbo].[p_sys_set_current_user]", parameters, token);                        
                    }
                };
            });
            services.AddDbContextOperationExecutor();
            return services;
        }
    }
}
