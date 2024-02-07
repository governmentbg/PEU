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
    /// Интерфейс за поддържане и съхранение на обекти от тип ServiceGroupTranslation.
    /// </summary>
    public interface IServiceGroupTranslationRepository :
        IRepositoryAsync<ServiceGroupTranslation, long?, ServiceGroupSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceGroupTranslationRepository за поддържане и съхранение на обекти от тип ServiceGroupTranslation.
    /// </summary>
    internal class ServiceGroupTranslationRepository : EAURepositoryBase<ServiceGroupTranslation, long?, ServiceGroupSearchCriteria, ServiceGroupDataContext>, IServiceGroupTranslationRepository
    {
        #region Constructors

        public ServiceGroupTranslationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceGroupDataContext context, ServiceGroupTranslation item, CancellationToken cancellationToken)
        {
            await context.ServiceGroupTranslationCreateAsync(
                item.GroupID,
                item.LanguageID,
                item.Name,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(ServiceGroupDataContext context, ServiceGroupTranslation item, CancellationToken cancellationToken)
        {
            await context.ServiceGroupTranslationUpdateAsync(
                item.GroupID,
                item.LanguageID,
                item.Name,
                cancellationToken);
        }

        #endregion
    }
}
