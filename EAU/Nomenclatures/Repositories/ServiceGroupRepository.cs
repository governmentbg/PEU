using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    /// <summary>
    /// Критерии за търсене за работа с одит
    /// </summary>
    public class ServiceGroupSearchCriteria: BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на групи
        /// </summary>
        public int[] IDs { get; set; }
        /// <summary>
        /// Уникален идентификатор на запис за език.
        /// </summary>
        public int? LanguageID { get; set; }
        /// <summary>
        /// Флаг, указващ дали да бъдат заредени стойностости от превод, независимо дали има такъв.
        /// </summary>
        public bool? ForceTranslated { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип ServiceGroup.
    /// </summary>
    public interface IServiceGroupRepository :
        IRepositoryAsync<ServiceGroup, int?, ServiceGroupSearchCriteria>,
        ISearchCollectionInfo2<ServiceGroup, ServiceGroupSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceGroupRepository за поддържане и съхранение на обекти от тип ServiceGroup.
    /// </summary>
    internal class ServiceGroupRepository : EAURepositoryBase<ServiceGroup, int?, ServiceGroupSearchCriteria, ServiceGroupDataContext>, IServiceGroupRepository
    {
        #region Constructors

        public ServiceGroupRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceGroupDataContext context, ServiceGroup item, CancellationToken cancellationToken)
        {
            item.GroupID = await context.CreateAsync(
                item.Name,
                item.OrderNumber,
                item.IconName,
                item.IsActive,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(ServiceGroupDataContext context, ServiceGroup item, CancellationToken cancellationToken)
        {
            await context.UpdateAsync(
                item.GroupID,
                item.Name,
                item.OrderNumber,
                item.IconName,
                item.IsActive,
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(ServiceGroupDataContext context, ServiceGroup item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(item.GroupID, cancellationToken);
        }

        public Task<CollectionInfo<ServiceGroup>> SearchInfoAsync(ServiceGroupSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<ServiceGroup>> SearchInfoAsync(PagedDataState state, ServiceGroupSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.IDs),
                    searchCriteria.LanguageID,
                    searchCriteria.ForceTranslated,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<ServiceGroup>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
