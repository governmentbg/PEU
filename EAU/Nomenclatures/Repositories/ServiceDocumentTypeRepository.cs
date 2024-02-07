using CNSys.Data;
using EAU.Data;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories.DataContexts;
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
    public class ServiceDocumentTypeSearchCriteria
    {
        /// <summary>
        /// Идентификатори на услуги
        /// </summary>
        public int[] ServiceIDs { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип ServiceDeliveryChannel.
    /// </summary>
    public interface IServiceDocumentTypeRepository :
        IRepositoryAsync<ServiceDocumentType, int?, ServiceDocumentTypeSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceDeliveryChannelRepository за поддържане и съхранение на обекти от тип ServiceDeliveryChannel.
    /// </summary>
    internal class ServiceDocumentTypeRepository : EAURepositoryBase<ServiceDocumentType, int?, ServiceDocumentTypeSearchCriteria, ServiceDocumentTypeDataContext>, IServiceDocumentTypeRepository
    {
        #region Constructors

        public ServiceDocumentTypeRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceDocumentTypeDataContext context, ServiceDocumentType item, CancellationToken cancellationToken)
        {
            await context.CreateAsync(
                item.ServiceID,
                item.DocTypeID,
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(ServiceDocumentTypeDataContext context, ServiceDocumentType item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(
                item.ServiceID,
                item.DocTypeID,
                cancellationToken);
        }

        protected override Task<IEnumerable<ServiceDocumentType>> SearchInternalAsync(ServiceDocumentTypeDataContext context, ServiceDocumentTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        protected override async Task<IEnumerable<ServiceDocumentType>> SearchInternalAsync(ServiceDocumentTypeDataContext dataContext, PagedDataState state, ServiceDocumentTypeSearchCriteria searchCriteria, CancellationToken cancellationToken)
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
