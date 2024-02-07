using CNSys;
using CNSys.Data;
using EAU.Nomenclatures.Models;
using EAU.Nomenclatures.Repositories;
using EAU.ServiceLimits.Cache;
using EAU.ServiceLimits.Models;
using EAU.ServiceLimits.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.ServiceLimits
{
    /// <summary>
    /// Интерфейс за работа с лимити на системата.
    /// </summary>
    public interface IDataServiceLimitService
    {
        /// <summary>
        /// Операция за редакция на лимит.
        /// </summary>
        /// <param name="serviceCode">Код.</param>
        /// <param name="requestsInterval">Интерцал.</param>
        /// <param name="requestsNumber">Брой.</param>
        /// <param name="cancellationToken">Токен поотказване.</param>
        Task<OperationResult> UpdateDataServiceLimitAsync(string serviceCode, TimeSpan requestsInterval, int requestsNumber, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за промяна на статуса на лимит.
        /// </summary>
        /// <param name="serviceCode">Код.</param>
        /// <param name="status">Статус.</param>
        /// <param name="cancellationToken">Токен поотказване.</param>
        Task<OperationResult> StatusChangeDataServiceLimitAsync(string serviceCode, DataServiceLimitStatus status, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за създаване на лимит за потребител.
        /// </summary>
        /// <param name="item">лимит.</param>
        /// <param name="cancellationToken">Токен поотказване.</param>
        /// <returns></returns>
        Task<OperationResult> CreateDataServiceUserLimitAsync(DataServiceUserLimit item, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за редакция на лимит за потребител.
        /// </summary>
        /// <param name="userLimitID">Идентификатор на лимит за потребител.</param>
        /// <param name="requestsInterval">Интерцал.</param>
        /// <param name="requestsNumber">Брой.</param>
        /// <param name="cancellationToken">Токен поотказване.</param>
        /// <returns></returns>
        Task<OperationResult> UpdateDataServiceUserLimitAsync(int userLimitID, TimeSpan requestsInterval, int requestsNumber, CancellationToken cancellationToken);

        /// <summary>
        /// Операция за промяна на статуса на лимит за потребител.
        /// </summary>
        /// <param name="userLimitID">Идентификатор на лимит за потребител.</param>
        /// <param name="status">Статус.</param>
        /// <param name="cancellationToken">Токен поотказване.</param>
        /// <returns></returns>
        Task<OperationResult> StatusChangeDataServiceUserLimitAsync(int userLimitID, DataServiceLimitStatus status, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Реализация на интерфейс за работа с лимити на системата.
    /// </summary>
    internal class DataServiceLimitService : IDataServiceLimitService
    {
        private readonly IServiceLimitsCache _serviceLimitsCache;
        private readonly IDataServiceLimitRepository _dataServiceLimitRepository;
        private readonly IDataServiceUserLimitRepository _dataServiceUserLimitRepository;
        private readonly IDbContextOperationExecutor _dBContextOperationExecutor;

        public DataServiceLimitService(
            IServiceLimitsCache serviceLimitsCache,
            IDataServiceLimitRepository dataServiceLimitRepository,
            IDataServiceUserLimitRepository dataServiceUserLimitRepository,
            IDbContextOperationExecutor dBContextOperationExecutor)
        {
            _serviceLimitsCache = serviceLimitsCache;
            _dataServiceLimitRepository = dataServiceLimitRepository;
            _dataServiceUserLimitRepository = dataServiceUserLimitRepository;
            _dBContextOperationExecutor = dBContextOperationExecutor;
        }

        #region DataServiceLimit

        public Task<OperationResult> UpdateDataServiceLimitAsync(string serviceCode, TimeSpan requestsInterval, int requestsNumber, CancellationToken cancellationToken)
        {
            return _dBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var limitCol = await _dataServiceLimitRepository.SearchInfoAsync(new DataServiceLimitsSearchCriteria() { ServiceCode = serviceCode }, cancellationToken);
                var limit = limitCol.Data.Single();
                limit.RequestsInterval = requestsInterval;
                limit.RequestsNumber = requestsNumber;
                await _dataServiceLimitRepository.UpdateAsync(limit, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);

        }

        public Task<OperationResult> StatusChangeDataServiceLimitAsync(string serviceCode, DataServiceLimitStatus status, CancellationToken cancellationToken)
        {
            return _dBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var limitCol = await _dataServiceLimitRepository.SearchInfoAsync(new DataServiceLimitsSearchCriteria() { ServiceCode = serviceCode }, cancellationToken);
                var limit = limitCol.Data.Single();
                limit.Status = status;
                await _dataServiceLimitRepository.UpdateAsync(limit, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        #endregion

        #region DataServiceUserLimit

        public async Task<OperationResult> CreateDataServiceUserLimitAsync(DataServiceUserLimit item, CancellationToken cancellationToken)
        {
            await _dataServiceUserLimitRepository.CreateAsync(item, cancellationToken);

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        public Task<OperationResult> UpdateDataServiceUserLimitAsync(int userLimitID, TimeSpan requestsInterval, int requestsNumber, CancellationToken cancellationToken)
        {
            return _dBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var limitCol = await _dataServiceUserLimitRepository.SearchInfoAsync(new DataServiceUserLimitsSearchCriteria() { UserLimitIDs = new int[] { userLimitID } }, cancellationToken);
                var limit = limitCol.Data.Single();
                limit.RequestsInterval = requestsInterval;
                limit.RequestsNumber = requestsNumber;

                await _dataServiceUserLimitRepository.UpdateAsync(limit, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        public Task<OperationResult> StatusChangeDataServiceUserLimitAsync(int userLimitID, DataServiceLimitStatus status, CancellationToken cancellationToken)
        {
            return _dBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var limitCol = await _dataServiceUserLimitRepository.SearchInfoAsync(new DataServiceUserLimitsSearchCriteria() { UserLimitIDs = new int[] { userLimitID } }, cancellationToken);
                var limit = limitCol.Data.Single();
                limit.Status = status;
                await _dataServiceUserLimitRepository.UpdateAsync(limit, cancellationToken);

                return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
            }, cancellationToken);
        }

        #endregion
    }
}
