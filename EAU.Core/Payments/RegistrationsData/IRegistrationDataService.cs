using CNSys;
using EAU.Payments.RegistrationsData.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.RegistrationsData
{
    /// <summary>
    /// Интерфейс за работа с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    public interface IRegistrationDataService
    {
        /// <summary>
        /// Редакция на стойност на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
        /// </summary>
        /// <param name="registrationDataID">Идентификатор на регистрационните данни.</param>
        /// <param name="description">Описание.</param>
        /// <param name="cin">КИН на ПЕАУ - клиентски номер в личните данни на търговеца.</param>
        /// <param name="email">E-mail на ПЕАУ по регистрация.</param>
        /// <param name="secretWord">Буквено цифрова секретна дума генерирана от ePay.</param>
        /// <param name="validityPeriod">Период на валидност на плащане - необходим за определяне на крайната дата и час на валидността на плащането през оператора.</param>
        /// <param name="portalUrl">Адрес за достъп.</param>
        /// <param name="notificationUrl">Електронен адрес за уведомяване - електронен адрес, на който се изпраща съобщение за променен статус на заявка за плащане.</param>
        /// <param name="serviceUrl">URL за достъп до услугите на ПЕП на ДАЕУ.</param>
        /// <param name="iban">URL за достъп до услугите на ПЕП на ДАЕУ.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        Task<OperationResult> UpdateAsync(int registrationDataID, string description, string cin, string email, string secretWord,
            TimeSpan? validityPeriod, string portalUrl, string notificationUrl, string serviceUrl, string iban, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за създаване на регистрационни данни на ПЕАУ в системата на платежен оператор.
        /// </summary>
        /// <param name="item">Регистрационни данни на ПЕАУ в системата на платежен оператор</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        Task<OperationResult> CreateAsync(RegistrationData item, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за изтриване на регистрационни данни на ПЕАУ в системата на платежен оператор ако за тях няма направено плащане.
        /// </summary>
        /// <param name="registrationDataID">Идентификатор на регистрационните данни.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns></returns>
        Task<OperationResult> DeleteAsync(int registrationDataID, CancellationToken cancellationToken);
    }
}
