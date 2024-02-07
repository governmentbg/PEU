using CNSys.Data;
using EAU.Data;
using EAU.Services.ServiceInstances.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.ServiceInstances.Repositories
{
    /// <summary>
    /// Интерфейс IServiceInstanceRepository за поддържане и съхранение на обекти от тип ServiceInstance.
    /// </summary>
    public interface IServiceInstanceRepository :
        IRepositoryAsync<ServiceInstance, long?, ServiceInstanceSearchCriteria>
    {
    }


    /// <summary>
    /// Реализация на интерфейс IServiceInstanceRepository за поддържане и съхранение на обекти от тип ServiceInstance.
    /// </summary>
    internal class ServiceInstanceRepository : EAURepositoryBase<ServiceInstance, long?, ServiceInstanceSearchCriteria, ServiceInstanceDataContext>, IServiceInstanceRepository
    {
        #region Constructors

        public ServiceInstanceRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IServiceInstanceEntity

        protected override async Task CreateInternalAsync(ServiceInstanceDataContext context, ServiceInstance item, CancellationToken cancellationToken)
        {
            long? serviceInstanceID = await context.ServiceInstanceCreateAsync((short?)item.Status,
                                item.ApplicantID,
                                item.ServiceInstanceDate,
                                item.ServiceID,
                                item.CaseFileURI,
                                item.AdditionalData,
                                item.StatusDate,
                                cancellationToken);

            item.ServiceInstanceID = serviceInstanceID;
        }

        protected override async Task UpdateInternalAsync(ServiceInstanceDataContext context, ServiceInstance item, CancellationToken cancellationToken)
        {
            await context.ServiceInstanceUpdateAsync(
                item.ServiceInstanceID,
                (short?)item.Status,
                item.AdditionalData,
                item.StatusDate,
                cancellationToken);
        }


        protected override async Task<IEnumerable<ServiceInstance>> SearchInternalAsync(ServiceInstanceDataContext context, PagedDataState state, ServiceInstanceSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await context.ServiceInstanceSearchAsync(
                                                EnumerableExtensions.ToStringNumberCollection(searchCriteria.ServiceInstanceIDs),
                                                (short?)searchCriteria.Status,
                                                searchCriteria.ApplicantID,
                                                searchCriteria.ServiceInstanceDateFrom,
                                                searchCriteria.ServiceInstanceDateTo,
                                                searchCriteria.ServiceID,
                                                searchCriteria.CaseFileURI,
                                                searchCriteria.LoadOption?.LoadWithLock,
                                                state.StartIndex,
                                                state.PageSize,
                                                (state.StartIndex == 1),
                                                cancellationToken);

            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<ServiceInstance>> SearchInternalAsync(ServiceInstanceDataContext context, ServiceInstanceSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }


        #endregion
    }
}
