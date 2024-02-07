using CNSys.Caching;
using EAU.Nomenclatures;
using EAU.Payments.RegistrationsData.Models;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.RegistrationsData.Cache
{
    /// <summary>
    /// Интерфейс за работа с кеш на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    public interface IRegistrationsData : ILoadable
    {
        /// <summary>
        /// Операция за изчитане на  кеш на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
        /// </summary>
        IEnumerable<RegistrationData> GetRegistrationData();

        /// <summary>
        /// Операция за изчитане на  кеш на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
        /// </summary>
        /// <param name="lastModifiedDate">Дата на последна редакция.</param>
        /// <returns></returns>
        IEnumerable<RegistrationData> GetRegistrationData(out DateTime? lastModifiedDate);

        /// <summary>
        /// Операция за изчитане на токен по промяна.
        /// </summary>
        /// <returns></returns>
        IChangeToken GetChangeToken();
    }
}
