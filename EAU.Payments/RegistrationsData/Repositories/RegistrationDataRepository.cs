using CNSys.Data;
using EAU.Data;
using EAU.Nomenclatures.Repositories;
using EAU.Payments.RegistrationsData.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.RegistrationsData.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    public interface IRegistrationDataRepository :
        IRepositoryAsync<RegistrationData, long?, RegistrationDataSearchCriteria>,
        ISearchCollectionInfo2<RegistrationData, RegistrationDataSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IEAURegistrationDataEntity за поддържане и съхранение на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    internal class RegistrationDataRepository : EAURepositoryBase<RegistrationData, long?, RegistrationDataSearchCriteria, RegistrationDataDataContext>, IRegistrationDataRepository
    {
        #region Constructors

        public RegistrationDataRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }


        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(RegistrationDataDataContext context, RegistrationData item, CancellationToken cancellationToken)
        {
            int? itemID = await context.RegistrationDataCreateAsync(
                                (int?)item.Type,
                                item.Description,
                                item.Cin,
                                item.Email,
                                item.SecretWord,
                                item.ValidityPeriod,
                                item.PortalUrl,
                                item.NotificationUrl,
                                item.ServiceUrl,
                                item.IBAN,
                                cancellationToken);

            item.RegistrationDataID = itemID;
        }

        protected override Task UpdateInternalAsync(RegistrationDataDataContext context, RegistrationData item, CancellationToken cancellationToken)
        {
            return context.RegistrationDataUpdateAsync(
                                item.RegistrationDataID,
                                (int?)item.Type,
                                item.Description,
                                item.Cin,
                                item.Email,
                                item.SecretWord,
                                item.ValidityPeriod,
                                item.PortalUrl,
                                item.NotificationUrl,
                                item.ServiceUrl,
                                item.IBAN,
                                cancellationToken);
        }

        protected override Task DeleteInternalAsync(RegistrationDataDataContext context, RegistrationData item, CancellationToken cancellationToken)
        {
            return DeleteInternalAsync(context, item.RegistrationDataID, cancellationToken);
        }

        protected override Task DeleteInternalAsync(RegistrationDataDataContext context, long? key, CancellationToken cancellationToken)
        {
            if (key == null)
                throw new ArgumentNullException();

            return context.RegistrationDataDeleteAsync((int?)key, cancellationToken);
        }

        protected override async Task<IEnumerable<RegistrationData>> SearchInternalAsync(RegistrationDataDataContext context, PagedDataState state, RegistrationDataSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count, lastUpdated) = await context.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.RegistrationDataIDs),
                    (int?)searchCriteria.Type,
                    searchCriteria.Cin,
                    searchCriteria.IBAN,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    cancellationToken);

            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<RegistrationData>> SearchInternalAsync(RegistrationDataDataContext context, RegistrationDataSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<RegistrationData>> SearchInfoAsync(RegistrationDataSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<RegistrationData>> SearchInfoAsync(PagedDataState state, RegistrationDataSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.RegistrationDataIDs),
                    (int?)searchCriteria.Type,
                    searchCriteria.Cin,
                    searchCriteria.IBAN,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<RegistrationData>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
