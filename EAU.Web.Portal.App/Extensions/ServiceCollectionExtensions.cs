using EAU.Web.Portal.App.Code.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEAUPortalAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(config =>
            {
                config.AddPolicy(PolicyNames.PersonIdentifierDataPolicyName, policyConfig =>
                {
                    policyConfig.AddRequirements(new PersonInfoAuthorizationRequirement());
                });
            });
            services.AddSingleton<IAuthorizationHandler, PersonInfoAuthorizationHandler>();

            return services;
        }
    }
}
