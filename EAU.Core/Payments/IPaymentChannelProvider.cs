using EAU.Payments.RegistrationsData.Models;

namespace EAU.Payments
{
    /// <summary>
    /// Интерфейс за достъп до услуги за работа с канали за плащане.
    /// </summary>
    public interface IPaymentChannelProvider
    {
        /// <summary>
        /// Операция за изчитане на услуга за работа с канали за плащане.
        /// </summary>
        /// <param name="regDataType"></param>
        /// <returns>Тип на регистрационните данни</returns>
        IPaymentChannelService GetPaymentChannelService(RegistrationDataTypes regDataType);
    }
}