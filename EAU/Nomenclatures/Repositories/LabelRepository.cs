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
    /// Критерии за търсене на превод на ресурс от чужд език.
    /// </summary>
    public class LabelSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Колекция от уникални идентификатори на етикети.
        /// </summary>
        public int[] LabelIDs { get; set; }
        /// <summary>
        /// Уникален идентификатор на запис за език.
        /// </summary>
        public int? LanguageID { get; set; }
        /// <summary>
        /// Код на запис на етикет.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Текст ан превод на етикет.
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Флаг, указващ дали да бъде заредено описанието на етикета.
        /// </summary>
        public bool LoadDecsription { get; set; }
        /// <summary>
        /// Флаг, указващ дали превода да се зареди в отделни полета.
        /// </summary>
        public bool LoadSeparateValueI18N { get; set; }
        /// <summary>
        /// Флаг, указващ дали да бъдат заредени само тези, които не са преведени.
        /// </summary>
        public bool LoadOnlyUntranslated { get; set; }
        /// <summary>
        /// Флаг, указващ дали да бъдат заредени стойностости от превод, независимо дали има такъв.
        /// </summary>
        public bool? ForceTranslated { get; set; }
    }

    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип Label.
    /// </summary>
    public interface ILabelRepository :
        IRepositoryAsync<Label, int?, LabelSearchCriteria>,
        ISearchCollectionInfo2<Label, LabelSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс ILabelEntity за поддържане и съхранение на обекти от тип Label.
    /// </summary>
    internal class LabelRepository : EAURepositoryBase<Label, int?, LabelSearchCriteria, LabelDataContext>, ILabelRepository
    {
        #region Constructors

        public LabelRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task UpdateInternalAsync(LabelDataContext context, Label item, CancellationToken token)
        {
            await context.LabelUpdateAsync(item.LabelID,
                                    item.Code,
                                    item.Value,
                                    item.Description,
                                    token);
        }

        public Task<CollectionInfo<Label>> SearchInfoAsync(LabelSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Label>> SearchInfoAsync(PagedDataState state, LabelSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.LabelIDs),
                    searchCriteria.LanguageID,
                    searchCriteria.Code,
                    searchCriteria.Value,
                    searchCriteria.LoadDecsription,
                    searchCriteria.LoadOnlyUntranslated,
                    searchCriteria.ForceTranslated,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Label>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }
       
        #endregion
    }
}
