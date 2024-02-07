using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Pb.Lyft.Ratelimit;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EAU.ServiceLimits.AspNetCore
{
    public class LyftServieRateLimiter : IServiceLimiter
    {
        private readonly GrpcChannel _channel;
        private readonly RateLimitService.RateLimitServiceClient _rateLimiter;
        private readonly string _domain;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IAsyncPolicy _rateLimiterPolicy;

        public LyftServieRateLimiter(IConfiguration configuration, ILoggerFactory loggerFactory, ILogger<LyftServieRateLimiter> logger)
        {
            _configuration = configuration.GetEAUSection();

            _domain = _configuration["SERVICE_LIMIT_DOMAIN"];

            var limitServer = _configuration["SERVICE_LIMIT_SERVER"];

            _channel = GrpcChannel.ForAddress(limitServer, new GrpcChannelOptions() { LoggerFactory = loggerFactory });

            _rateLimiter = new RateLimitService.RateLimitServiceClient(_channel);

            _logger = logger;

            _rateLimiterPolicy = Policy.Handle<Exception>().CircuitBreakerAsync(3, new TimeSpan(0, 0, 13), OnBreak, OnReset);
        }
        public async Task<bool> ShouldRateLimitAsync(StringValues serviceCodes, int? userCIN, IPAddress userIPAddress)
        {
            if (_configuration.GetValue<int>("SERVICE_LIMIT_DISABLED") > 0)
                return false;

            try
            {
                return await _rateLimiterPolicy.ExecuteAsync(async () =>
                {
                    var request = new RateLimitRequest()
                    {
                        Domain = _domain
                    };

                    string entryValue = userCIN.HasValue ? string.Format("CIN:{0}", userCIN) : string.Format("IP:{0}", userIPAddress);

                    foreach (var serviceCode in serviceCodes)
                    {
                        var descriptor = new RateLimitDescriptor();

                        request.Descriptors.Add(descriptor);

                        var entry = new RateLimitDescriptor.Types.Entry();

                        descriptor.Entries.Add(entry);

                        entry.Key = serviceCode;
                        entry.Value = entryValue;
                    }

                    var response = await _rateLimiter.ShouldRateLimitAsync(request);

                    return response.OverallCode == RateLimitResponse.Types.Code.OverLimit;
                });
            }
            catch(BrokenCircuitException)
            {
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);

                /*Ако има проблем с извикването на услугата за лимитиране, не ограничаваме заявките*/
                return false;
            }
        }

        private void OnBreak(Exception exception, TimeSpan duration)
        {
            _logger.LogCritical("Rate Limiting CircuitBreaker was broken");
        }

        private void OnReset()
        {
            _logger.LogCritical("Rate Limiting CircuitBreaker was reset.");
        }
    }
}
