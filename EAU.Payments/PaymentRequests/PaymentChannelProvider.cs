using EAU.Payments.PaymentRequests.Epay;
using EAU.Payments.PaymentRequests.PepDaeu;
using EAU.Payments.RegistrationsData.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EAU.Payments.PaymentRequest
{
    /// <summary>
    /// Реализация на интерфейс за достъп до услуги за работа с канали за плащане.
    /// </summary>
    public class PaymentChannelProvider : IPaymentChannelProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentChannelProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentChannelService GetPaymentChannelService(RegistrationDataTypes regDataType)
        {
            if (regDataType == RegistrationDataTypes.ePay)
            {
                return _serviceProvider.GetRequiredService<EpayPaymentChannelService>();
            }
            else if (regDataType == RegistrationDataTypes.PepOfDaeu)
            {
                return _serviceProvider.GetRequiredService<PepDaeoPaymentChannelService>();
            }
            else
            {
                return null;
            }
        }
    }
}
