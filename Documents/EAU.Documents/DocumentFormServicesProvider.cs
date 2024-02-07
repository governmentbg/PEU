using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace EAU.Documents
{
    internal class DocumentFormServiceProvider : IDocumentFormServiceProvider
    {
        private readonly DocumentFormServiceProviderOptions _documentServiceProviderOptions;
        private readonly IServiceProvider _serviceProvider;

        public DocumentFormServiceProvider(IOptions<DocumentFormServiceProviderOptions> documentServiceProviderOptions, IServiceProvider serviceProvider)
        {
            _documentServiceProviderOptions = documentServiceProviderOptions.Value;
            _serviceProvider = serviceProvider;
        }

        public object GetService(string documentTypeURI, Type serviceType)
        {
            if (_documentServiceProviderOptions.Services.TryGetValue(new ValueTuple<string, Type>(documentTypeURI, serviceType), out Type Timpl))
            {
                return GetService(Timpl);
            }
            else
                return null;
        }

        public object GetService(Type serviceType)
        {
            return _serviceProvider.GetService(serviceType);
        }
    }

    public class DocumentFormServiceProviderOptions
    {
        public IDictionary<ValueTuple<string, Type>, Type> Services { get; set; } = new Dictionary<ValueTuple<string, Type>, Type>();

        public void RegisterService<TService, TImpl>(string documentTypeURI)
        {
            Services.Add(new ValueTuple<string, Type>(documentTypeURI, typeof(TService)), typeof(TImpl));
        }
    }
}
