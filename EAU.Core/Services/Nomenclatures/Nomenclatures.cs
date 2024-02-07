using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
using EAU.Services.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.Nomenclatures
{
    /// <summary>
    /// Интерфейс за работа с кеш на звената за връчване на услугата
    /// </summary>
    public interface IDeliveryUnitsInfo
    {
        ValueTask EnsureLoadedAsync(int serviceID, CancellationToken cancellationToken);
        ValueTask EnsureLoadedAsync(int serviceID, AdmServiceTermType? serviceTermType, CancellationToken cancellationToken);

        IEnumerable<UnitInfo> Search(int serviceID, out DateTime? lastModifiedDate);
        IEnumerable<UnitInfo> Search(int serviceID, AdmServiceTermType? serviceTermType, out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на ROOT звената за връчване на услугата
    /// </summary>
    public interface IDeliveryUnitsRootInfo
    {
        ValueTask EnsureLoadedAsync(int serviceID, CancellationToken cancellationToken);
        ValueTask EnsureLoadedAsync(int serviceID, AdmServiceTermType? serviceTermType, CancellationToken cancellationToken);

        IEnumerable<UnitInfo> Search(int serviceID, out DateTime? lastModifiedDate);
        IEnumerable<UnitInfo> Search(int serviceID, AdmServiceTermType? serviceTermType, out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на звената изпълнители на услугата
    /// </summary>
    public interface IServingUnitsInfo
    {
        ValueTask EnsureLoadedAsync(int serviceID, CancellationToken cancellationToken);

        IEnumerable<UnitInfo> Search(int serviceID, out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Интерфейс за работа с кеш на причини за прекратяване на регистрация на ПС
    /// </summary>
    public interface ITerminationOfRegistrationOfVehicleReasons: ILoadable
    {
        IEnumerable<Nomenclature> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Извличане на данни за причината за издаване на дубликат на СРМПС
    /// </summary>
    public interface IReasonsForIssuingDuplicateOfSRMPS : ILoadable
    {
        IEnumerable<Nomenclature> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Номенклатурата на държавите в МВР
    /// </summary>
    public interface ICountries : ILoadable
    {
        IEnumerable<Nomenclature> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Номенклатурата на цветовете МПС в МВР
    /// </summary>
    public interface IVehicleBaseColors : ILoadable
    {
        IEnumerable<Nomenclature> Search(out DateTime? lastModifiedDate);
    }

    /// <summary>
    /// Номенклатура на първите букви на регистрационните номера
    /// </summary>
    public interface IRegistrationNumberProvinceCodes
    {
        ValueTask EnsureLoadedAsync(int policeDepartmentID, int vehicleTypeCode, CancellationToken cancellationToken);

        IEnumerable<string> Search(int policeDepartmentID, int vehicleTypeCode, out DateTime? lastModifiedDate);
    }
}
