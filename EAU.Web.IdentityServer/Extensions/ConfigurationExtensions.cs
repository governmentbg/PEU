using CNSys.Data;
namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static ConnectionStringSettings GetSamlCacheConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("SamlCache", "ConnectionStrings");
        }
    }
}