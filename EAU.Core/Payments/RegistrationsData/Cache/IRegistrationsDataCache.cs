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
    public interface IRegistrationsDataCache : IDataCacheItem<CachedDataInfo<List<RegistrationData>>>
    {
    }
}
