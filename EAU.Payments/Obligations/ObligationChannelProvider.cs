using EAU.Payments.Obligations.AND;
using EAU.Payments.Obligations.Models;
using EAU.Payments.Obligations.SerivceInstances;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EAU.Payments.Obligations
{
    /// <summary>
    /// Реализация на интерфейс за достъп до услуги за работа с канали за задължения.
    /// </summary>
    public class ObligationChannelProvider : IObligationChannelProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public ObligationChannelProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IObligationChannelService GetObligationChannelService(ObligationTypes obligationType)
        {
            if (obligationType == ObligationTypes.AND)
            {
                return _serviceProvider.GetRequiredService<AndObligationChannelService>();
            }
            else if (obligationType == ObligationTypes.ServiceInstance)
            {
                return _serviceProvider.GetRequiredService<ServiceInstanceObligationChannelService>();
            }
            else
            {
                return null;
            }
        }
    }
}
