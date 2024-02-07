using CNSys.Data;
using EAU.Data;
using EAU.Nomenclatures.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    /// <summary>
    /// Интерфейс за поддържане и съхранение на обекти от тип LabelTranslation.
    /// </summary>
    public interface ILabelTranslationRepository :
        IRepositoryAsync<LabelTranslation, long?, LabelSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс ILabelTranslationEntity за поддържане и съхранение на обекти от тип LabelTranslation.
    /// </summary>
    internal class LabelTranslationRepository : EAURepositoryBase<LabelTranslation, long?, LabelSearchCriteria, LabelDataContext>, ILabelTranslationRepository
    {
        #region Constructors

        public LabelTranslationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(LabelDataContext context, LabelTranslation item, CancellationToken cancellationToken)
        {
            await context.LabelTranslationCreateAsync(
                item.LabelID, 
                item.LanguageID, 
                item.Value,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(LabelDataContext context, LabelTranslation item, CancellationToken cancellationToken)
        {
            await context.LabelTranslationUpdateAsync(
                item.LabelID,
                item.LanguageID,
                item.Value,
                cancellationToken);
        }

        #endregion
    }
}
