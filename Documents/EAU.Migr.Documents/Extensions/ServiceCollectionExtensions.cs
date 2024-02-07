using CNSys.Xml.Schema;
using EAU.Documents.Domain.Validations.XSDSchemas;
using EAU.Migr.Documents;
using EAU.Migr.Documents.Domain;
using EAU.Migr.Documents.Domain.Validations;
using EAU.Migr.Documents.Domain.Validations.XSDSchemas;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigrDocumentServices(this IServiceCollection services)
        {
            services.AddDocumentServiceProvider();

            services.AddMigrDocumentServices<ApplicationForIssuingDocumentForForeignersService>(DocumentTypeUrisMigr.ApplicationForIssuingDocumentForForeigners);

            services.AddValidatorsFromAssemblyContaining<ApplicationForIssuingDocumentForForeignersValidator>(ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection AddMigrDocumentServices<TImpl>(this IServiceCollection services, string docTypeURI) where TImpl : class
        {
            services.AddDocumentServices<TImpl>(docTypeURI);
            services.AddDocumentXsdValidationService<IXmlSchemaValidator, EAUMigrDocumentsXmlSchemaValidators>(docTypeURI);
            services.AddDocumentXsdValidationService<IWeakenedXmlSchemaValidator, EAUMigrDocumentsWeakenedXmlSchemaValidator>(docTypeURI);

            return services;
        }
    }
}