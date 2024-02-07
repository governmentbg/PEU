using EAU.Common.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Emails.Service
{
    public class AppParametersConfigurationService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IAppParameters _appParameters;
        private readonly ILogger _logger;

        public AppParametersConfigurationService(IConfiguration configuration, IAppParameters appParameters, ILogger<AppParametersConfigurationService> logger)
        {
            _configuration = configuration;
            _appParameters = appParameters;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            try
            {
                await _appParameters.EnsureLoadedAsync(stoppingToken);

                /*Регистрира параметрите на ПЕАУ за инвалидиране при промяна*/
                (_configuration as IConfigurationRoot).RegisterAppParametersSourceInEAUConfiguration(_appParameters);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
        }
    }
}
