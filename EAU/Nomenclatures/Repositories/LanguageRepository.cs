using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    public class LanguageSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатор на език.
        /// </summary>
        public int? LanguageID { get; set; }

        /// <summary>
        /// Код на език.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Име на език.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Флаг, дали статусът е активен.
        /// </summary>
        public bool? IsActive { get; set; }
    }

    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип Language.
    /// </summary>
    public interface ILanguageRepository :
        IRepositoryAsync<Language, long?, LanguageSearchCriteria>,
        ISearchCollectionInfo2<Language, LanguageSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс ILanguageEntity за поддържане и съхранение на обекти от тип Language.
    /// </summary>
    internal class LanguageRepository : EAURepositoryBase<Language, long?, LanguageSearchCriteria, LanguageDataContext>, ILanguageRepository
    {
        #region Constructors

        public LanguageRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(LanguageDataContext context, Language item, CancellationToken cancellationToken)
        {
            item.LanguageID = await context.CreateAsync(item.Code, item.Name, item.IsActive, cancellationToken);
        }

        protected override async Task UpdateInternalAsync(LanguageDataContext context, Language item, CancellationToken cancellationToken)
        {
            await context.UpdateAsync(
                item.LanguageID,
                item.IsActive,
                cancellationToken);
        }

        protected override IEnumerable<Language> SearchInternal(LanguageDataContext context, LanguageSearchCriteria searchCriteria)
        {
            return SearchInternal(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria);
        }

        public Task<CollectionInfo<Language>> SearchInfoAsync(LanguageSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Language>> SearchInfoAsync(PagedDataState state, LanguageSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    searchCriteria.LanguageID,
                    searchCriteria.Code,
                    searchCriteria.Name,
                    searchCriteria.IsActive,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Language>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }
        #endregion
    }
}
