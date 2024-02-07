using CNSys.Data;
using EAU.Common.Models;
using EAU.Data;
using EAU.Nomenclatures.Models;
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
    public class ServiceDeclarationSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на услуги
        /// </summary>
        public int[] ServiceIDs { get; set; }

        /// <summary>
        /// Идентификатор на декларативно обстоятелство/ политика
        /// </summary>
        public int? DeclarationID { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип ServiceDeclaration.
    /// </summary>
    public interface IServiceDeclarationRepository :
        IRepositoryAsync<ServiceDeclaration, int?, ServiceDeclarationSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceDeclarationRepository за поддържане и съхранение на обекти от тип ServiceDeclaration.
    /// </summary>
    internal class ServiceDeclarationRepository : EAURepositoryBase<ServiceDeclaration, int?, ServiceDeclarationSearchCriteria, ServiceDeclarationDataContext>, IServiceDeclarationRepository
    {
        #region Constructors

        public ServiceDeclarationRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceDeclarationDataContext context, ServiceDeclaration item, CancellationToken cancellationToken)
        {
            await context.CreateAsync(
                item.ServiceID,
                item.DeclarationID,
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(ServiceDeclarationDataContext context, ServiceDeclaration item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(
                item.ServiceID,
                item.DeclarationID,
                cancellationToken);
        }

        protected override Task<IEnumerable<ServiceDeclaration>> SearchInternalAsync(ServiceDeclarationDataContext context, ServiceDeclarationSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        protected override async Task<IEnumerable<ServiceDeclaration>> SearchInternalAsync(ServiceDeclarationDataContext dataContext, PagedDataState state, ServiceDeclarationSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var(data, count) = await dataContext.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.ServiceIDs),
                    searchCriteria.DeclarationID,
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
