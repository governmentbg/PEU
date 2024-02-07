using System;

namespace Microsoft.Extensions.Hosting
{
    public static class HostEnvironmentEnvExtensions
    {
        //
        // Summary:
        //     Checks if the current host environment name is "Development.Local".
        //
        // Parameters:
        //   hostEnvironment:
        //     An instance of Microsoft.Extensions.Hosting.IHostEnvironment.
        //
        // Returns:
        //     True if the environment name is "Development.Local",
        //     otherwise false.
        public static bool IsDevelopmentLocal(this IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostEnvironment));
            }

            return hostEnvironment.IsEnvironment("Development.Local");
        }
    }
}
