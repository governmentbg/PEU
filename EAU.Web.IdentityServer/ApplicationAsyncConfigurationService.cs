using EAU.Common.Cache;
using EAU.Nomenclatures;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer
{
    public class ApplicationAsyncConfigurationService : IHostedService
    {
        private readonly ILanguages _languages;
        private readonly ILabelLocalizations _labels;
        //private readonly IStaticPages _staticPages;
        private readonly IAppParameters _appParameters;

        public ApplicationAsyncConfigurationService(ILanguages languages, IAppParameters appParameters, ILabelLocalizations labels/*, IStaticPages staticPages*/)
        {
            _languages = languages;
            _labels = labels;
            //_staticPages = staticPages;
            _appParameters = appParameters;
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
            //yield return _staticPages.EnsureLoadedAsync(cancellationToken).AsTask();
            yield return _appParameters.EnsureLoadedAsync(cancellationToken).AsTask();
        }
    }
}
