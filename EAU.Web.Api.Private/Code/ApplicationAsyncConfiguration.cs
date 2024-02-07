using EAU.Common.Cache;
using EAU.Nomenclatures;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace EAU.Web.Api.Private.Code
{
    public class ApplicationAsyncConfigurationService : IHostedService
    {
        private readonly ILanguages _languages;
        private readonly ILabelLocalizations _labels;
        private readonly IAppParameters _appParameters;
        private readonly IDocumentTypes _documentTypes;
        private readonly IServices _services;
        private readonly IServiceGroups _serviceGroups;
        private readonly IDeclarations _declarations;
        private readonly IServiceTerms _serviceTerms;
        private readonly IDeliveryChannels _deliveryChannels;
        private readonly IEkatte _ekatte;
        private readonly IGrao _grao;

        public ApplicationAsyncConfigurationService(
            ILanguages languages,
            ILabelLocalizations labels,
            IAppParameters appParameters,
            IDocumentTypes documentTypes,
            IServices services,
            IServiceGroups serviceGroups,
            IDeclarations declarations,
            IServiceTerms serviceTerms,
            IDeliveryChannels deliveryChannels,
            IEkatte ekatte,
            IGrao grao)
        {
            _languages = languages;
            _labels = labels;
            _appParameters = appParameters;
            _documentTypes = documentTypes;
            _services = services;
            _serviceGroups = serviceGroups;
            _declarations = declarations;
            _serviceTerms = serviceTerms;
            _deliveryChannels = deliveryChannels;
            _ekatte = ekatte;
            _grao = grao;
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
            yield return _appParameters.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _documentTypes.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _services.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _serviceGroups.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _declarations.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _serviceTerms.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _deliveryChannels.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _ekatte.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _grao.EnsureLoadedAsync(cancellationToken).AsTask();
        }
    }
}
