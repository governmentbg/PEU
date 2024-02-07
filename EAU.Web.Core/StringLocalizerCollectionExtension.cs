using EAU.Web;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StringLocalizerCollectionExtension
    {
        public static IServiceCollection AddEAUWebStringLocalizer(this IServiceCollection services)
        {
            services.TryAddSingleton<IStringLocalizer, EAUWebStringLocalizer>();

            return services;
        }
    }
}
