using CNSys.Data;
using EAU.CMS.Models;
using EAU.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.CMS.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип PageTranslation.
    /// </summary>
    public interface IPageTranslationRepository :
        IRepositoryAsync<PageTranslation, long?, PageSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IPageTranslationRepository за поддържане и съхранение на обекти от тип PageTranslation.
    /// </summary>
    internal class PageTranslationRepository : EAURepositoryBase<PageTranslation, long?, PageSearchCriteria, PageDataContext>, IPageTranslationRepository
    {
        #region Constructors

        public PageTranslationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(PageDataContext context, PageTranslation item, CancellationToken cancellationToken)
        {
            await context.PageTranslationCreateAsync(
                item.PageID,
                item.LanguageID,
                item.Title,
                item.Content,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(PageDataContext context, PageTranslation item, CancellationToken cancellationToken)
        {
            await context.PageTranslationUpdateAsync(
                item.PageID,
                item.LanguageID,
                item.Title,
                item.Content,
                cancellationToken);
        }

        #endregion
    }
}
