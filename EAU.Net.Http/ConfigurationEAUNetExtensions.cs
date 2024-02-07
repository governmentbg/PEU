using System;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationEAUNetExtensions
    {
        public const string EAUSectionName = "EAU";

        public static IConfiguration GetEAUSection(this IConfiguration configuration)
        {
            return configuration.GetSection(EAUSectionName);
        }
    }
}
