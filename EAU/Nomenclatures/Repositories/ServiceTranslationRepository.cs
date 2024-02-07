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
    /// Интерфейс за поддържане и съхранение на обекти от тип ServiceTranslation.
    /// </summary>
    public interface IServiceTranslationRepository :
        IRepositoryAsync<ServiceTranslation, long?, ServiceSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceTranslationRepository за поддържане и съхранение на обекти от тип ServiceTranslation.
    /// </summary>
    internal class ServiceTranslationRepository : EAURepositoryBase<ServiceTranslation, long?, ServiceSearchCriteria, ServiceDataContext>, IServiceTranslationRepository
    {
        #region Constructors

        public ServiceTranslationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceDataContext context, ServiceTranslation item, CancellationToken cancellationToken)
        {
            await context.ServiceTranslationCreateAsync(
                item.ServiceID,
                item.LanguageID,
                item.Name,
                item.Description,
                item.AttachedDocumentsDescription,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(ServiceDataContext context, ServiceTranslation item, CancellationToken cancellationToken)
        {
            await context.ServiceTranslationUpdateAsync(
                item.ServiceID,
                item.LanguageID,
                item.Name,
                item.Description,
                item.AttachedDocumentsDescription,
                cancellationToken);
        }

        #endregion
    }
}
