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
    /// Критерии за търсене за работа с
    /// </summary>
    public class ServiceDeliveryChannelSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на услуги
        /// </summary>
        public int[] ServiceIDs { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип ServiceDeliveryChannel.
    /// </summary>
    public interface IServiceDeliveryChannelRepository :
        IRepositoryAsync<ServiceDeliveryChannel, int?, ServiceDeliveryChannelSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceDeliveryChannelRepository за поддържане и съхранение на обекти от тип ServiceDeliveryChannel.
    /// </summary>
    internal class ServiceDeliveryChannelRepository : EAURepositoryBase<ServiceDeliveryChannel, int?, ServiceDeliveryChannelSearchCriteria, ServiceDeliveryChannelDataContext>, IServiceDeliveryChannelRepository
    {
        #region Constructors

        public ServiceDeliveryChannelRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceDeliveryChannelDataContext context, ServiceDeliveryChannel item, CancellationToken cancellationToken)
        {
            await context.CreateAsync(
                item.ServiceID,
                item.DeliveryChannelID,
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(ServiceDeliveryChannelDataContext context, ServiceDeliveryChannel item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(
                item.ServiceID, 
                item.DeliveryChannelID, 
                cancellationToken);
        }

        protected override Task<IEnumerable<ServiceDeliveryChannel>> SearchInternalAsync(ServiceDeliveryChannelDataContext context, ServiceDeliveryChannelSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        protected override async Task<IEnumerable<ServiceDeliveryChannel>> SearchInternalAsync(ServiceDeliveryChannelDataContext dataContext, PagedDataState state, ServiceDeliveryChannelSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await dataContext.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.ServiceIDs),
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    cancellationToken);

            state.Count = count ?? state.Count;

            return data.ToList();
        }

        #endregion
    }
}
