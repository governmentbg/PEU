using CNSys.Data;
using EAU.CMS.Models;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.CMS.Repositories
{
    /// <summary>
    /// Критерии за търсене на страници със съдържание.
    /// </summary>
    public class PageSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на страници със съдържание.
        /// </summary>
        public int[] PageIDs { get; set; }
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
    /// Интерфейс за поддържане и съхранение на страници със съдържание.
    /// </summary>
    public interface IPageRepository :
        IRepositoryAsync<Page, long?, PageSearchCriteria>,
        ISearchCollectionInfo2<Page, PageSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IPageEntity за поддържане и съхранение на обекти от тип Page.
    /// </summary>
    internal class PageRepository : EAURepositoryBase<Page, long?, PageSearchCriteria, PageDataContext>, IPageRepository
    {
        #region Constructors

        public PageRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }


        #endregion

        #region CRUD

        protected override Task UpdateInternalAsync(PageDataContext context, Page item, CancellationToken cancellationToken)
        {
            return context.PageUpdateAsync(
                                item.Code,
                                item.Title,
                                item.Content,
                                cancellationToken);
        }

        public Task<CollectionInfo<Page>> SearchInfoAsync(PageSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Page>> SearchInfoAsync(PagedDataState state, PageSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.PageIDs),
                    searchCriteria.LanguageID,
                    searchCriteria.ForceTranslated,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Page>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
