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
    /// Критерии за търсене на декларативни обстоятелства и политики.
    /// </summary>
    public class DeclarationSearchCriteria : BasePagedSearchCriteria
    {
        public int ID { get; set; }

        /// <summary>
        /// Идентификатори на декларативни обстоятелства и политики.
        /// </summary>
        public int[] IDs { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип Declaration.
    /// </summary>
    public interface IDeclarationRepository :
        IRepositoryAsync<Declaration, int?, DeclarationSearchCriteria>,
        ISearchCollectionInfo2<Declaration, DeclarationSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IDocumentTypeRepository за поддържане и съхранение на обекти от тип Declaration.
    /// </summary>
    internal class DeclarationRepository : EAURepositoryBase<Declaration, int?, DeclarationSearchCriteria, DeclarationDataContext>, IDeclarationRepository
    {
        #region Constructors

        public DeclarationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(DeclarationDataContext context, Declaration item, CancellationToken cancellationToken)
        {
            item.DeclarationID = await context.CreateAsync(
               item.Description,
               item.Content,
               item.IsRquired,
               item.IsAdditionalDescriptionRequired,
               item.Code,
               cancellationToken);
        }

        protected override async Task UpdateInternalAsync(DeclarationDataContext context, Declaration item, CancellationToken cancellationToken)
        {
            await context.UpdateAsync(
                item.DeclarationID,
                item.Description,
                item.Content,
                item.IsRquired,
                item.IsAdditionalDescriptionRequired,
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(DeclarationDataContext context, Declaration item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(
                item.DeclarationID,
                cancellationToken);
        }

        #endregion

        #region IDocumentTypeRepository

        public Task<CollectionInfo<Declaration>> SearchInfoAsync(DeclarationSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Declaration>> SearchInfoAsync(PagedDataState state, DeclarationSearchCriteria searchCriteria, CancellationToken cancellationToken)
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

                return new CollectionInfo<Declaration>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion

    }
}
