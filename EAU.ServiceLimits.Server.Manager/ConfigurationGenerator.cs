using EAU.ServiceLimits.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EAU.ServiceLimits.Server.Manager
{
    public interface IConfigurationGenerator
    {
        ConfigurationRoot GenerateConfiguration(IEnumerable<DataServiceLimit> dataServiceLimits, IEnumerable<DataServiceUserLimit> dataServiceUserLimits);
    }
    public class ConfigurationGenerator : IConfigurationGenerator
    {
        private readonly IConfiguration _configuration;

        public ConfigurationGenerator(IConfiguration configuration)
        {
            _configuration = configuration.GetEAUSection();
        }

        public ConfigurationRoot GenerateConfiguration(IEnumerable<DataServiceLimit> dataServiceLimits, IEnumerable<DataServiceUserLimit> dataServiceUserLimits)
        {
            ConfigurationRoot root = new ConfigurationRoot();

            root.Domain = _configuration["SERVICE_LIMIT_DOMAIN"];

            root.Descriptors = CreateDesriptorsForServices(dataServiceLimits, dataServiceUserLimits);

            return root;
        }

        private List<Descriptor> CreateDesriptorsForServices(IEnumerable<DataServiceLimit> dataServiceLimits, IEnumerable<DataServiceUserLimit> dataServiceUserLimits)
        {
            var userLimitsPerServiceLimitlookup = dataServiceUserLimits.ToLookup(item => item.ServiceLimitID.Value);

            List<Descriptor> ret = new List<Descriptor>();

            foreach (var dataServiceLimit in dataServiceLimits)
            {
                ret.Add(CreateServiceLimitDescriptor(dataServiceLimit));

                foreach (var dataServiceUserLimit in userLimitsPerServiceLimitlookup[dataServiceLimit.ServiceLimitID.Value])
                {
                    ret.Add(CreateServiceDescriptorPerUser(dataServiceUserLimit));
                }
            }

            return ret;
        }

        private Descriptor CreateServiceDescriptorPerUser(DataServiceUserLimit dataServiceUserLimit)
        {
            Descriptor ret = new Descriptor();

            ret.Key = dataServiceUserLimit.ServiceLimit.ServiceCode;
            ret.RateLimit = GetRateLimitUnit(dataServiceUserLimit.RequestsInterval, dataServiceUserLimit.RequestsNumber);

            /*използваме КИН за уникална идентификация на потребителите*/
            ret.Value = $"CIN:{dataServiceUserLimit.User.CIN}";

            return ret;
        }

        private Descriptor CreateServiceLimitDescriptor(DataServiceLimit dataServiceLimit)
        {
            Descriptor ret = new Descriptor();

            ret.Key = dataServiceLimit.ServiceCode;

            if (dataServiceLimit.Status == DataServiceLimitStatus.Active)
                ret.RateLimit = GetRateLimitUnit(dataServiceLimit.RequestsInterval, dataServiceLimit.RequestsNumber);

            return ret;
        }

        private RateLimit GetRateLimitUnit(TimeSpan requestsInterval, int requestsNumber)
        {
            RateLimit ret = new RateLimit();

            ret.RequestPerUnit = requestsNumber;

            if (requestsInterval.Ticks == TimeSpan.TicksPerSecond)
                ret.Unit = "second";
            else if (requestsInterval.Ticks == TimeSpan.TicksPerMinute)
                ret.Unit = "minute";
            else if (requestsInterval.Ticks == TimeSpan.TicksPerHour)
                ret.Unit = "hour";
            else if (requestsInterval.Ticks == TimeSpan.TicksPerDay)
                ret.Unit = "day";
            else
            {
                /*ако конфигурацията не е на един от определените интервали се приравнява към минута и се осреднява*/

                ret.Unit = "minute";
                ret.RequestPerUnit = (int)((TimeSpan.TicksPerMinute / requestsInterval.Ticks) * requestsNumber);
            }

            return ret;
        }
    }
}
