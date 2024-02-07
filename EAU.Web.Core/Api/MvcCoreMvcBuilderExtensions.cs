
using EAU.Web.Api;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcCoreMvcBuilderExtensions
    {
        /// <summary>
        /// Закача позволените контролери за базово API.
        /// </summary>
        public static IMvcBuilder ConfigureBaseApiControllerFeatureProvider(this IMvcBuilder builder)
        {
            return builder
                .ConfigureApplicationPartManager(sa => sa.FeatureProviders.Add(new DefaultApiControllerFeatureProvider(DefaultApiControllerFeatureProvider.ControllerFullName.BaseFullNamePattern)));
        }
    }
}
