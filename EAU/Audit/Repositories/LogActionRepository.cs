using CNSys.Data;
using EAU.Audit.Models;
using EAU.Common.Models;
using EAU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Audit.Repositories
{
    /// <summary>
    /// Критерии за търсене за работа с одит
    /// </summary>
    public class LogActionSearchCriteria
    {
        /// <summary>
        /// Режим за търсене на записи в одит: Търсене в оперативна база = 1; Търсене в архивна база = 2.
        /// </summary>
        public LogActionSearchModes Mode { get; set; }

        /// <summary>
        /// Идентификатори на записи в одит.
        /// </summary>
        public int[] LogActionIDs { get; set; }

        /// <summary>
        /// Период от.
        /// </summary>
        public DateTime? LogActionDateFrom { get; set; }

        /// <summary>
        /// Период до.
        /// </summary>
        public DateTime? LogActionDateTo { get; set; }

        /// <summary>
        /// Типа обект.
        /// </summary>
        public ObjectTypes? ObjectType { get; set; }

        /// <summary>
        /// Събитие.
        /// </summary>
        public ActionTypes? ActionType { get; set; }

        /// <summary>
        /// Функционалност/модул през който е настъпило събитието..
        /// </summary>
        public Functionalities? Functionality { get; set; }

        /// <summary>
        /// Стойност на ключов атрибут на обекта - в зависимост от обекта това може да бъде УРИ на преписка, УРИ на документ или 
        /// потребителско име за обект потребител.  Ключовият атрибут за които се пази стойността е дефиниран в списъка на събитията
        /// и обектите за които се прави одитен запис.  
        public string Key { get; set; }

        /// <summary>
        /// Профил на потребителят, извършващ действието - данни за връзка към потребителски профил. Запазват се само ако потребителят се е автентикирал.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// IP адрес.
        /// </summary>
        public IPAddress IpAddress { get; set; }
    }

    /// <summary>
    /// Реализация на интерфeйс ILogActionEntity за поддържане и съхранение на обекти от тип LogAction.
    /// </summary>
    public interface ILogActionRepository :
        IRepository<LogAction, long?, LogActionSearchCriteria>,
        IRepositoryAsync<LogAction, long?, LogActionSearchCriteria>
    {
        Task CreateDocumentAccessedDataAsync(DocumentAccessedData documentAccessedData, CancellationToken cancellationToken);
        Task CreateDocumentAccessedDataValueAsync(DocumentAccessedDataValue documentAccessedDataValue, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс ILogActionEntity за поддържане и съхранение на обекти от тип LogAction.
    /// </summary>
    internal class LogActionRepository : EAURepositoryBase<LogAction, long?, LogActionSearchCriteria, LogActionDataContext>, ILogActionRepository
    {
        #region Constructors

        public LogActionRepository(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        #endregion

        #region ILogActionEntity

        protected override async Task CreateInternalAsync(LogActionDataContext context, LogAction item, CancellationToken cancellationToken)
        {
            long? logActionID = await context.LogActionCreateAsync(item.LogActionDate,
                                (short)item.ObjectType,
                                (short?)item.ActionType,
                                (short)item.Functionality,
                                item.Key,
                                item.LoginSessionID,
                                item.UserID,
                                item.UserEmail,
                                item.IpAddress,
                                item.AdditionalData,
                                cancellationToken);

            item.LogActionID = logActionID;
        }

        protected override async Task<IEnumerable<LogAction>> SearchInternalAsync(LogActionDataContext context, PagedDataState state, LogActionSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            var (data, count) = await context.LogActionSearchAsync(
                                                (short?)searchCriteria.Mode,
                                                searchCriteria.LogActionIDs,
                                                searchCriteria.LogActionDateFrom,
                                                searchCriteria.LogActionDateTo,
                                                (short?)searchCriteria.ObjectType,
                                                (short?)searchCriteria.ActionType,
                                                (short?)searchCriteria.Functionality,
                                                searchCriteria.Key,
                                                searchCriteria.UserID,
                                                searchCriteria.IpAddress?.GetAddressBytes(),
                                                state.StartIndex,
                                                state.PageSize,
                                                (state.StartIndex == 1),
                                                cancellationToken);

            state.Count = count ?? state.Count;

            return data.ToList();
        }

        protected override Task<IEnumerable<LogAction>> SearchInternalAsync(LogActionDataContext context, LogActionSearchCriteria searchCriteria, CancellationToken cancellationToken)
        {
            return SearchInternalAsync(context, PagedDataState.CreateMaxPagedDataState(), searchCriteria, cancellationToken);
        }

        public Task CreateDocumentAccessedDataAsync(DocumentAccessedData documentAccessedData, CancellationToken cancellationToken)
        {
            return DoOperationAsync(async (ctx, token) =>
            {
                var id = await ctx.DocumentAccessedDataCreateAsync(documentAccessedData.DocumentUri, documentAccessedData.DocumentTypeId, 
                    documentAccessedData.ApplicantNames, documentAccessedData.ApplicantIdentifier,
                    documentAccessedData.IpAddress, token);
                documentAccessedData.Id = id;
            }, cancellationToken);
        }

        public Task CreateDocumentAccessedDataValueAsync(DocumentAccessedDataValue documentAccessedDataValue, CancellationToken cancellationToken)
        {
            return DoOperationAsync((ctx, token) =>
            {
                return ctx.DocumentAccessedDataValueCreateAsync(documentAccessedDataValue.DocumentAccessedDataId.Value, (byte?)documentAccessedDataValue.DataType, documentAccessedDataValue.DataValue, token);

            }, cancellationToken);
        }

        #endregion
    }
}
