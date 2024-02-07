using CNSys;
using CNSys.Xml.Schema;
using EAU.Documents;
using EAU.Documents.Domain;
using EAU.Documents.Domain.Validations.XSDSchemas;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDocumentServiceProvider(this IServiceCollection services)
        {
            services.TryAddSingleton<IDocumentFormServiceProvider, DocumentFormServiceProvider>();

            return services;
        }

        public static void AddDocumentServices<TImpl>(this IServiceCollection services, string documentTypeURI) where TImpl : class
        {
            services.TryAddSingleton<TImpl>();

            services.Configure<DocumentFormServiceProviderOptions>((options) =>
            {
                options.RegisterService<IDocumentFormService, TImpl>(documentTypeURI);
                options.RegisterService<IDocumentFormInitializationService, TImpl>(documentTypeURI);
                options.RegisterService<IDocumentFormValidationService, TImpl>(documentTypeURI);
                options.RegisterService<IDocumentFormPrintService, TImpl>(documentTypeURI);
            });
        }

        public static void AddDocumentService<TService, TImpl>(this IServiceCollection services, string documentTypeURI) where TImpl : class
        {
            services.TryAddSingleton<TImpl>();

            services.Configure<DocumentFormServiceProviderOptions>((options) =>
            {
                options.RegisterService<TService, TImpl>(documentTypeURI);
            });
        }

        public static void AddDocumentXsdValidationService<TService, TImpl>(this IServiceCollection services, string documentTypeURI) where TImpl : SingletonBase<TImpl>
        {
            services.TryAddSingleton((sp) => { return (TImpl)SingletonBase<TImpl>.Current; });

            services.Configure<DocumentFormServiceProviderOptions>((options) =>
            {
                options.RegisterService<TService, TImpl>(documentTypeURI);
            });
        }

        #region Common documents

        public static IServiceCollection AddCommonDocumentServices(this IServiceCollection services)
        {
            services.AddDocumentServiceProvider();

            services.AddCommonDocumentServices<RemovingIrregularitiesInstructionsService>(DocumentTypeUris.RemovingIrregularitiesInstructionsUri);
            services.AddCommonDocumentServices<ReceiptAcknowledgedMessageService>(DocumentTypeUris.ReceiptAcknowledgeMessageUri);
            services.AddCommonDocumentServices<ReceiptNotAcknowledgedMessageService>(DocumentTypeUris.ReceiptNotAcknowledgeMessageUri);
            services.AddCommonDocumentServices<PaymentInstructionsService>(DocumentTypeUris.PaymentInstructions);
            services.AddCommonDocumentServices<ReceiptAcknowledgedPaymentForMOIService>(DocumentTypeUris.ReceiptAcknowledgedPaymentForMOI);
            services.AddCommonDocumentServices<OutstandingConditionsForStartOfServiceMessageService>(DocumentTypeUris.OutstandingConditionsForStartOfServiceMessage);
            services.AddCommonDocumentServices<RefusalService>(DocumentTypeUris.Refusal);           
            services.AddCommonDocumentServices<DocumentsWillBeIssuedMessageService>(DocumentTypeUris.DocumentsWillBeIssuedMessage);
            services.AddCommonDocumentServices<TerminationOfServiceMessageService>(DocumentTypeUris.TerminationOfServiceMessage);
            services.AddCommonDocumentServices<ActionsTakenMessageService>(DocumentTypeUris.ActionsTakenMessage);
            services.AddCommonDocumentServices<ApplicationForWithdrawServiceService>(DocumentTypeUris.ApplicationForWithdrawService);
            services.AddCommonDocumentServices<OutstandingConditionsForWithdrawServiceMessageService>(DocumentTypeUris.OutstandingConditionsForWithdrawServiceMessage);

            services.AddValidatorsFromAssemblyContaining<EAU.Documents.Domain.Validations.ElectronicAdministrativeServiceHeaderValidator>(ServiceLifetime.Singleton);

            return services;
        }

        private static IServiceCollection AddCommonDocumentServices<TImpl>(this IServiceCollection services, string docTypeURI) where TImpl : class
        {
            services.AddDocumentServices<TImpl>(docTypeURI);
            services.AddDocumentXsdValidationService<IXmlSchemaValidator, EAUDocumentsXmlSchemaValidator>(docTypeURI);
            services.AddDocumentXsdValidationService<IWeakenedXmlSchemaValidator, EAUDocumentsWeakenedXmlSchemaValidator>(docTypeURI);

            return services;
        }

        #endregion
    }
}
