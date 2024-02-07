using EAU.Common.Cache;
using EAU.Nomenclatures;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.EDocViewer.App
{
    public class ApplicationAsyncConfigurationService : IHostedService
    {
        private readonly ILanguages _languages;
        private readonly ILabels _labels;
        private readonly ILabelLocalizations _labelLocalizations;
        private readonly IDeclarations _declarations;
        private readonly IDeliveryChannels _deliveryChannels;
        private readonly IDocumentTypes _documentTypes;
        private readonly IServices _services;
        private readonly IServiceGroups _serviceGroups;
        private readonly IAppParameters _appParameters;
        private readonly IServiceTerms _serviceTerms;

        public ApplicationAsyncConfigurationService(
            ILanguages languages,
            ILabels labels,
            ILabelLocalizations labelLocalizations,
            IDeclarations declarations,
            IDeliveryChannels deliveryChannels,
            IDocumentTypes documentTypes,
            IServices services,
            IServiceGroups serviceGroups,
            IAppParameters appParameters,
            IServiceTerms serviceTerms)
        {
            _languages = languages;
            _labels = labels;
            _labelLocalizations = labelLocalizations;
            _declarations = declarations;
            _deliveryChannels = deliveryChannels;
            _documentTypes = documentTypes;
            _services = services;
            _serviceGroups = serviceGroups;
            _appParameters = appParameters;
            _serviceTerms = serviceTerms;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(CreateConfigurationTasks(cancellationToken));
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private IEnumerable<Task> CreateConfigurationTasks(CancellationToken cancellationToken)
        {
            yield return _languages.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _labels.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _labelLocalizations.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _declarations.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _deliveryChannels.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _documentTypes.EnsureLoadedAsync(cancellationToken).AsTask();            
            yield return _serviceTerms.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _serviceGroups.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _appParameters.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _services.EnsureLoadedAsync(cancellationToken).AsTask();
        }
    }
}
