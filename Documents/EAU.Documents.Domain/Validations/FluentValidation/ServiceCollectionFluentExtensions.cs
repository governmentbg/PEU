using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EAU.Documents.Domain.Validations.FluentValidation
{
    public static class ServiceCollectionFluentExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidatorFactory, ServiceProviderValidatorFactory>();

            return services;
        }
    }
}
