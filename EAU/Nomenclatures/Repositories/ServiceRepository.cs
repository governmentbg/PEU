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
    /// Критерии за търсене за работа с одит
    /// </summary>
    public class ServiceSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на услуги
        /// </summary>
        public int[] IDs { get; set; }
        /// <summary>
        /// Уникален идентификатор на запис за език.
        /// </summary>
        public int? LanguageID { get; set; }
        /// <summary>
        /// Код на запис на услуга.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Превод на наименование на услуга.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Флаг, указващ дали да бъде заредено описанието на услуга.
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
        /// Флаг указващ дали услугата е активна
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// УРИ на административна услуга
        /// </summary>
        public string SunauServiceUri { get; set; }
        /// <summary>
        /// Идентификатор на група
        /// </summary>
        public int? GroupID { get; set; }
        /// <summary>
        /// Вид приложен документ
        /// </summary>
        public DocumentType AttachedDocumentType { get; set; }
        /// <summary>
        /// Флаг, указващ дали да бъдат заредени стойностости от превод, независимо дали има такъв.
        /// </summary>
        public bool? ForceTranslated { get; set; }
    }

    /// <summary>
    /// Интерфeйс за поддържане и съхранение на обекти от тип Service.
    /// </summary>
    public interface IServiceRepository :
        IRepositoryAsync<Service, int?, ServiceSearchCriteria>,
        ISearchCollectionInfo2<Service, ServiceSearchCriteria>
    {
    }

    /// <summary>
    /// Реализация на интерфейс IServiceRepository за поддържане и съхранение на обекти от тип Service.
    /// </summary>

    internal class ServiceRepository : EAURepositoryBase<Service, int?, ServiceSearchCriteria, ServiceDataContext>, IServiceRepository
    {
        #region Constructors

        public ServiceRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region CRUD

        protected override async Task CreateInternalAsync(ServiceDataContext context, Service item, CancellationToken cancellationToken)
        {
            item.ServiceID = await context.CreateAsync(
                item.GroupID,
                item.Name,
                item.DocumentTypeID,
                item.SunauServiceUri,
                (short?)item.InitiationTypeID,
                item.ResultDocumentName,
                item.Description,
                item.ExplanatoryTextService,
                item.ExplanatoryTextFulfilledService,
                item.ExplanatoryTextRefusedOrTerminatedService,
                item.OrderNumber,
                item.AdmStructureUnitName,
                item.AttachedDocumentsDescription,
                item.AdditionalConfiguration,
                item.ServiceUrl,
                item.IsActive,
                cancellationToken);
        }

        protected override async Task UpdateInternalAsync(ServiceDataContext context, Service item, CancellationToken cancellationToken)
        {
            await context.UpdateAsync(
                item.ServiceID,
                item.GroupID,
                item.Name,
                item.DocumentTypeID,
                item.SunauServiceUri,
                (short?)item.InitiationTypeID,
                item.ResultDocumentName,
                item.Description,
                item.ExplanatoryTextService,
                item.ExplanatoryTextFulfilledService,
                item.ExplanatoryTextRefusedOrTerminatedService,
                item.OrderNumber,
                item.AdmStructureUnitName,
                item.AttachedDocumentsDescription,
                item.AdditionalConfiguration,
                item.ServiceUrl,
                item.IsActive,
                cancellationToken);
        }

        protected override async Task DeleteInternalAsync(ServiceDataContext context, Service item, CancellationToken cancellationToken)
        {
            await context.DeleteAsync(item.ServiceID, cancellationToken);
        }

        protected override Task<IEnumerable<Service>> SearchInternalAsync(ServiceDataContext context, ServiceSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        protected override async Task<IEnumerable<Service>> SearchInternalAsync(ServiceDataContext dataContext, PagedDataState state, ServiceSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count, lastUpdated) = await dataContext.SearchAsync(
                     EnumerableExtensions.ToStringNumberCollection(searchCriteria.IDs),
                     searchCriteria.LanguageID,
                     searchCriteria.ForceTranslated,
                     state.StartIndex,
                     state.PageSize,
                     (state.StartIndex == 1),
                     cancellationToken);

            state.Count = count ?? state.Count;

            return  data.ToList();
        }

        public Task<CollectionInfo<Service>> SearchInfoAsync(ServiceSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInfoAsync(PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task<CollectionInfo<Service>> SearchInfoAsync(PagedDataState state, ServiceSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (dc, innerToken) =>
            {
                var (data, count, lastUpdated) = await dc.SearchAsync(
                    EnumerableExtensions.ToStringNumberCollection(searchCriteria.IDs),
                    searchCriteria.LanguageID,
                    searchCriteria.ForceTranslated,
                    state.StartIndex,
                    state.PageSize,
                    (state.StartIndex == 1),
                    innerToken);

                state.Count = count ?? state.Count;

                return new CollectionInfo<Service>()
                {
                    Data = data.ToList(),
                    LastUpdatedOn = lastUpdated.GetValueOrDefault()
                };

            }, cancellationToken);
        }

        #endregion
    }
}
