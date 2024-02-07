using EAU.Common.Cache;
using EAU.ServiceLimits.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace EAU.ServiceLimits.Server.Manager
{
    public class ServiceLimitsManagerService : BackgroundService
    {
        private readonly IServiceLimitsCache _serviceLimitsCache;
        private readonly IConfigurationGenerator _configurationGenerator;
        private readonly Serializer _serializer;
        private readonly IConfiguration _eauConfiguration;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IAppParameters _appParameters;

        public ServiceLimitsManagerService(
            IServiceLimitsCache serviceLimitsCache,
            IConfigurationGenerator configurationGenerator,
            IConfiguration configuration,
            ILogger<ServiceLimitsManagerService> logger, 
            IAppParameters appParameters)
        {
            _serviceLimitsCache = serviceLimitsCache;
            _configurationGenerator = configurationGenerator;

            _serializer = new Serializer();

            _eauConfiguration = configuration.GetEAUSection();

            _configuration = configuration;

            _logger = logger;

            _appParameters = appParameters;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            try
            {
                await _appParameters.EnsureLoadedAsync(stoppingToken);

                /*Регистрира параметрите на ПЕАУ за инвалидиране при промяна*/
                (_configuration as IConfigurationRoot).RegisterAppParametersSourceInEAUConfiguration(_appParameters);

                await _serviceLimitsCache.EnsureLoadedAsync(stoppingToken);
                SyncConfiguration();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }

            ChangeToken.OnChange(() =>
            {
                return GetChangeToken();
            }, () =>
            {
                try
                {
                    SyncConfiguration();
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                }
            });
        }

        private IChangeToken GetChangeToken()
        {
            return _serviceLimitsCache.GetChangeToken();
        }

        private void SyncConfiguration()
        {
            var dataServiceLimits = _serviceLimitsCache.GetDataServiceLimits();
            var dataServiceUserLimits = _serviceLimitsCache.GetDataServiceUserLimits();

            var configurationRoot = _configurationGenerator.GenerateConfiguration(dataServiceLimits, dataServiceUserLimits);

            var fileToWrite = _eauConfiguration["SERVICE_LIMIT_CFG_FILE"];

            WriteConfigurationTo(fileToWrite, configurationRoot);

            _logger.LogInformation("Configuration Written to {0}", fileToWrite);

            StartSyncProcess();
        }

        private void StartSyncProcess()
        {
            var processToStart = _eauConfiguration["SERVICE_LIMIT_ON_SYNC_PROCESS"];
            var processArgs = _eauConfiguration["SERVICE_LIMIT_ON_SYNC_PROCESS_ARGS"];

            if (!string.IsNullOrEmpty(processToStart))
            {
                using (var process = Process.Start(new ProcessStartInfo()
                {
                    FileName = processToStart,
                    Arguments = processArgs,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }))
                {
                    _logger.LogInformation("Started Sync Process Id = {0} {1} with args = {2}", process.Id, processToStart, processArgs);

                    var result = process.StandardOutput.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(result))
                        _logger.LogInformation("Sync Process StandardOutput:{0}", result);

                    var error = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(error))
                        _logger.LogError("Sync Process StandardError:{0}", error);

                    process.WaitForExit(130000);
                }
            }
        }

        private void WriteConfigurationTo(string fileToWrite, ConfigurationRoot root)
        {
            /*Ensure the directory*/
            Directory.CreateDirectory(Path.GetDirectoryName(fileToWrite));

            using (StreamWriter writer = new StreamWriter(fileToWrite))
            {
                _serializer.Serialize(writer, root);
            }
        }
    }
}
