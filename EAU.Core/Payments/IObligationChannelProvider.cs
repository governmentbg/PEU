using EAU.Payments.Obligations.Models;

namespace EAU.Payments
{
    /// <summary>
    /// Интерфейс за достъп до услуги за работа с канали за задължения.
    /// </summary>
    public interface IObligationChannelProvider
    {
        /// <summary>
        /// Операця за изчитане на услуга за работа с канали за задължения.
        /// </summary>
        /// <param name="obligationType">Видове задължение</param>
        /// <returns>Услуга за работа с канали за задължения.</returns>
        IObligationChannelService GetObligationChannelService(ObligationTypes obligationType);
    }
}
