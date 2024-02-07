using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    /// <summary>
    /// Критерии за търсене за работа с 
    /// </summary>
    public class ServiceTermSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатор на услуга
        /// </summary>
        public int? ServiceID { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип ServiceTerm.
    /// </summary>
    public interface IServiceTermRepository :
        IRepositoryAsync<ServiceTerm, int?, ServiceTermSearchCriteria>,
        ISearchCollectionInfo2<ServiceTerm, ServiceTermSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceTermRepository за поддържане и съхранение на обекти от тип ServiceTerm.
    /// </summary>
    internal class ServiceTermRepository : EAURepositoryBase<ServiceTerm, int?, ServiceTermSearchCriteria, ServiceTermDataContext>, IServiceTermRepository
    {
        #region Constructors

        public ServiceTermRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceTermDataContext context, ServiceTerm item, CancellationToken cancellationToken)
        {
            item.ServiceTermID = await context.CreateAsync(
                item.ServiceID,
                (short?)item.ServiceTermType,
                item.Price,
                item.ExecutionPeriod,
                item.Description,
                (short?)item.PeriodType,
                item.IsActive,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(ServiceTermDataContext context, ServiceTerm item, CancellationToken cancellationToken)
        {
            await context.UpdateAsync(
                item.ServiceTermID,
                item.ServiceID,
                (short?)item.ServiceTermType,
                item.Price,
                item.ExecutionPeriod,
                item.Description,
                (short?)item.PeriodType,
                item.IsActive,
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(ServiceTermDataContext context, ServiceTerm item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(item.ServiceTermID, cancellationToken);
        }

        public Task<CollectionInfo<ServiceTerm>> SearchInfoAsync(ServiceTermSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<ServiceTerm>> SearchInfoAsync(PagedDataState state, ServiceTermSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    searchCriteria.ServiceID,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<ServiceTerm>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
