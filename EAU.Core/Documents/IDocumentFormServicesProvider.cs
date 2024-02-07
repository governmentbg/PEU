using System;

namespace EAU.Documents
{
    public interface IDocumentFormServiceProvider : IServiceProvider
    {
        //
        // Summary:
        //     Gets the service object of the specified type.
        //
        // Parameters:
        //   serviceType:
        //     An object that specifies the type of service object to get.
        //
        // Returns:
        //     A service object of type serviceType. -or- null if there is no service object
        //     of type serviceType.
        object GetService(string documentTypeURI, Type serviceType);
    }

    public static class DocumentFormServiceProviderExtensions
    {
        public static T GetRequiredService<T>(this IDocumentFormServiceProvider servicesProvider, string documentTypeURI)
        {
            return (T)servicesProvider.GetService(documentTypeURI, typeof(T));
        }
        public static IDocumentFormInitializationService GetDocumentFormInitializationService(this IDocumentFormServiceProvider serviceProvider, string documentTypeURI)
        {
            return serviceProvider.GetRequiredService<IDocumentFormInitializationService>(documentTypeURI);
        }

        public static IDocumentFormService GetDocumentFormService(this IDocumentFormServiceProvider serviceProvider, string documentTypeURI)
        {
            return serviceProvider.GetRequiredService<IDocumentFormService>(documentTypeURI);
        }

        public static IDocumentFormValidationService GetDocumentFormValidationService(this IDocumentFormServiceProvider serviceProvider, string documentTypeURI)
        {
            return serviceProvider.GetRequiredService<IDocumentFormValidationService>(documentTypeURI);
        }

        public static IDocumentFormPrintService GetDocumentFormPrintService(this IDocumentFormServiceProvider serviceProvider, string documentTypeURI)
        {
            return serviceProvider.GetRequiredService<IDocumentFormPrintService>(documentTypeURI);
        }
    }
}

