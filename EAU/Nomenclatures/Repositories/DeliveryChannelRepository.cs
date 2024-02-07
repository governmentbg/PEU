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
    /// Критерии за търсене на начините на получаване на резултат от услуга.
    /// </summary>
    public class DeliveryChannelSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на начините на получаване на резултат от услуга.
        /// </summary>
        public int[] IDs { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип DeliveryChannel.
    /// </summary>
    public interface IDeliveryChannelRepository :
        ISearchCollectionInfo2<DeliveryChannel, DeliveryChannelSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IDocumentTypeRepository за поддържане и съхранение на обекти от тип DocumentType.
    /// </summary>
    internal class DeliveryChannelRepository : EAURepositoryBase<DeliveryChannel, int?, DeliveryChannelSearchCriteria, DeliveryChannelDataContext>, IDeliveryChannelRepository
    {
        #region Constructors

        public DeliveryChannelRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region IDeliveryChannelRepository

        public Task<CollectionInfo<DeliveryChannel>> SearchInfoAsync(DeliveryChannelSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<DeliveryChannel>> SearchInfoAsync(PagedDataState state, DeliveryChannelSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.IDs),
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<DeliveryChannel>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion

    }
}
